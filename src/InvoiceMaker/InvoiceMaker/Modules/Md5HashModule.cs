using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Modules
{
    public class Md5HashModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += HandleBeginRequest;
        }

        private const string apiHashPath = "/api/hash/";
        private const string apiBinHashPath = "/api/binhash/";

        private void HandleBeginRequest(object sender, EventArgs e)
        {
            var app = sender as InvoiceMakerApplication;
            if (app != null)
            {
                HttpContext context = app.Context;

                string path = context.Request.FilePath;

                if (path.StartsWith(apiHashPath))
                {
                    string valueToHash = path.Replace(apiHashPath, string.Empty);
                    context.RewritePath($"/{valueToHash}.hash");
                }
                else if (path.StartsWith("/api/binhash/"))
                {
                    string valueToHash = path.Replace(apiBinHashPath, string.Empty);
                    context.RewritePath($"/{valueToHash}.binhash");
                }
            }
        }
    }
}