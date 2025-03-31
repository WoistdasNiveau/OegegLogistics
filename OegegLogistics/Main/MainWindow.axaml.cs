using Avalonia.Controls;
using Avalonia.Input;

namespace OegegLogistics.Main;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void InputElement_OnTapped(object? sender, TappedEventArgs e)
    {
        (DataContext as MainWindowViewModel).OpenAddVehicleWindow();
    }
}