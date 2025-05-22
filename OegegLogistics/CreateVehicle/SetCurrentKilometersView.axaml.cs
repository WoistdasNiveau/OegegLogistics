using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Mvvm.Navigation;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.CreateVehicle;

[ViewFor<SetCurrentKilometersViewModel>]
public partial class SetCurrentKilometersView : UserControl
{
    public SetCurrentKilometersView(SetCurrentKilometersViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}