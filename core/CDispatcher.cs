/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 其它线程向主线程投递任务
 * usage: CDispatcher.Instance.post(() => { <some action from other thread> })
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    class CDispatcher : Singleton<CDispatcher>
    {
        private readonly object _lock = new object();
        Queue<Action> mTask = new Queue<Action>();
        public void post(Action act)
        {
            lock(_lock)
            {
                mTask.Enqueue(act);
            }
        }

        public void init()
        {

        }

        public void update()
        {
            lock(_lock)
            {
                while(mTask.Count != 0)
                {
                    var act = mTask.Dequeue();
                    try
                    {
                        act?.Invoke();
                    }
                    catch(Exception ex)
                    {
                        CLogger.Instance.error("CDispatcher", ex.ToString());
                    }
                }
            }
        }

        public void exit()
        {

        }
    }
}
