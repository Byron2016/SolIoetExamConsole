using Business.Interfaces;
using Business.Repositories;
using ConsoleApp2.Controller;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup  DI
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var attendanceController = serviceProvider.GetService<AttendanceController>();
            attendanceController.Index();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddTransient<AttendanceController>();

            services.AddSingleton<IAttendance, AttendanceRepository>();
        }
    }
}
