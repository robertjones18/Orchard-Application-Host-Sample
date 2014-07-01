using System;
using System.Threading.Tasks;
using Lombiq.OrchardAppHost.Configuration;
using Orchard.Environment.Configuration;
using Orchard.Settings;
using Orchard.Setup.Services;

namespace Lombiq.OrchardAppHost.Sample.Samples
{
    /// <summary>
    /// Demonstrates that the setup can also be run inside the App Host.
    /// </summary>
    static class SetupSample
    {
        public static async Task RunSample(AppHostSettings settings)
        {
            Console.WriteLine("=== Setup sample starts === ");

            using (var host = await OrchardAppHostFactory.StartHost(settings))
            {
                // We can even run the setup on a new shell. A project reference to Orchard.Setup is needed.
                // The setup shouldn't run in a transaction.
                await host.Run<ISetupService, ShellSettings>((setupService, shellSettings) => Task.Run(() =>
                    {
                        Console.WriteLine("Running setup for the following shell: " + shellSettings.Name);
                        setupService.Setup(new SetupContext
                            {
                                SiteName = "Test",
                                AdminUsername = "admin",
                                AdminPassword = "password",
                                DatabaseProvider = "SqlCe",
                                Recipe = "Default"
                            });

                        Console.WriteLine("Setup done");
                    }), wrapInTransaction: false);

                // After setup everything else should be run in a separate scope.
                await host.Run<ISiteService, ShellSettings>((siteService, shellSettings) => Task.Run(() =>
                    {
                        Console.WriteLine(siteService.GetSiteSettings().SiteName);
                        Console.WriteLine(shellSettings.Name);
                    }));
            }

            Console.WriteLine("=== Setup host sample ended === ");
        }
    }
}
