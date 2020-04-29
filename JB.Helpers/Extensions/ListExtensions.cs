using System.Collections.Generic;
using System.Web.Mvc;

namespace JB.Helpers
{
    static class ListExtensions
    {
        public static List<SelectListItem> AddPrompt(this List<SelectListItem> list)
        {
            var prompt = new SelectListItem(){ Text = "Please select one", Value = "-1" };

            list.Insert(0, prompt);

            return list;
                
        }

    }
}
