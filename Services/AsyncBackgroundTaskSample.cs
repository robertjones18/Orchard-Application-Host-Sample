using System.Threading.Tasks;
using Lombiq.OrchardAppHost.Environment.Tasks;

namespace Lombiq.OrchardAppHost.Sample.Services
{
    // We also have async background tasks that can run even in a transient host. Check out the interface!
    public class AsyncBackgroundTaskSample : IAsyncBackgroundTask
    {
        public Task Sweep()
        {
            return Task.Delay(1500);
        }
    }
}
