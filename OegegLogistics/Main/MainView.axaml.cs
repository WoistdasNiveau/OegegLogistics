using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Mvvm.Navigation;
using OegegLogistics.Vehicles;

namespace OegegLogistics.Main;

[ViewFor<MainViewModel>]
public partial class MainView : UserControl
{
    public MainView(MainViewModel mainViewModel, VehiclesView vehiclesView)
    {
        InitializeComponent();
        DataContext = mainViewModel;
        ContentControl.Content = vehiclesView;
    }
    
    private void InputElement_OnTapped(object? sender, TappedEventArgs e)
    {
        (DataContext as MainViewModel).OpenAddVehicleWindow();
    }
}