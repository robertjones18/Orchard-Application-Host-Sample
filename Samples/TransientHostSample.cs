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
                await host.Run<ITestService, ILoggerService, IClock>((testService, logger, clock) =>
                    {
                        testService.Test(); // Custom dependencies from imported and enabled extensions work too.
                        logger.Error("Test log entry from transient shell.");
                        Console.WriteLine(clock.UtcNow.ToString());

                        // If there's nothing async int the delegate you can just simply return a completed Task too.
                        return Task.CompletedTask;
                    });

                // You can even run such "getters" to just fetch something from Orchard. Note that you can use
                // Task.FromResult() as well.
                var utcNow = await host.RunGet(scope => Task.FromResult(scope.Resolve<IClock>().UtcNow));
            }

            Console.WriteLine("=== Transient host sample ended === ");
        }
    }
}
