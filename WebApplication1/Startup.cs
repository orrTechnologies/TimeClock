﻿using Microsoft.Owin;
using Owin;
using TimeClock.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace TimeClock.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
