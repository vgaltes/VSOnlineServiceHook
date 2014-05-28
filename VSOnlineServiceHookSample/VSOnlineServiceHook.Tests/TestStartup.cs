using Owin;
using System.Web.Http;

namespace VSOnlineServiceHook.Tests
{
    internal class TestStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            VSOnlineServiceHook.Api.Configuration.WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}