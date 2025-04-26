using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleWindow : Window
{
    public CreateVehicleWindow(CreateVehicleWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}