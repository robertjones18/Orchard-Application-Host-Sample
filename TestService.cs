using Orchard;

namespace Lombiq.OrchardAppHost.Sample
{
    public interface ITestService : IDependency
    {
        void Test();
    }


    public class TestService : ITestService
    {
        public void Test()
        {
        }
    }
}
