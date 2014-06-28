using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard.Environment;
using Orchard.Logging;

namespace Lombiq.OrchardAppHost.Sample
{
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
