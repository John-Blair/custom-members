using System.Web.Mvc;
using JB.Helpers;
using Umbraco.Web.Mvc;

namespace JB
{
    public class ContactController : SurfaceController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonNetResult SendMail(ContactViewModel contact)
        {
            const string newline = "<br/>";
            var result = new JsonNetResult
            {
                Data = $"E:{contact.EmailAddress} M:{contact.Message}N:{contact.Name}S:{contact.Subject}"
            };
            GMailService.SendMail(
                EnvironmentSecret.Instance.EmailAdmin,
                $"Contact From {contact.EmailAddress} : {contact.Subject} on {EnvironmentService.Stage}",
                $"{EnvironmentService.Environment()}Contact Name:{contact.Name}{newline}Contact Email:{contact.EmailAddress}{newline}Message:{contact.Message}");
            return result;
        }
    }
}