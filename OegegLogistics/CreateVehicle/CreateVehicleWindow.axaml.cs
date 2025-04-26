using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleWindow : Window
{
    public CreateVehicleWindow()
    {
        InitializeComponent();
    }

    public static Window Create(UserControl control)
    {
        CreateVehicleWindow window = new();
        
        Grid.SetRow(control, 0);
        window.root.Children.Add(control);
        
        return window;
    }
}