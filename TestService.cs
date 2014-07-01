using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
