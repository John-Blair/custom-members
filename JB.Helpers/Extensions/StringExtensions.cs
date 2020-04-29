using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB.Helpers
{
    public static class StringExtensions
    {
        public static string EmptyIfNull(this string str)
        {
            return str ?? "";
        }

        public static string NullIfEmpty(this string str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }

        public static Boolean ToBoolean(this String str)
        {
            return ConvertToBoolean(str, false);
        }

        public static Boolean ConvertToBoolean(String str, Boolean defaultValue)
        {
            String[] booleanStringOff = { "0", "off", "no" ,"n"};

            if (String.IsNullOrEmpty(str))
                return defaultValue;
            else if (booleanStringOff.Contains(str, StringComparer.InvariantCultureIgnoreCase))
                return false;

            Boolean result;
            if (!Boolean.TryParse(str, out result))
                result = true;

            return result;
        }
    }
}
