using System;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Lombiq.OrchardAppHost.Configuration;
using Lombiq.OrchardAppHost.Sample.Samples;
using Orchard.Environment.Configuration;

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
                        // A random App_Data folder so the setup sample can run from a fresh state.
                        AppDataFolderPath = "~/App_Data" + new Random().Next(),
                        ModuleFolderPaths = new[] { @"~/../../../Orchard.Web/Modules" },
                        CoreModuleFolderPaths = new[] { @"~/../../../Orchard.Web/Core" },
                        ThemeFolderPaths = new[] { @"~/../../../Orchard.Web/Themes" },
                        ImportedExtensions = new[] { typeof(Program).Assembly },
                        DefaultShellFeatureStates = new[]
                        {
                            new DefaultShellFeatureState
                            {
                                ShellName = ShellSettings.DefaultName,
                                EnabledFeatures = new[] { "Lombiq.OrchardAppHost.Sample", "Lombiq.OrchardAppHost.Sample.ShellEvents" }
                            }
                        },
                        DisableConfiguratonCaches = true,
                        DisableExtensionMonitoring = true,
                        // Configuring the logging of SQL queries (see: http://weblogs.asp.net/bleroy/logging-sql-queries-in-orchard).
                        // This needs a reference to the Log4Net assembly.
                        Log4NetConfigurator = loggerRespository => ((Logger)loggerRespository.GetLogger("NHibernate.SQL")).Level = Level.Debug
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
