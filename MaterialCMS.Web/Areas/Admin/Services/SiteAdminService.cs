﻿using System.Collections.Generic;
using System.Linq;
using MaterialCMS.Entities.Multisite;
using MaterialCMS.Helpers;
using MaterialCMS.Models;
using MaterialCMS.Services.CloneSite;
using MaterialCMS.Tasks;
using NHibernate;

namespace MaterialCMS.Web.Areas.Admin.Services
{
    public class SiteAdminService : ISiteAdminService
    {
        private readonly ICloneSiteService _cloneSiteService;
        private readonly ISession _session;

        public SiteAdminService(ISession session, ICloneSiteService cloneSiteService)
        {
            _session = session;
            _cloneSiteService = cloneSiteService;
        }

        public List<Site> GetAllSites()
        {
            return _session.QueryOver<Site>().OrderBy(site => site.Name).Asc.Cacheable().List().ToList();
        }

        public Site GetSite(int id)
        {
            return _session.Get<Site>(id);
        }

        public void AddSite(Site site, List<SiteCopyOption> options)
        {
            _session.Transact(session => session.Save(site));

            _cloneSiteService.CloneData(site, options);
        }

        public void SaveSite(Site site)
        {
            _session.Transact(session => session.Update(site));
        }

        public void DeleteSite(Site site)
        {
            _session.Transact(session => session.Delete(site));
        }
    }
}