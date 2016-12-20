/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: list加强
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    public class ListEx<T> : IEnumerable<T> where T : class
    {
        List<T> mContainer = new List<T>();
        public void pushBack(T v)
        {
            if (mContainer.Contains(v)) throw new Exception();
            mContainer.Add(v);
        }

        public void pushFront(T v)
        {
            if (mContainer.Contains(v)) throw new Exception();
            mContainer.Insert(0, v);
        }

        public void remove(T v)
        {
            mContainer.Remove(v);
        }

        public void clear()
        {
            mContainer.Clear();
        }

        public void stepLeft(T v, bool cir = false)
        {
            int i = mContainer.IndexOf(v);
            i--;
            if (i < 0)
            {
                if (!cir) return;
                i = mContainer.Count - 1;
            }
            mContainer.Remove(v);
            mContainer.Insert(i, v);
        }

        public T leftOf(T v, bool cir = false)
        {
            int i = mContainer.IndexOf(v);
            i--;
            if (i < 0)
            {
                if (!cir) return null;
                i = mContainer.Count - 1;
            }
            return mContainer[i];
        }

        public void stepRight(T v, bool cir = false)
        {
            int i = mContainer.IndexOf(v);
            i++;
            if (i == mContainer.Count)
            {
                if (!cir) return;
                i = 0;
            }
            mContainer.Remove(v);
            mContainer.Insert(i, v);
        }

        public T rightOf(T v, bool cir = false)
        {
            int i = mContainer.IndexOf(v);
            i++;
            if (i == mContainer.Count)
            {
                if (!cir) return null;
                i = 0;
            }
            return mContainer[i];
        }

        public void insertBefore(T v, T u)
        {
            if (v == u) return;
            mContainer.Remove(v);
            int i = mContainer.IndexOf(u);
            mContainer.Insert(i, v);
        }

        public void insertAfter(T v, T u)
        {
            if (v == u) return;
            mContainer.Remove(v);
            int i = mContainer.IndexOf(u);
            mContainer.Insert(i + 1, v);
        }

        

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)mContainer).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)mContainer).GetEnumerator();
        }

    }
}
