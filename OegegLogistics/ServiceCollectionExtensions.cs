using Microsoft.Extensions.DependencyInjection;
using Mvvm.Navigation;
using OegegLogistics.CreateVehicle;
using OegegLogistics.Main;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Vehicles;

namespace OegegLogistics
{

    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<VehiclesView>();
            services.AddTransient<UicNumberView>();

            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<VehiclesViewModel>();
            services.AddTransient<CreateVehicleViewModel>();

            return services;
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<NavigationService>();

            return services;
        }

        public static void AddNavigation(this IServiceCollection collection)
        {
            //collection.AddSingleton<Mvvm.Navigation.Navigation>();
            collection.AddSingleton<Navigator<BaseViewModel>>();
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
