using System;
using System.Threading.Tasks;
using Lombiq.OrchardAppHost.Configuration;
using Lombiq.OrchardAppHost.Sample.Services;
using Orchard.Logging;
using Orchard.Services;

namespace Lombiq.OrchardAppHost.Sample.Samples
{
    /// <summary>
    /// Demonstrates how to use an extremely light-weight transient host when we don't need the persistence layer.
    /// </summary>
    static class TransientHostSample
    {
        public static async Task RunSample(AppHostSettings settings)
        {
            Console.WriteLine("=== Transient host sample starts === ");

            using (var host = await OrchardAppHostFactory.StartTransientHost(settings, null, null))
            {
                await host.Run<ITestService, ILoggerService, IClock>((testService, logger, clock) => Task.Run(() =>
                    {
                        testService.Test(); // Custom dependencies from imported and enabled extensions work too.
                        logger.Error("Test log entry from transient shell.");
                        Console.WriteLine(clock.UtcNow.ToString());
                    }));

                // You can even run such "getters" to just fetch something from Orchard.
                var utcNow = await host.RunGet(scope => Task.Run(() => scope.Resolve<IClock>().UtcNow), wrapInTransaction: false);
            }

            Console.WriteLine("=== Transient host sample ended === ");
        }
    }
}
