﻿using Microsoft.Extensions.DependencyInjection;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.API;
using TourPlanner.BusinessLayer.ImportExport;
using TourPlanner.DataLayer;
using TourPlanner.DataLayer.Repositories;
using TourPlanner.HelperLayer.Services;
using TourPlanner.PresentationLayer.ViewModels;

namespace TourPlanner.PresentationLayer
{
    public sealed class IoCContainerConfig
    {
        private static IoCContainerConfig instance = null;
        private readonly ServiceProvider _serviceProvider;

        public IoCContainerConfig()
        {
            var services = new ServiceCollection();

            services.AddSingleton<TourRepository>();
            services.AddSingleton<TourLogRepository>();
            services.AddSingleton<FileImporter_Exporter>();
            services.AddSingleton<TourDbContext>();
            services.AddSingleton<DbHandler>();
            services.AddSingleton<DLHandler>();

            services.AddSingleton<APIRequestDirections>();
            services.AddSingleton<BLHandler>();

            services.AddTransient<DialogService>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<AddTourViewModel>();
            services.AddTransient<EditTourViewModel>();
            services.AddTransient<AddTourLogViewModel>();
            services.AddTransient<EditTourLogViewModel>();

            // finish configuration and build the provider
            _serviceProvider = services.BuildServiceProvider();
        }

        public static IoCContainerConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IoCContainerConfig();
                }
                return instance;
            }
        }

        public MainViewModel MainViewModel
            => _serviceProvider.GetService<MainViewModel>();
        public AddTourViewModel AddTourViewModel
            => _serviceProvider.GetService<AddTourViewModel>();
        public EditTourViewModel EditTourViewModel
            => _serviceProvider.GetService<EditTourViewModel>();
        public AddTourLogViewModel AddTourLogViewModel
            => _serviceProvider.GetService<AddTourLogViewModel>();
        public EditTourLogViewModel EditTourLogViewModel
            => _serviceProvider.GetService<EditTourLogViewModel>();
    }
}
