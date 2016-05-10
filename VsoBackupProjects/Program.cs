using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NLog;
using VsoBackupProjects.Configuration;
using VsoBackupProjects.DependencyResolution;
using VsoBackupProjects.LoggingInterface;
using VsoBackupProjects.VisualStudioOnline;
using VsoBackupProjects.VisualStudioOnline.Models;

namespace VsoBackupProjects
{
    /// <summary>
    ///   VsoUserName="username visual studio online"
    ///   VsoPassword="encryptedpassword - use VsoBackupProjects.EncryptPasswordConsole"
    ///   VsoProjects="https://xxxxxxxxxxxxxxxx.visualstudio.com/DefaultCollection/_apis/projects"
    ///   VsoVersionControlUrl="https://xxxxxxxxxxxxxxxx.visualstudio.com/xxxxxxxxxxxx_yourguid_xxxxxxxxxxxxxxxx/_api/_versioncontrol"
    ///   BackupFolder="c:\backupVso"
    /// </summary>
    class Program
    {
        private static VisualStudioOnline.VsoRestApiService _vsoRestApiService;
        private static IKernel _ninjectKernel;

        static void Main(string[] args)
        {

            // Get Configuration
            var configuration = (VsoConfiguration)ConfigurationManager.GetSection("VsoConfiguration");
            // Init Ninject kernel
            _ninjectKernel = new StandardKernel(new LoggingModule());
            var logger = _ninjectKernel.Get<ILoggingService>();

            VsoBackupController backupController = new VsoBackupController(configuration,logger);

            logger.Info("Process finished.");
            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();

        }
    }
}
