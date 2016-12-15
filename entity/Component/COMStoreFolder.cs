using ns_artDesk.core;

namespace ns_artDesk
{
    [RequireCom(typeof(COMLister))]
    class COMStoreFolder : CComponent
    {
        public override void onAwake()
        {
            base.onAwake();
            var ls = getComponent<COMLister>();
            ls.doGetItem = () =>
            {
                ListEx<COMListItem> items = new ListEx<COMListItem>();
                var tags = CArtAppList.Instance.getAllTags();
                foreach (var tag in tags)
                {
                    var tagItem = instance<COMStoreTag>();
                    tagItem.tag = tag;
                    items.pushBack(tagItem.getComponent<COMListItem>());
                }
                return items;
            };
        }
    }
}
