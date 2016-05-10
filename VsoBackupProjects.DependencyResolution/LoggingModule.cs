using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsoBackupProjects.LoggingInterface;

namespace VsoBackupProjects.DependencyResolution
{
    public class LoggingModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            ILoggingService logger = LoggingService.LoggingService.GetLoggingService();
            Bind<ILoggingService>().ToConstant(logger);
        }
    }
}
