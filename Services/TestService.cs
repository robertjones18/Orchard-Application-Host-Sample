using Orchard;

namespace Lombiq.OrchardAppHost.Sample.Services
{
    // Dependencies get registered in the usual way.
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
