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
        public T fromSting<T>(string text) where T : class
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(text);
            return fromXML<T>(doc.DocumentElement);
        }

        public T fromXML<T>(XmlElement elem) where T : class
        {
            return fromXML(typeof(T), elem) as T;
        }

        public object fromXML(Type t, XmlElement elem)
        {
            object instance = Activator.CreateInstance(t);

            if (t == typeof(bool))
            {
                return Convert.ToBoolean(elem.GetAttribute("value"));
            }
            else if (t == typeof(int))
            {
                return Convert.ToInt32(elem.GetAttribute("value"));
            }
            else if (t == typeof(double))
            {
                return Convert.ToInt32(elem.GetAttribute("value"));
            }
            else if (t == typeof(string))
            {
                return elem.GetAttribute("value");
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
                        foreach (XmlElement childElement in ls[0].ChildNodes)
                        {
                            var obj = fromXML(ts[0], childElement);
                            var objList = (ins as IList);
                            objList.Insert(objList.Count, obj);
                        }
                        prop.SetValue(instance, ins);
                    }
                }
                else
                {
                    foreach (XmlElement childElement in elem.ChildNodes)
                    {
                        if (childElement.Name == prop.Name)
                        {
                            var obj = fromXML(prop.PropertyType, childElement);
                            prop.SetValue(instance, obj);
                            break;
                        }
                    }
                }
            }
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
                var e = doc.CreateElement(name);
                e.SetAttribute("value", obj.ToString());
                return e;
            }
            else if (obj is IList)
            {
                var subs = (obj as IList);
                var e = doc.CreateElement(name);
                foreach (var sub in subs)
                {
                    var e1 = toXML("child", sub, doc);
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
                    p.Value = prop.GetValue(obj).ToString();
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
