using System.Linq;
using Avalonia.Controls;

namespace OegegLogistics.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        vehicles.ItemsSource = new string[]
                {"cat", "camel", "cow", "chameleon", "mouse", "lion", "zebra" }
            .OrderBy(x => x);
    }
}