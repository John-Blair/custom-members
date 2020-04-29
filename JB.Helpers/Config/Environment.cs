using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace JB.Helpers
{
    public sealed class Environment
    {
        private static readonly NameValueCollection settingCollection =
               (NameValueCollection)ConfigurationManager.GetSection("Environment");
        private static readonly Environment instance = new Environment();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Environment()
        {
        }

        private bool _TotalUnitsInit;
        private int _TotalUnits;
        public int TotalUnits
        {
            get
            {
                if (!_TotalUnitsInit)
                {
                    _TotalUnitsInit = true;
                    var totalUnits = settingCollection["TotalUnits"] ?? "";
                    if (!string.IsNullOrEmpty(totalUnits))
                    {
                        _TotalUnits = Convert.ToInt32(totalUnits);
                    }
                }
                return _TotalUnits;
            }
        }

       


        private bool _DebugMenuInit;
        private bool _DebugMenu;
        public bool DebugMenu
        {
            get
            {
                if (!_DebugMenuInit)
                {
                    _DebugMenuInit = true;
                    _DebugMenu = settingCollection["DebugMenu"].ToBoolean();

                }

                return _DebugMenu;
            }
        }


        private Environment()
        {

        }

        public static Environment Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
