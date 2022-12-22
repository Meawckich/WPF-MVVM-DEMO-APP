using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestApplication.Views;
using System.Windows;
using TestApplication.Models;
using TestApplication.ViewModels;
using TestApplication.DbContext;
using System.IO;
using System.IO.Enumeration;
using System.Security.Cryptography;
using System.Text;
using System;
using TestApplication.Excel;
using TestApplication.DataDTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestApplication.Stores;

namespace TestApplication
{
    public partial class App : Application
    {
        private readonly IHost _apphost;
        private const string? FILENAME = "TestDb.db";

        public App()
        {
            _apphost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<LoginViewModel>();
                    services.AddTransient<SqLiteDbContext>();
                    services.AddTransient<DataViewModel>();
                    services.AddTransient<ExcelReader>();
                    services.AddTransient<ListViewDTO>();
                    services.AddTransient<MainViewModel>();
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();

            if (File.Exists(FILENAME))
                File.Delete(FILENAME);

            using (var db = _apphost.Services.GetRequiredService<SqLiteDbContext>())
            {
                _apphost.Services.GetRequiredService<SqLiteDbContext>().CreateDb();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _apphost!.StartAsync();
            _apphost!.Services.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
         {
            await _apphost!.StopAsync();
            base.OnExit(e);
        }
    }
}
