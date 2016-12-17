﻿using System;
using MaterialCMS.Entities.People;

namespace MaterialCMS.Web.Areas.Admin.Models.UserEdit
{
    public abstract class UserTabBase
    {
        public abstract int Order { get; }
        public abstract string Name(User user);

        public abstract bool ShouldShow(User user);

        public abstract Type ParentType { get; }
    }
}