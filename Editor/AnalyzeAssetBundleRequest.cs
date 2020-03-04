using System;
using System.Runtime;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

namespace UnityEditor.AssetBundleAnalyzer
{
    public class AnalyzeAssetBundleRequest
    {
        public void CreateDatabase(string toolsPath, string assetPath, string outputName, bool usePattern, string pattern, bool keepTemp, bool storeRaw, bool debug, bool verbose)
        {
          var arguments = "";
          if (usePattern)
          {
             arguments += (" -p "+ pattern + " ");
          }
          if (keepTemp)
          {
             arguments += " -k ";
          }
          if (storeRaw)
          {
             arguments += " -r ";
          }
          if (debug)
          {
             arguments += " -d ";
          }
          if (verbose)
          {
             arguments += " -v ";
          }
          arguments = arguments + " -o "+ outputName + " \""+ toolsPath + "\" \"" + assetPath + "\"";
          RunPythonScript(arguments);
        }

        private void RunPythonScript(string args)
        {
			var analyzerScriptPath	= Path.GetFullPath("Packages/com.unity.asset-bundle-analyzer/Editor/analyzer.py");

            var start = new ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = string.Format(analyzerScriptPath + " {0}", args);
            start.UseShellExecute = false;
            start.RedirectStandardError = true;
            start.RedirectStandardOutput = true;
            start.RedirectStandardInput = true;

            StartProcess(start);
        }

        private void StartProcess(ProcessStartInfo start)
        {
            using (var process = Process.Start(start))
            {
                UnityEngine.Debug.Log("AssetBundle Analyzer running. Process could take a minute!");
                var output = process.StandardOutput.ReadToEnd();
                if (output != "")
                {
                    UnityEngine.Debug.Log(output);
                }
                else
                {
                    UnityEngine.Debug.Log("Folder scanned, no bundles found.");
                }
            }
        }
    }
}
