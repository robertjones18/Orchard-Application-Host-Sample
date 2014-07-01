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
using Lombiq.OrchardAppHost.Services;
using Orchard.Environment.Descriptor.Models;
using Orchard.Setup.Services;
using Orchard;
using Orchard.Mvc;
using Lombiq.OrchardAppHost.Sample.Samples;

namespace Lombiq.OrchardAppHost.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
                {
                    // There are a lot of settings you can use.
                    var settings = new AppHostSettings
                    {
                        AppDataFolderPath = "~/App_Data" + new Random().Next(), // A random App_Data folder so the setup sample can run from a fresh state.
                        ModuleFolderPaths = new[] { @"E:\Projects\Munka\Lombiq\Orchard Dev Hg\src\Orchard.Web\Modules" },
                        CoreModuleFolderPaths = new[] { @"E:\Projects\Munka\Lombiq\Orchard Dev Hg\src\Orchard.Web\Core" },
                        ThemeFolderPaths = new[] { @"E:\Projects\Munka\Lombiq\Orchard Dev Hg\src\Orchard.Web\Themes" },
                        ImportedExtensions = new[] { typeof(Program).Assembly, typeof(IOrchardAppHost).Assembly },
                        DefaultShellFeatureStates = new[]
                        {
                            new DefaultShellFeatureState
                            {
                                ShellName = ShellSettings.DefaultName,
                                EnabledFeatures = new[] { "Lombiq.OrchardAppHost.Sample", "Lombiq.OrchardAppHost.Sample.ShellEvents" }
                            }
                        },
                        DisableConfiguratonCaches = true
                    };


                    // Samples are being run below, check out the static classes.

                    await SetupSample.RunSample(settings);
                    Console.WriteLine();
                    await LoopSample.RunSample(settings);
                    Console.WriteLine();
                    await TransientHostSample.RunSample(settings);
                    

                    Console.ReadKey();
                }).Wait(); // This is a workaround just to be able to run all this from inside a console app.
        }
    }
}
