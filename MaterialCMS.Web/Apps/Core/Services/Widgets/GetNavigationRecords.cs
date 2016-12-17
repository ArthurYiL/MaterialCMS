﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MaterialCMS.Entities.Documents.Web;
using MaterialCMS.Helpers;
using MaterialCMS.Services.Widgets;
using MaterialCMS.Web.Apps.Core.Models.Navigation;
using MaterialCMS.Web.Apps.Core.Widgets;
using NHibernate;
using NHibernate.Criterion;

namespace MaterialCMS.Web.Apps.Core.Services.Widgets
{
    public class GetNavigationRecords : GetWidgetModelBase<Navigation>
    {
        private readonly ISession _session;

        public GetNavigationRecords(ISession session)
        {
            _session = session;
        }

        public override object GetModel(Navigation widget)
        {
            var rootPages = GetPages(null);
            var childPages = widget.IncludeChildren ? GetPages(rootPages) : new List<Webpage>();
            var navigationRecords =
                rootPages.Where(webpage => webpage.Published).OrderBy(webpage => webpage.DisplayOrder)
                       .Select(webpage => new NavigationRecord
                       {
                           Text = MvcHtmlString.Create(webpage.Name),
                           Url = MvcHtmlString.Create("/" + webpage.LiveUrlSegment),
                           Children = childPages.Where(webpage1 => webpage1.ParentId == webpage.Id)
                                            .Select(webpage1 =>
                                                    new NavigationRecord
                                                    {
                                                        Text = MvcHtmlString.Create(webpage1.Name),
                                                        Url = MvcHtmlString.Create("/" + webpage1.LiveUrlSegment)
                                                    }).ToList()
                       }).ToList();

            return new NavigationList(navigationRecords.ToList());
        }

        private IList<Webpage> GetPages(IList<Webpage> parents)
        {
            var queryOver = _session.QueryOver<Webpage>();
            if (parents == null)
            {
                queryOver = queryOver.Where(webpage => webpage.Parent == null);
            }
            else
            {
                var parentIds = parents.Select(p => p.Id).ToList();
                queryOver = queryOver.Where(webpage => webpage.Parent.Id.IsIn(parentIds));
            }
            return
                queryOver.Where(webpage => webpage.RevealInNavigation)
                    .OrderBy(webpage => webpage.DisplayOrder).Asc
                    .Cacheable()
                    .List().ToList(x => x.Published);
        }
    }
}