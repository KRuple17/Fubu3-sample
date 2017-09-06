using System;
using System.Web;
using Fubu3App.Registry;
using FubuMVC.Core;

namespace Fubu3
{
    public class Global : HttpApplication
    {
        private FubuRuntime _runtime;

        protected void Application_Start(object sender, EventArgs e)
        {
            _runtime = FubuRuntime.For<DemoFubuRegistry>();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _runtime.Dispose();
        }
    }
}