using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Navigation;

namespace OegegLogistics.CreateVehicle;

[ViewFor<AddRepairsViewModel>]
public partial class AddRepairsView : UserControl
{
    public AddRepairsView(AddRepairsViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}