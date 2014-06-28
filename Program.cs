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
                ImportedExtensions = new[] { typeof(Program).Assembly }
            };

            using (var host = new OrchardAppHost(settings))
            {
                host.Startup();

                var run = true;

                Console.CancelKeyPress += (sender, e) => run = false;

                while (run)
                {
                    Console.WriteLine("Cycle");

                    host.RunInTransaction(scope =>
                    {
                        scope.Resolve<ILoggerService>().Error("Test log entry.");
                        Console.WriteLine(scope.Resolve<IClock>().UtcNow.ToString());
                    });

                    host.Run<ISiteService, ShellSettings>((siteService, shellSettings) =>
                        {
                            Console.WriteLine(siteService.GetSiteSettings().SiteName);
                            Console.WriteLine(shellSettings.Name);
                        });

                    Console.WriteLine();

                    System.Threading.Thread.Sleep(1500);
                }
            }
        }
    }
}
