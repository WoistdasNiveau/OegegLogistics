using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Mvvm.Navigation;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

[ViewFor<CreateVehicleWindowViewModel>]
public partial class CreateVehicleWindow : Window
{
    public CreateVehicleWindow(CreateVehicleWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}