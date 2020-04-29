using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace JB.Helpers
{
    public sealed class EnvironmentSecret
    {
        private static readonly NameValueCollection settingCollection =
               (NameValueCollection)ConfigurationManager.GetSection("EnvironmentSecret");
        private static readonly EnvironmentSecret instance = new EnvironmentSecret();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static EnvironmentSecret()
        {
        }


        private string _SmtpHost;
        public string SmtpHost
        {
            get {
                return _SmtpHost ?? 
                    (_SmtpHost = settingCollection["SmtpHost"] ?? "");
            }
        }

        private string _EmailAdmin;
        public string EmailAdmin
        {
            get
            {
                return _EmailAdmin ??
                    (_EmailAdmin = settingCollection["EmailAdmin"] ?? "");
            }
        }

        private string _NetworkCredentialUserName;
        public string NetworkCredentialUserName
        {
            get
            {
                return _NetworkCredentialUserName ??
                    (_NetworkCredentialUserName = settingCollection["NetworkCredentialUserName"] ?? "");
            }
        }

        private string _NetworkCredentialPassword;
        public string NetworkCredentialPassword
        {
            get
            {
                return _NetworkCredentialPassword ??
                    (_NetworkCredentialPassword = settingCollection["NetworkCredentialPassword"] ?? "");
            }
        }
        private string _EmailApiKey;
        public string EmailApiKey
        {
            get
            {
                return _EmailApiKey ??
                    (_EmailApiKey = settingCollection["EmailApiKey"] ?? "");
            }
        }


        private string _AdminKey;
        public string AdminKey
        {
            get
            {
                return _AdminKey ??
                    (_AdminKey = settingCollection["AdminKey"] ?? "");
            }
        }



        private EnvironmentSecret()
        {

        }

        public static EnvironmentSecret Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
