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
using Orchard.Setup.Services;

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
                ImportedExtensions = new[] { typeof(Program).Assembly, typeof(IOrchardAppHost).Assembly },
                DefaultShellFeatureStates = new[] { new DefaultShellFeatureState { ShellName = ShellSettings.DefaultName, EnabledFeatures = new[] { "Lombiq.OrchardAppHost", "Lombiq.OrchardAppHost.Sample.ShellEvents" } } },
                DisableConfiguratonCaches = true
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

                // We can even run the setup on a new shell. A reference to Orchard.Setup is needed.
                // The setup shouldn't run in a transaction.
                //host.Run<ISetupService, ShellSettings>((setupService, shellSettings) =>
                //{
                //    Console.WriteLine("Running setup for the following shell: " + shellSettings.Name);
                //    setupService.Setup(new SetupContext
                //        {
                //            SiteName = "Test",
                //            AdminUsername = "admin",
                //            AdminPassword = "password",
                //            DatabaseProvider = "SqlCe",
                //            Recipe = "Default"
                //        });

                //    Console.WriteLine("Setup done");
                //}, ShellSettings.DefaultName, false);

                //// After setup everything else should run in a separate scope.
                //host.Run<ISiteService, ShellSettings>((siteService, shellSettings) =>
                //{
                //    Console.WriteLine(siteService.GetSiteSettings().SiteName);
                //    Console.WriteLine(shellSettings.Name);
                //});

                while (run)
                {
                    Console.WriteLine("Cycle");

                    host.Run<ILoggerService, IClock>((logger, clock) =>
                    {
                        logger.Error("Test log entry.");
                        Console.WriteLine(clock.UtcNow.ToString());
                    });

                    // Another overload of Run() for simple transaction handling and for using the work context scope directly.
                    host.RunInTransaction(scope =>
                    {
                        Console.WriteLine(scope.Resolve<ISiteService>().GetSiteSettings().SiteName);
                        Console.WriteLine(scope.Resolve<ShellSettings>().Name);
                    });

                    Console.WriteLine();

                    System.Threading.Thread.Sleep(1500);
                }
            }
        }
    }
}
