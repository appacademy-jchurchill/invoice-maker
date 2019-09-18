using InvoiceMaker.Data;
using InvoiceMaker.Initialization;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Web;
using System.Web.Routing;

namespace InvoiceMaker
{
    public class InvoiceMakerApplication : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Debug.WriteLine("Application_Start");

            RouteConfiguration.AddRoutes(RouteTable.Routes);

            Database.SetInitializer(
                new CreateDatabaseIfNotExists<Context>());
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Debug.WriteLine("Application_End");
        }

        public override void Init()
        {
            base.Init();

            BeginRequest += HandleBeginRequest;
            AuthenticateRequest += HandleAuthenticateRequest;
        }

        protected void HandleBeginRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleBeginRequest");
        }

        private void HandleAuthenticateRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleAuthenticateRequest");
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    Debug.WriteLine("Application_BeginRequest");
        //}
    }
}