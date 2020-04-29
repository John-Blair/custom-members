using System;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Web;
//using System.Web.Mvc;
using System.Text;
using System.Web.Hosting;
using System.Web.UI;
using System.Diagnostics;
using Umbraco.Core.Configuration;
using Umbraco.Core;
using System.Reflection;

namespace JB.Helpers
{
    public class EnvironmentService
    {

        public static string Environment()
        {
            var sb = new StringBuilder();
            sb.AppendHtmlLine($"Environment:{Stage}");
            try
            {

            sb.AppendHtmlLine($"Domain: {Domain}");
            sb.AppendHtmlLine($"Url: {Url}");
            sb.AppendHtmlLine($"Machine Name: {System.Environment.MachineName}");
                sb.AppendHtmlLine($"Server IP Address: {ServerIPAddress()}");
                sb.AppendHtmlLine($"Client IP Address: {ClientIPAddress()}");
                sb.AppendHtmlLine($"Assembly Version: {AssemblyVersion}");
                sb.AppendHtmlLine($"File Version: {FileVersion()}");
                sb.AppendHtmlLine($"Umbraco CMS Version: {UmbracoVersion.SemanticVersion.ToSemanticString()}");
            sb.AppendHtmlLine($"Application Physical Path: {HostingEnvironment.ApplicationPhysicalPath}");
            sb.AppendHtmlLine($"ApplicationID: {HostingEnvironment.ApplicationID}");
            sb.AppendHtmlLine($"Environment Variable %temp%: {System.Environment.ExpandEnvironmentVariables("%temp%")}");
            sb.AppendHtmlLine(String.Empty);
            }
            catch (Exception ex) {
                sb.AppendHtmlLine($"Exception:{ex.Message}");
            };
            return sb.ToString();
        }

        public static string Domain => HttpContext.Current?.Request?.Url?.Host ?? "Unknown";
        public static string Url => HttpContext.Current?.Request?.Url?.ToString() ?? "Unknown";

        public static string Stage => System.Configuration.ConfigurationManager.AppSettings["Environment.Stage"] ?? String.Empty;

        public static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string FileVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            return version;
        }


        public static string ServerIPAddress()
        {
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
           // IPAddress ipAddress = ipHostInfo.AddressList[0];

            return HttpContext.Current?.Request?.ServerVariables["LOCAL_ADDR"] ?? "Unknown";
        }

        public static string ClientIPAddress()
        {
            string ipList = HttpContext.Current?.Request?.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                // Remove port number - if present.
                return ipList.Split(',')[0].Split(':')[0];
            }

            return HttpContext.Current?.Request?.ServerVariables["REMOTE_ADDR"] ?? string.Empty;
        }

    }
}