using NUnit.Framework;
using VsoBackupProjects.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using VsoBackupProjects.DependencyResolution;
using VsoBackupProjects.LoggingInterface;

namespace VsoBackupProjects.LoggingService.Tests
{
    [TestFixture()]
    public class LoggingServiceTests
    {
        // Ninject kernel
        private IKernel _ninjectKernel;

        public  LoggingServiceTests()
        {
            // Init Ninject kernel
            _ninjectKernel = new StandardKernel(new LoggingModule());
        }
        [Test()]
        public void GetLoggingServiceTest()
        {
            // Arrange

            var logger = _ninjectKernel.Get<ILoggingService>();

            // Act

            try
            {
                logger.Trace("Sample trace message");
                logger.Debug("Sample debug message");
                logger.Info("Sample informational message");
                logger.Warn("Sample warning message");
                logger.Error("Sample error message");
                logger.Fatal("Sample fatal error message");

                throw new Exception("Tried to divide by zero", new DivideByZeroException());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            // Assert

            Assert.Pass("Check Debug output for log message");
        }
    }
}