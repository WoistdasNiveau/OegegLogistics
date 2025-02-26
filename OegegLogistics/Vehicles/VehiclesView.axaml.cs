using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OegegLogistics.Vehicles;

public partial class VehiclesView : UserControl
{
    public VehiclesView()
    {
        InitializeComponent();
        DataContext = new VehiclesViewModel();
    }
}