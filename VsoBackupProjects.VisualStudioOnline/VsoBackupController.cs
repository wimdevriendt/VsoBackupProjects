using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VsoBackupProjects.Configuration;
using VsoBackupProjects.LoggingInterface;
using VsoBackupProjects.VisualStudioOnline.Models;

namespace VsoBackupProjects.VisualStudioOnline
{
    public class VsoBackupController
    {
        private readonly ILoggingService _logger;
        public VsoBackupController(VsoConfiguration configuration, ILoggingService logger)
        {
            _logger = logger;

            try
            {
                //check backupfolder
                System.IO.Directory.CreateDirectory(configuration.BackupFolder);

                var vsoRestApiService = new VsoRestApiService(configuration.VsoUserName, configuration.VsoPassword);

                var task = vsoRestApiService.ExecuteRequest<RootObject>(configuration.VsoProjects);
                task.Wait(new TimeSpan(0, 0, 30));
                var result = task.Result;

                var all = result.Value;
                var groupedByTeamProject = all.GroupBy(m => m.Name).ToList();

                foreach (var teamProject in groupedByTeamProject)
                {
                    foreach (var repo in teamProject)
                    {
                        string repoLink = $"{configuration.VsoVersionControlUrl}/itemContentZipped?repositoryId=&path=%24%2F{repo.Name}";

                        try
                        {
                            //Console.WriteLine(repoLink);
                            var sw = new Stopwatch();
                            sw.Start();
                            _logger.Info($"Downloading {repo.Name}");
                            var t = vsoRestApiService.DownloadAsync($"{configuration.VsoVersionControlUrl}/itemContentZipped?repositoryId=&path=%24%2F{repo.Name}", $@"{configuration.BackupFolder}\{repo.Name}_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}.zip");
                            t.Wait();
                            sw.Stop();
                            var ts = sw.Elapsed;
                            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
                            _logger.Info($"Finished downloading {repo.Name}. Duration : {elapsedTime} ");
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
