﻿using System.Web.Mvc;
using MaterialCMS.Helpers;
using MaterialCMS.Services;
using NHibernate;

namespace MaterialCMS.Settings
{
    public abstract class SiteSettingsBase
    {
        public virtual string TypeName
        {
            get { return GetType().Name.BreakUpString(); }
        }

        public virtual string DivId
        {
            get { return GetType().Name.BreakUpString().ToLowerInvariant().Replace(" ", "-"); }
        }

        public virtual bool RenderInSettings
        {
            get { return false; }
        }

        public virtual void SetViewData(ISession session, ViewDataDictionary viewDataDictionary)
        {
        }
    }
}