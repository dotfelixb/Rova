using System;
using System.Threading.Tasks;

namespace Rova.Web.Components
{
    public class LinkData
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
    public class Breadcrumb
    {
        public LinkData[] Routes { get; set; } = new LinkData[]{};
        public event Action OnChange;

        public void SetRoute(LinkData[] routes)
        {
            Routes = routes;
            NotifyChange();
        }

        private void NotifyChange()
        {
            OnChange?.Invoke();
        }
    }
}