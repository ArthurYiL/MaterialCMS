﻿using MaterialCMS.Entities.Documents.Layout;

namespace MaterialCMS.Entities.Documents.Web
{
    public class PageWidgetSort : SiteEntity
    {
        public virtual Webpage Webpage { get; set; }
        public virtual LayoutArea LayoutArea { get; set; }
        public virtual Widget.Widget Widget { get; set; }
        public virtual int Order { get; set; }
    }
}