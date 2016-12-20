using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    class ArtBrowser : EntityBase
    {
        public static ArtBrowser Instance
        {
            get
            {
                return Singleton<ArtBrowser>.Instance;
            }
        }

        COMLister mRoot = null;
        public COMLister root
        {
            get
            {
                return mRoot;
            }
        }

        COMStoreFolder mStore = null;
        public COMStoreFolder store
        {
            get
            {
                return mStore;
            }
        }

        COMDesktop mDestop = null;
        public COMDesktop destop
        {
            get
            {
                return mDestop;
            }
        }

        COMUpward mBackward = null;
        public COMUpward backward
        {
            get
            {
                return mBackward;
            }
        }

        public ArtBrowser()
        {
            mRoot = CComponent.instance<COMLister>();
            mStore = CComponent.instance<COMStoreFolder>();
            mDestop = CComponent.instance<COMDesktop>();
            mBackward = CComponent.instance<COMUpward>();
            mRoot.items.pushBack(mDestop.getComponent<COMListItem>());
            mRoot.items.pushBack(mStore.getComponent<COMListItem>());
            //history.add(mRoot);

            CEventHub.Instance.evtBackward += () =>
            {
                var ls = history.undo();
                if(ls != null)
                {
                    ls.getComponent<COMPanel>().setView();
                    CEventHub.Instance.gotoURL(history.ToList());
                }
            };

            CEventHub.Instance.evtForward += () =>
            {
                var ls = history.redo();
                if (ls != null)
                {
                    ls.getComponent<COMPanel>().setView();
                    CEventHub.Instance.gotoURL(history.ToList());
                }
            };

            CEventHub.Instance.evtKeyUp += key =>
            {
                if (key == System.Windows.Input.Key.Return)
                {
                    var choose = history.data.getChoosed();
                    if(choose != null)
                    {
                        var ls = choose.getComponent<COMLister>();
                        if (ls != null)
                        {
                            accessInto(ls);
                        }
                        else if (choose.getComponent<COMUpward>() != null)
                        {
                            exitFrom();
                        }
                    }
                }
                else if(key == System.Windows.Input.Key.Back)
                {
                    exitFrom();
                }
                else if(key == System.Windows.Input.Key.Left)
                {
                    var choose = history.data.getChoosed();
                    if(choose == null)
                    {
                        var i = history.data.currentItems().Last();
                        i.getComponent<COMIcon>().currentUI().choosed = true;
                    }
                    else
                    {
                        var i = history.data.currentItems().leftOf(choose, true);
                        i.getComponent<COMIcon>().currentUI().choosed = true;
                    }
                }
                else if(key == System.Windows.Input.Key.Right)
                {
                    var choose = history.data.getChoosed();
                    if (choose == null)
                    {
                        var i = history.data.currentItems().First();
                        i.getComponent<COMIcon>().currentUI().choosed = true;
                    }
                    else
                    {
                        var i = history.data.currentItems().rightOf(choose, true);
                        i.getComponent<COMIcon>().currentUI().choosed = true;
                    }
                }
                else if(key == System.Windows.Input.Key.Home)
                {
                    var i = history.data.currentItems().First();
                    i.getComponent<COMIcon>().currentUI().choosed = true;
                }
                else if(key == System.Windows.Input.Key.End)
                {
                    var i = history.data.currentItems().Last();
                    i.getComponent<COMIcon>().currentUI().choosed = true;
                }
            };
        }

        DataHistory<COMLister> history = new DataHistory<COMLister>();
        public void accessInto(COMLister ls)
        {
            history.add(ls);
            ls.getComponent<COMPanel>().setView();
            CEventHub.Instance.gotoURL(history.ToList());
        }

        public void exitFrom()
        {
            var current = history.undo();
            if(current != null)
            {
                current.getComponent<COMPanel>().setView();
                CEventHub.Instance.gotoURL(history.ToList());
            }
        }

        public void setView()
        {
            accessInto(destop.getComponent<COMLister>());
        }

        public void setAddress(List<COMLister> ls)
        {
            history.clear();
            foreach(var item in ls)
            {
                history.add(item);
            }
            var last = ls.Last();
            last.getComponent<COMPanel>().setView();
            CEventHub.Instance.gotoURL(history.ToList());
        }
    }
}
