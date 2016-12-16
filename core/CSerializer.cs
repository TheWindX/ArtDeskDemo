/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 对类进行序列化和反序列化
 * 1. （反）序列化所有的属性成员
 * 2. 需加上[SerializedProp]标签
 * 3. 简单类型支持bool, int, double, string, 复合类型支持List<XXX>, 自定义类
 * 4. List<int> 表示为 <xxxlist> <item value="1"/> <item value="1"/> <item value="1"/> </xxxlist>
 * 例子如下
<applist>
    <apps>
        <item path="/app_root/2d/photoshop_11">
            <meta name="photoshop_11" version="12345" title="photoshop11" desc="descxxx" type="internal_standalone"/>
        </item>
        <item path="/app_root/2d/photoshop_12">
            <meta name="photoshop_12" version="212345" title="photoshop12" desc="descxxx" type="internal_standalone"/>
        </item>
    </apps>
    <numbers>
        <item value="1">
        <item value="2">
        <item value="3">
    </numbers>
</applist>

    public class CArtAppMeta
    {
        [SerializedProp]
        public string name { get; set; }
        [SerializedProp]
        public double version { get; set; }
        [SerializedProp]
        public string title { get; set; }
        [SerializedProp]
        public string desc { get; set; }
        [SerializedProp]
        public string type { get; set; }
    }

    public class CArtApp
    {
        [SerializedProp]
        public string path { get; set; }
        [SerializedProp]
        public CArtAppMeta meta { get; set; }
    }


    public class CArtAppList
    {
        [SerializedProp]
        public List<CArtApp> apps { get; set; }
        public List<int> numbers { get; set; }
    }
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;

namespace ns_artDesk.core
{
    class CSerializer : Singleton<CSerializer>
    {
        public T fromXML<T>(string text) where T : class
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(text);
            return fromXML<T>(doc.DocumentElement);
        }

        public T fromXML<T>(XmlElement elem) where T : class
        {
            return fromXML(typeof(T), elem) as T;
        }

        public void fromXML(ref object instance, string text)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(text);
                fromXML(ref instance, doc.DocumentElement);
            }
            catch(XmlException ex)
            {
                CLogger.Instance.error("CSerializer", ex.ToString());
            }
        }


        public void fromXML(ref object instance, XmlElement elem)
        {
            var t = instance.GetType();
            if (t == typeof(bool))
            {
                instance = Convert.ToBoolean(elem.GetAttribute("value"));
            }
            else if (t == typeof(int))
            {
                instance = Convert.ToInt32(elem.GetAttribute("value"));
            }
            else if (t == typeof(double))
            {
                instance = Convert.ToInt32(elem.GetAttribute("value"));
            }
            else if (t == typeof(string))
            {
                instance = elem.GetAttribute("value");
            }

            var props = t.GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(bool))
                {
                    if (!elem.HasAttribute(prop.Name)) continue;
                    var value = elem.GetAttribute(prop.Name);
                    prop.SetValue(instance, Convert.ToBoolean(value));
                }
                else if (prop.PropertyType == typeof(int))
                {
                    if (!elem.HasAttribute(prop.Name)) continue;
                    var value = elem.GetAttribute(prop.Name);
                    prop.SetValue(instance, Convert.ToInt32(value));
                }
                else if (prop.PropertyType == typeof(double))
                {
                    if (!elem.HasAttribute(prop.Name)) continue;
                    var value = elem.GetAttribute(prop.Name);
                    prop.SetValue(instance, Convert.ToDouble(value));
                }
                else if (prop.PropertyType == typeof(string))
                {
                    if (!elem.HasAttribute(prop.Name)) continue;
                    var value = elem.GetAttribute(prop.Name);
                    prop.SetValue(instance, value);
                }
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition()
        == typeof(List<>))
                {
                    var ls = elem.GetElementsByTagName(prop.Name);
                    if (ls.Count == 0)
                        continue;
                    else
                    {
                        var listType = typeof(List<>);
                        var ts = prop.PropertyType.GetGenericArguments();
                        var constructedListType = listType.MakeGenericType(ts);
                        var ins = Activator.CreateInstance(constructedListType);
                        foreach (XmlNode childNode in ls[0].ChildNodes)
                        {
                            if (childNode is XmlElement)
                            {
                                var childElement = childNode as XmlElement;
                                var obj = fromXML(ts[0], childElement);
                                var objList = (ins as IList);
                                objList.Insert(objList.Count, obj);
                            }
                        }
                        prop.SetValue(instance, ins);
                    }
                }
                else
                {
                    foreach (XmlNode childNode in elem.ChildNodes)
                    {
                        if(childNode is XmlElement)
                        {
                            var childElement = childNode as XmlElement;
                            if (childElement.Name == prop.Name)
                            {
                                var obj = fromXML(prop.PropertyType, childElement);
                                prop.SetValue(instance, obj);
                                break;
                            }
                        }
                        
                    }
                }
            }
        }
        public object fromXML(Type t, XmlElement elem)
        {
            object instance = null;
            if (t == typeof(string))
            {
                instance = "";
            }
            else
            {
                try
                {
                    instance = Activator.CreateInstance(t);
                }
                catch(Exception ex)
                {
                    CLogger.Instance.error("CSerializer",  ex.ToString());
                }
            }
            fromXML(ref instance, elem);
            return instance;
        }

        public string toXML(Object obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            doc.AppendChild(toXML("root", obj, doc));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public XmlElement toXML(string name, Object obj, XmlDocument doc)
        {
            if (obj == null) return null;
            var tx = obj.GetType();
            if (obj is bool)
            {
                var e = doc.CreateElement(name);
                e.SetAttribute("value", obj.ToString());
                return e;
            }
            else if (obj is int)
            {
                var e = doc.CreateElement(name);
                e.SetAttribute("value", obj.ToString());
                return e;
            }
            else if (obj is double)
            {
                var e = doc.CreateElement(name);
                e.SetAttribute("value", obj.ToString());
                return e;
            }
            else if (obj is string)
            {
                var str = (obj as string);
                if (str == null)
                    str = "";
                var e = doc.CreateElement(name);
                e.SetAttribute("value", str);
                return e;
            }
            else if (obj is IList)
            {
                var subs = (obj as IList);
                var e = doc.CreateElement(name);
                foreach (var sub in subs)
                {
                    var e1 = toXML("item", sub, doc);
                    e.AppendChild(e1);
                }
                return e;
            }
            XmlElement elem = doc.CreateElement(name);
            var t = obj.GetType();
            var props = t.GetProperties();
            foreach (var prop in props)
            {
                var att = prop.GetCustomAttribute<SerializedPropAttribute>();
                if (att == null) continue;

                if (prop.PropertyType == typeof(bool))
                {
                    var p = doc.CreateAttribute(prop.Name);
                    p.Value = prop.GetValue(obj).ToString();
                    elem.Attributes.Append(p);
                }
                else if (prop.PropertyType == typeof(int))
                {
                    var p = doc.CreateAttribute(prop.Name);
                    p.Value = prop.GetValue(obj).ToString();
                    elem.Attributes.Append(p);
                }
                else if (prop.PropertyType == typeof(double))
                {
                    var p = doc.CreateAttribute(prop.Name);
                    p.Value = prop.GetValue(obj).ToString();
                    elem.Attributes.Append(p);
                }
                else if (prop.PropertyType == typeof(string))
                {
                    var p = doc.CreateAttribute(prop.Name);
                    var str = prop.GetValue(obj);
                    if (str == null) str = "";
                    p.Value = str.ToString();
                    elem.Attributes.Append(p);
                }
                else
                {
                    var sub = toXML(prop.Name, prop.GetValue(obj), doc);
                    if (sub != null)
                        elem.AppendChild(sub);
                }
            }
            return elem;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class SerializedPropAttribute : System.Attribute
    {
    }
}
