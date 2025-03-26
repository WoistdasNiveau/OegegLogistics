using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Navigation;

namespace OegegLogistics.Vehicles;

[ViewFor<VehiclesViewModel>]
public partial class VehiclesView : UserControl
{
    public VehiclesView(VehiclesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}