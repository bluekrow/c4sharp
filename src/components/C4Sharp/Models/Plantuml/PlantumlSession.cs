using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Versioning;
using System.Text;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// Session
    /// </summary>
    public class PlantumlSession : IDisposable
    {
        public bool StandardLibraryBaseUrl { get; private set; }
        public bool GenerateDiagramImages { get; private set; }
        private string FilePath { get; }
        private ProcessStartInfo ProcessInfo { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PlantumlSession()
        {
            FilePath = PlantumlResource.Load();
            ProcessInfo = new ProcessStartInfo
            {
                FileName = "java",
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };
        }

        /// <summary>
        /// Constructor with credentials to impersonate
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        [SupportedOSPlatform("windows")]
        public PlantumlSession(string username, string password) : this()
        {
            if (!OperatingSystem.IsWindows())
            {
                return;
            }

            ProcessInfo.UserName = username;
            ProcessInfo.PasswordInClearText = password;
        }

        /// <summary>
        /// The C4Sharp has embedded the current version of C4-PluntUML.
        /// But, if you want to use the C4-PlantUML up-to-date version from their repo,
        /// use this method
        /// </summary>
        /// <returns>PlantumlSession instance</returns>
        public PlantumlSession UseStandardLibraryBaseUrl()
        {
            StandardLibraryBaseUrl = true;
            return this;
        }

        /// <summary>
        /// The C4Sharp will generate *.puml files of your diagram.
        /// Also, you could save the *.png files using this method
        /// </summary>
        /// <returns></returns>
        public PlantumlSession UseDiagramImageBuilder()
        {
            GenerateDiagramImages = true;
            return this;
        }

        /// <summary>
        /// Execute plantuml.jar
        /// </summary>
        /// <param name="path">puml files path</param>
        /// <param name="processWholeDirectory">process all *.puml files</param>
        /// <exception cref="PlantumlException"></exception>
        internal void Execute(string path, bool processWholeDirectory)
        {
            var directory = processWholeDirectory
                ? path
                : new FileInfo(path)?.Directory?.FullName;

            try
            {
                if (string.IsNullOrEmpty(directory))
                {
                    throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.");
                }

                var results = new StringBuilder();

                var jar = StandardLibraryBaseUrl
                    ? $"-jar {FilePath} -verbose -o \"{directory}\" -charset UTF-8"
                    : $"-jar {FilePath} -DRELATIVE_INCLUDE=\".\" -verbose -o \"{directory}\" -charset UTF-8";

                ProcessInfo.Arguments = $"{jar} {path}";
                ProcessInfo.RedirectStandardOutput = true;
                ProcessInfo.StandardOutputEncoding = Encoding.UTF8;

                var process = new Process { StartInfo = ProcessInfo };

                process.OutputDataReceived += (_, args) => { results.AppendLine(args.Data); };

                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.", e);
            }
        }

        /// <summary>
        /// Clear Plantuml Resource
        /// </summary>
        public void Dispose()
        {
            PlantumlResource.Clear(FilePath);
        }
    }
}