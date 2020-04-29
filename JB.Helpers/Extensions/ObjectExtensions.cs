using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB.Helpers
{
    static public class ObjectExtensions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            /*
            IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
            foreach (var item in anonymousDictionary)
                expando.Add(item);
                */
            return (ExpandoObject)expando;
        }
    }
}
