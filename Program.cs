using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lombiq.OrchardAppHost.Configuration;
using Orchard.Environment.Configuration;
using Orchard.Logging;
using Orchard.Services;
using Orchard.Settings;
using Lombiq.OrchardAppHost.Services;
using Orchard.Environment.Descriptor.Models;

namespace Lombiq.OrchardAppHost.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new AppHostSettings
            {
                AppDataFolderPath = "~/App_Data",
                ModuleFolderPaths = new[] { @"E:\Projects\Munka\Lombiq\Orchard Dev Hg\src\Orchard.Web\Modules" },
                CoreModuleFolderPaths = new[] { @"E:\Projects\Munka\Lombiq\Orchard Dev Hg\src\Orchard.Web\Core" },
                ThemeFolderPaths = new[] { @"E:\Projects\Munka\Lombiq\Orchard Dev Hg\src\Orchard.Web\Themes" },
                ImportedExtensions = new[] { new ShellExtensions { ShellName = ShellSettings.DefaultName, Extensions = new[] { typeof(Program).Assembly } } }
            };

            //var enabledFeatures = new[] { new ShellFeature { Name = "Settings" } };
            //using (var host = OrchardAppHostFactory.StartTransientHost(settings, null, enabledFeatures))
            //{
            //    host.Run<ILoggerService, IClock>((logger, clock) =>
            //    {
            //        logger.Error("Test log entry.");
            //        Console.WriteLine(clock.UtcNow.ToString());
            //    });
            //}

            //Console.ReadKey();

            using (var host = OrchardAppHostFactory.StartHost(settings))
            {
                var run = true;

                Console.CancelKeyPress += (sender, e) => run = false;

                while (run)
                {
                    Console.WriteLine("Cycle");

                    host.RunInTransaction(scope =>
                    {
                        Console.WriteLine(scope.Resolve<ISiteService>().GetSiteSettings().SiteName);
                        Console.WriteLine(scope.Resolve < ShellSettings>().Name);
                    });

                    host.Run<ILoggerService, IClock>((logger, clock) =>
                    {
                        logger.Error("Test log entry.");
                        Console.WriteLine(clock.UtcNow.ToString());
                    });

                    Console.WriteLine();

                    System.Threading.Thread.Sleep(1500);
                }
            }
        }
    }
}
