using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace JB.VersionManager
{
    class Program
    {
        internal static string _assemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        const string versionFile = "AssemblyInfoVersion.cs";

        /// <summary>
        /// Get the full path of the AssemblyInfoVersion.cs file to update the version.
        /// </summary>
        internal static string _assemblyVersionFile
        {
            get
            {
                return _assemblyDirectory + "\\" + versionFile;
            }
        }

        //Setup Defaults for File to Update and what part.
        // File to update.
        internal static string _versionFile = _assemblyVersionFile;
        /// <summary>
        /// Indicate which part of the version number to update M.m.B.R  i.e.  0,1,2,3 or -1 to use the enviornment variable.
        /// </summary>
        internal static int _startingVersionNumber = 3;

       

        /// <summary>
        /// This function will increment versions. Builds are counted from 1 to 9999, Minor: from 1 to 99
        /// </summary>
        static int Main(string[] args)
        {
            IdentifyVersionFileAndVersionChangeRequired(args);
            if (!File.Exists(_versionFile))
            {
                EventLog evlog = new EventLog("Application", Environment.MachineName, AppDomain.CurrentDomain.FriendlyName);
                evlog.WriteEntry(String.Format("File [{0}] not found", _versionFile), EventLogEntryType.Error);
                Console.Write("File [{0}] not found", _versionFile);
                return -1;
            }

            try
            {
                String[] contents = File.ReadAllLines(_versionFile, Encoding.UTF8);
                for (int x = 0; x < contents.Length; x++)
                {
                    // Skip lines that don't contain a version that needs to be changed.
                    if (!contents[x].Contains("AssemblyFileVersion") &&
                        !contents[x].Contains("AssemblyInformationalVersion") &&
                        !contents[x].Contains("<version>")
                        ) { continue; }

                    // Change the version - the algorithm works for assembly files and nuspec files.
                    string newVersion;
                    contents[x] = ChangeVersion(contents[x], out newVersion);
                    Console.WriteLine(contents[x]);
                    
                }
                File.WriteAllLines(_versionFile, contents, Encoding.UTF8); // save file
            }
            catch (Exception e)
            {
                TextWriter errWriter = Console.Error;
                EventLog evlog = new EventLog("Application", Environment.MachineName, AppDomain.CurrentDomain.FriendlyName);
                errWriter.WriteLine("(Reading File) Exception thrown: " + e);
                evlog.WriteEntry("(Reading File) Exception thrown: " + e.Message, EventLogEntryType.Error);
                return -2;
            }

            return 0;
        }

        /// <summary>
        /// check input and fill missing arguments
        /// </summary>
        /// <param name="args">command line input arguments</param>
        private static void IdentifyVersionFileAndVersionChangeRequired(string[] args)
        {
            // Setup defaults.
            _versionFile = _assemblyVersionFile;

            if (args.Count() > 0)
            {
                _versionFile = args[0]; // Full path required - will be a nuspec
                _startingVersionNumber = 2; // Nuspec has M.m.B only.
            }
            if (args.Count() > 1)
            {
                // Indicate which version part to change.
                switch (args[1])
                {
                    case "-P":
                        {
                            var file = _versionFile.Split(new string[] { "nuspec" },StringSplitOptions.RemoveEmptyEntries )[0];
                            var version = GetExistingVersion(_versionFile);
                            var nuget = $@"@echo off
""C:\Program Files (x86)\NuGet\nuget.exe"" push {file}{version}.nupkg  oy2h35copt64yow64hjqdyvliesws4burdeurmojpmpo4i -Source https://api.nuget.org/v3/index.json";

                            Console.WriteLine($"{nuget}");
                            Environment.Exit(0);
                            break;
                        }
                    case "-M": _startingVersionNumber = 0; break;
                    case "-m": _startingVersionNumber = 1; break;
                    case "-B": _startingVersionNumber = 2; break;
                    default: _startingVersionNumber = 3; break; // default number to increment at is Release
                }
            }
        }

        /// <summary>
        /// Transforms version string into <seealso cref="System.Int32"/> increment numbers and return back the new numbers as a string
        /// </summary>
        /// <param name="versionLineToChange">Current version number as a <see cref="System.String"/></param>
        /// <returns><see cref="System.String"/> with the new version number</returns>
        private static String ChangeVersion(String versionLineToChange , out string newVersion)
        {
            // Nuspec <version>1.2.3</version>
            // Default [assembly: AssemblyFileVersion("1.4.3.15")] 

            versionLineToChange = versionLineToChange.Replace('*', '0');  // some files have * instead of build figure

            String sNums = Regex.Replace(versionLineToChange, @"[^\d\.]", ""); // get version numbers i.e. remove everything that is not a digit or a dot

            // Get individual numbers as array elements of int.
            int[] v = Array.ConvertAll<String, Int32>(sNums.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries), e => Convert.ToInt32(e));

            //Increment elements in the array - inc more major element if least one reaches its max value.
            Increment(ref v, _startingVersionNumber);

            newVersion = String.Join(".", Array.ConvertAll<Int32, String>(v, e => e.ToString()));
            return versionLineToChange.Replace(sNums, newVersion); // replace old with new version
        }


        private static string CurrentVersion(String versionLineToChange)
        {
            // Nuspec <version>1.2.3</version>
            // Default [assembly: AssemblyFileVersion("1.4.3.15")] 

            versionLineToChange = versionLineToChange.Replace('*', '0');  // some files have * instead of build figure

            string sNums = Regex.Replace(versionLineToChange, @"[^\d\.]", ""); // get version numbers i.e. remove everything that is not a digit or a dot

            return sNums;

       }





        /// <summary>
        /// Recursively increment version numbers starting from right to left.
        /// </summary>
        /// <param name="ver">array with versions. Zero indexed is Major. Max indexed (3) is Release</param>
        /// <param name="idx">Starting number - from bigger to smaller</param>
        private static void Increment(ref int[] ver, int idx)
        {
            int maxVal = Convert.ToInt32(new String('9', idx + 1));
            try
            {
                if (ver[idx] + 1 > maxVal)
                {
                    Increment(ref ver, idx - 1);
                    ver[idx] = 1;
                    return;
                }
                ver[idx]++;
            }
            catch (Exception e)
            {
                try
                {
                    //          EventLog evlog = new EventLog("Application", Environment.MachineName, AppDomain.CurrentDomain.FriendlyName);
                    //          evlog.WriteEntry("Exception thrown: " + e.Message, EventLogEntryType.Error);
                    (new EventLog("Application", Environment.MachineName, AppDomain.CurrentDomain.FriendlyName)).WriteEntry("Exception thrown: " + e.Message, EventLogEntryType.Error);
                }
                catch  { }
                return;
            }
        }

        static string GetExistingVersion(string file)
        {
            if (!File.Exists(file))
            {
                EventLog evlog = new EventLog("Application", Environment.MachineName, AppDomain.CurrentDomain.FriendlyName);
                evlog.WriteEntry(String.Format("File [{0}] not found", file), EventLogEntryType.Error);
                Console.Write("File [{0}] not found", file);
                Environment.Exit(-1);
                return string.Empty;
            }

            try
            {
                String[] contents = File.ReadAllLines(file, Encoding.UTF8);
                for (int x = 0; x < contents.Length; x++)
                {
                    // Skip lines that don't contain a version that needs to be changed.
                    if (!contents[x].Contains("AssemblyFileVersion") &&
                        !contents[x].Contains("AssemblyInformationalVersion") &&
                        !contents[x].Contains("<version>")
                        ) { continue; }

                    // Change the version - the algorithm works for assembly files and nuspec files.
                    return CurrentVersion(contents[x]);

                }
            }
            catch (Exception e)
            {
                TextWriter errWriter = Console.Error;
                EventLog evlog = new EventLog("Application", Environment.MachineName, AppDomain.CurrentDomain.FriendlyName);
                errWriter.WriteLine("(Reading File) Exception thrown: " + e);
                evlog.WriteEntry("(Reading File) Exception thrown: " + e.Message, EventLogEntryType.Error);
                Environment.Exit(-2);

                return string.Empty;
            }

            return string.Empty;
        }


    }
}
