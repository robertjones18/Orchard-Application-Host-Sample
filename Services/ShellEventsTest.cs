using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.Logging;

namespace Lombiq.OrchardAppHost.Sample.Services
{
    // Shell events work as usual, even from (enabled) sub-features.
    [OrchardFeature("Lombiq.OrchardAppHost.Sample.ShellEvents")]
    public class ShellEventsTest : IOrchardShellEvents
    {
        public ILogger Logger { get; set; }


        public void Activated()
        {
            Logger.Information("Shell events work");
        }

        public void Terminating()
        {
        }
    }
}
