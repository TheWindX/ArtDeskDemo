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
                items.pushBack(ArtBrowser.Instance.backward.getComponent<COMListItem>());
                var tags = CArtAppList.Instance.getAllTags();
                foreach (var tag in tags)
                {
                    var tagItem = instance<COMStoreTag>();
                    tagItem.tag = tag;
                    items.pushBack(tagItem.getComponent<COMListItem>());
                }
                ls.mItem = items;
                return items;
            };

            var icon = getComponent<COMIcon>();
            var ui = icon.getUI();
            ui.title = "app store";
            ui.mIcon.Source = IconManager.Instance.store;
        }
    }
}
