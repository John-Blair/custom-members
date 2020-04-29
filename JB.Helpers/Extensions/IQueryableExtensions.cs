using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JB.Helpers
{
    static class IQueryableExtensions
    {
        public static List<SelectListItem> ToSelectList<T>(this IQueryable<T> query, Func<T, string> value, Func<T, string> text)
        {
            return query.ToList().Select(x => new SelectListItem() { Text = text(x), Value = value(x) }).ToList();
        }

    }
}
