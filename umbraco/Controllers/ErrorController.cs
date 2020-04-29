using System.Web.Mvc;
using JB.Helpers;
using Umbraco.Web.Mvc;
using System.Net;
using System;

namespace JB
{
    public class ErrorController : SurfaceController
    {
        [HttpPost]
        public ActionResult StatusCode(int statusCode)
        {
            if (statusCode==500)
            {
                throw new DivideByZeroException();
            }
            return new HttpStatusCodeResult(statusCode);
        }
    }
}