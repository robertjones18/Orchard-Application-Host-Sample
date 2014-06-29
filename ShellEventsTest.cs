using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.Logging;

namespace Lombiq.OrchardAppHost.Sample
{
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
