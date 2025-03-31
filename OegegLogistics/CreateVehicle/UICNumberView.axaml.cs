using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Navigation;

namespace OegegLogistics.CreateVehicle;

[ViewFor<CreateVehicleViewModel>]
public partial class UicNumberView : UserControl
{
    public UicNumberView(CreateVehicleViewModel createVehicleViewModel)
    {
        InitializeComponent();
        DataContext = createVehicleViewModel;
    }
}