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
        _navigator.Navigate<T>();
    }

    public async Task NavigateNewWindowAsync<W, T>() where W : Window where T :UserControl
    {
        try
        {
            W window = _serviceProvider.GetRequiredService<W>();
            window.Show();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}