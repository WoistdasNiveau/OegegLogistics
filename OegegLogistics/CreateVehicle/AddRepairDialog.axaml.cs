using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.CreateVehicle;

public partial class AddRepairDialog : UserControl
{
    public AddRepairDialog()
    {
        InitializeComponent();
        
        List<ToleranceType> toleranceTypes = Enum.GetValues<ToleranceType>().ToList();
        toleranceTypeBox.ItemsSource = toleranceTypes;
        toleranceTypes.First(t => t == ToleranceType.Km);
        
        SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        Border.Width = e.NewSize.Width / 3;
        Border.Height = e.NewSize.Height / 3;
    }
}