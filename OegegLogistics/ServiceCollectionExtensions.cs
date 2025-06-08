using Microsoft.Extensions.DependencyInjection;
using Mvvm.Navigation;
using OegegLogistics.CreateVehicle;
using OegegLogistics.Main;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Shared.Services;
using OegegLogistics.Shared.Windows;
using OegegLogistics.Vehicles;

namespace OegegLogistics
{

    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWindows(this IServiceCollection services)
        {
            services.AddTransient<BaseWindow>();
            services.AddTransient<CreateVehicleWindow>();

            return services;
        }

        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<MainView>();
            services.AddTransient<VehiclesView>();
            services.AddTransient<SelectVehicleTypeView>();
            services.AddTransient<SetCurrentKilometersView>();
            services.AddTransient<AddRepairsView>();
            services.AddTransient<EditRepairDialog>();

            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddTransient<VehiclesViewModel>();
            services.AddTransient<CreateVehicleWindowViewModel>();
            services.AddTransient<SetCurrentKilometersViewModel>();
            services.AddTransient<SelectVehicleTypeViewModel>();
            services.AddTransient<AddRepairsViewModel>();

            return services;
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<NavigationService>();
            services.AddTransient<JsonService>();

            return services;
        }

        public static void AddNavigation(this IServiceCollection collection)
        {
            collection.AddSingleton<Navigator<BaseViewModel>>();
        }
        
        public static void AddModels(this IServiceCollection collection)
        {
            collection.AddSingleton<CreateVehicleData>();
        }
    }
}

    # region copiedFromNotCreatedFile

    namespace Mvvm.Navigation
    {
        public static partial class ServiceCollectionExtensions
        {
            static partial void AddViewsAndViewModels(
                global::Microsoft.Extensions.DependencyInjection.IServiceCollection services);

            static partial void AddMappedViewsAndViewModels(
                global::Microsoft.Extensions.DependencyInjection.IServiceCollection services);

            public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddMvvmNavigation(
                this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
            {
                services = services ?? throw new global::System.ArgumentNullException(nameof(services));

                _ = services
                    .AddScoped<global::Mvvm.Navigation.Navigator<global::CommunityToolkit.Mvvm.ComponentModel.ObservableObject>>();

                AddMappedViewsAndViewModels(services);
                AddViewsAndViewModels(services);

                return services;
            }
        }
    }

    #endregion
