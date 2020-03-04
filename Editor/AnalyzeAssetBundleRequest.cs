using System.Diagnostics;
using System.IO;


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
            var process = new Process {StartInfo = start};
            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += OutputHandler;
            process.ErrorDataReceived += OutputHandler;
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
        
        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) 
        {
            if (outLine.Data != "") // print if the line is not empty
                UnityEngine.Debug.Log(outLine.Data);
        }
    }
}
