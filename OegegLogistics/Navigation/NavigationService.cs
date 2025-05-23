using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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
        try
        {
            _navigator.NavigateByType(typeof(T));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task NavigateBackAsync()
    {
        try
        {
            _navigator.NavigateBack();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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

    public async Task ShowDialogAsync<U>(object? dataContext = default) where U : UserControl
    {
        try
        {
            Window window = (TopLevel.GetTopLevel(_navigator.CurrentView as UserControl) as Window)!;
            Window dialog = new Window();
            UserControl control = _serviceProvider.GetRequiredService<U>();
            if(dataContext != null)
                control.DataContext = dataContext;
            dialog.Content = control;
            
            await dialog.ShowDialog(window);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}