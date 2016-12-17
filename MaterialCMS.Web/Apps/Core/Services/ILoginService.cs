﻿using System;
using System.Threading.Tasks;
using MaterialCMS.Entities.People;
using MaterialCMS.Web.Apps.Core.Models;
using MaterialCMS.Web.Apps.Core.Models.RegisterAndLogin;

namespace MaterialCMS.Web.Apps.Core.Services
{
    public interface ILoginService
    {
        Task<LoginResult> AuthenticateUser(LoginModel loginModel);
    }
}