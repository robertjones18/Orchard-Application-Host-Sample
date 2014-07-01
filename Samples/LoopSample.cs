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

namespace Lombiq.OrchardAppHost.Sample.Samples
{
    /// <summary>
    /// Demonstrates how the Orchard App Host can also be run in a loop (e.g. in the main loop of a console app).
    /// </summary>
    static class LoopSample
    {
        public static async Task RunSample(AppHostSettings settings)
        {
            Console.WriteLine("=== Loop sample starts === ");

            using (var host = await OrchardAppHostFactory.StartHost(settings))
            {
                var run = true;

                // Hit Ctrl+C to exit the loop, but not the app (other samples will follow up).
                Console.CancelKeyPress += (sender, e) =>
                    {
                        e.Cancel = true;
                        run = false;
                    };


                while (run)
                {
                    Console.WriteLine("Cycle starts.");


                    await host.Run<ILoggerService, IClock>((logger, clock) => Task.Run(() =>
                        {
                            logger.Error("Test log entry.");
                            Console.WriteLine(clock.UtcNow.ToString());
                        }));

                    // Another overload of Run() for simple transaction handling and for using the work context scope directly.
                    await host.RunInTransaction(async scope =>
                        {
                            Console.WriteLine(scope.Resolve<ISiteService>().GetSiteSettings().SiteName);

                            // Simulating an async call. Because of this the delegate is marked as async (and we don't need to wrap it
                            // into a Task.Run()).
                            await Task.Delay(3000);

                            Console.WriteLine(scope.Resolve<ISiteService>().GetSiteSettings().SiteName);
                            Console.WriteLine(scope.Resolve<ShellSettings>().Name);
                        });


                    Console.WriteLine("Cycle ends.");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("=== Loop sample ended === ");
        }
    }
}
