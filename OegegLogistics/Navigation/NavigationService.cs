using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Mvvm.Navigation;
using OegegLogistics.CreateVehicle;
using OegegLogistics.Shared;

namespace OegegLogistics.Navigation;

public class NavigationService
{
    private readonly Navigator<BaseViewModel> _navigator;
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(Navigator<BaseViewModel> navigator, IServiceProvider serviceProvider)
    {
        _navigator = navigator;
        _serviceProvider = serviceProvider;
    }
    
    // == public methods ==
    public async Task NavigateAsync<T>() where T : BaseViewModel
    {
        
    }

    public async Task NavigateNewWindowAsync<T>()
    {
        var s = typeof(T);
        var o = new CreateVehicleViewModel();
        var z = new UicNumberView(new CreateVehicleViewModel());
        T view = _serviceProvider.GetRequiredService<T>();
        Window window = new Window();
        window.Content = view;
        window.Show();
    }
}