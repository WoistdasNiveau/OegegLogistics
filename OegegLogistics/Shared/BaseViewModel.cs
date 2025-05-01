using CommunityToolkit.Mvvm.ComponentModel;
using Mvvm.Navigation;
using OegegLogistics.Navigation;

namespace OegegLogistics.Shared;

public abstract class BaseViewModel : ObservableObject
{
    public NavigationService NavigationService { get; }

    protected BaseViewModel(NavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}