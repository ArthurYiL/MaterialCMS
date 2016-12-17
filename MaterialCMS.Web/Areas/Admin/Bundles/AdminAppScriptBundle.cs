﻿using System.Collections.Generic;
using System.Linq;
using MaterialCMS.Services;
using MaterialCMS.Website.Optimization;

namespace MaterialCMS.Web.Areas.Admin.Bundles
{
    public class AdminAppScriptBundle : IScriptBundle
    {
        private readonly IEnumerable<IAppScriptList> _scriptLists;

        public AdminAppScriptBundle(IEnumerable<IAppScriptList> scriptLists)
        {
            _scriptLists = scriptLists;
        }

        public string Url
        {
            get { return "~/admin/scripts/apps"; }
        }

        public IEnumerable<string> Files
        {
            get
            {
                return _scriptLists.SelectMany(list => list.AdminScripts);
            }
        }
    }
}