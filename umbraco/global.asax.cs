using JB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;

namespace JB
{
    public class MvcApplication : UmbracoApplication
    {
        protected override void OnApplicationError(object sender, EventArgs e)
        {
            const string newline = "<br/>";
            base.OnApplicationError(sender, e);
            Exception exc = Server.GetLastError();
            string innerException = exc.InnerException?.ToString() ?? "No inner exception";

            GMailService.SendMail(EnvironmentSecret.Instance.EmailAdmin,
                $"Global Application Error on {EnvironmentService.Stage} {EnvironmentService.Domain}",
                EnvironmentService.Environment() + newline +
                "Error Message:" + exc.Message + newline +
                "Stack Trace:" + exc.StackTrace + newline +
                "Inner Exception:" + innerException + newline );
        }
    }
}
