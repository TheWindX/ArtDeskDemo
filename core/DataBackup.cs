using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    public class DataHistory<Data> : IEnumerable<Data> where Data : class
    {
        //版本队列
        List<Data> mDataBackup = new List<Data>();
        int dataIdx = -1;//版本号

        public Data data
        {
            get
            {
                return mDataBackup[dataIdx];
            }
        }

        private void trim()
        {
            if (dataIdx == -1)
                return;
            mDataBackup.RemoveRange(dataIdx + 1, mDataBackup.Count - dataIdx - 1);
        }

        //备份
        public void add(Data data)
        {
            trim();//消除所有未来版本
            mDataBackup.Add(data);
            dataIdx = mDataBackup.Count - 1;
        }

        //撤销
        public Data undo()
        {
            dataIdx--;
            if (dataIdx < 0)
            {
                dataIdx = 0;
                return null;
            }
            return data;
                
        }

        //重做
        public Data redo()
        {
            dataIdx++;
            if (dataIdx >= mDataBackup.Count)
            {
                dataIdx = mDataBackup.Count - 1;
                return null;
            }
            return data;
        }

        public IEnumerator<Data> GetEnumerator()
        {
            return mDataBackup.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mDataBackup.GetEnumerator();
        }
    }
}
