using DevGearBox.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevGearBox.Win
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            var services = new ServiceCollection();
            ConfigureServices(services);
            var servicesProvider = services.BuildServiceProvider();

            ServiceProviderHolder.ServiceProvider = servicesProvider;

            ApplicationConfiguration.Initialize();

            Application.Run(new FrmMain());
        }

        public static class ServiceProviderHolder
        {
            public static IServiceProvider ServiceProvider { get; set; }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ToolsServices>();
        }
    }
}