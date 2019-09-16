using System;
using System.Diagnostics;
using System.Web;

namespace InvoiceMaker
{
    public class InvoiceMakerApplication : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Debug.WriteLine("Application_Start");
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