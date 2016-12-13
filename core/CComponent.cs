/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 动态组件
 * */
using System;
using System.Collections.Generic;
using System.Linq;

namespace ns_artDesk.core
{
    public class COMObj
    {
        List<CComponent> mComponents = new List<CComponent>();
        public string name
        {
            get;
            set;
        }
        
        public IEnumerable<CComponent> addComponent(Type t)
        {
            List<CComponent> coms = new List<CComponent>();
            foreach (var com in mComponents)
            {
                if (t.IsAssignableFrom(com.GetType()))
                {
                    coms.Add(com);
                    return coms;
                }
            }

            var dps = CComponent.getDependcy(t);
            foreach (var d in dps)
            {
                coms.AddRange(addComponent(d.com));
            }

            var c = (CComponent)Activator.CreateInstance(t);
            c.mObj = this;
            this.mComponents.Add(c);
            coms.Add(c);
            return coms;
        }

        public IEnumerable<CComponent> addComponent<T>() where T : CComponent
        {
            return addComponent(typeof(T));
        }

        public CComponent getComponent(Type t)
        {
            foreach (var com in mComponents)
            {
                if (t.IsAssignableFrom(com.GetType()))
                {
                    return com;
                }
            }
            return null;
        }

        public T getComponent<T>() where T : CComponent
        {
            return getComponent(typeof(T)) as T;
        }

        public IEnumerable<CComponent> removeComponent(Type t)
        {
            List<CComponent> coms = new List<CComponent>();
            var com = getComponent(t);
            if (com == null) return coms;

            var dps = CComponent.getDependcy(t);
            foreach (var d in dps)
            {
                coms.AddRange(removeComponent(d.com));
            }
            if (com != null)
            {
                mComponents.Remove(com);
            }
            coms.Add(com);
            return coms;
        }

        public IEnumerable<CComponent> removeComponent(CComponent com)
        {
            return removeComponent(com.GetType());
        }

        public IEnumerable<CComponent> removeComponent<T>() where T : CComponent
        {
            return removeComponent(typeof(T));
        }

        public IEnumerable<CComponent> components
        {
            get
            {
                return mComponents;
            }
        }
    }

    public class CComponent
    {
        internal COMObj mObj = null;

        public COMObj comObject
        {
            get
            {
                return mObj;
            }
        }

        public IEnumerable<CComponent> addComponent<T>() where T : CComponent
        {
            return mObj.addComponent<T>();
        }

        public T getComponent<T>() where T : CComponent
        {
            return mObj.getComponent<T>();
        }

        public void removeComponent<T>() where T : CComponent
        {
            mObj.removeComponent<T>();
        }

        internal static IEnumerable<RequireComAttribute> getDependcy(Type t)
        {
            var attrs = t.GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (attr is RequireComAttribute)
                {
                    yield return attr as RequireComAttribute;
                }
            }
        }
    }

    public class ComInfoAttribute : System.Attribute
    {
        public string name;
    }

    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RequireComAttribute : System.Attribute
    {
        public Type com;

        public RequireComAttribute(Type com)
        {
            this.com = com;
        }
    }

}
