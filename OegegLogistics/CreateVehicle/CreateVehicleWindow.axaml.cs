using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Mvvm.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Shared.Windows;

namespace OegegLogistics.CreateVehicle;

[ViewFor<CreateVehicleWindowViewModel>]
public partial class CreateVehicleWindow : BaseWindow
{
    public CreateVehicleWindow(CreateVehicleWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}