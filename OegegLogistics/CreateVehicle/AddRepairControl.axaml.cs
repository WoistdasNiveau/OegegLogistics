using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.CreateVehicle;

public class AddRepairControl : TemplatedControl
{
    #region StyledProperties

    public static readonly StyledProperty<string> RepairTypeProperty = AvaloniaProperty.Register<AddRepairControl, string>(
        nameof(RepairType));

    public string RepairType
    {
        get => GetValue(RepairTypeProperty);
        set => SetValue(RepairTypeProperty, value);
    }

    public static readonly StyledProperty<int?> RepairTypeNumberProperty = AvaloniaProperty.Register<AddRepairControl, int?>(
        nameof(RepairTypeNumber));

    public int? RepairTypeNumber
    {
        get => GetValue(RepairTypeNumberProperty);
        set => SetValue(RepairTypeNumberProperty, value);
    }

    public static readonly StyledProperty<string> RepairTypeNameProperty = AvaloniaProperty.Register<AddRepairControl, string>(
        nameof(RepairTypeName));

    public string RepairTypeName
    {
        get => GetValue(RepairTypeNameProperty);
        set => SetValue(RepairTypeNameProperty, value);
    }

    public static readonly StyledProperty<float> KmLimitProperty = AvaloniaProperty.Register<AddRepairControl, float>(
        nameof(KmLimit));

    public float KmLimit
    {
        get => GetValue(KmLimitProperty);
        set => SetValue(KmLimitProperty, value);
    }

    public static readonly StyledProperty<float> ToleranceProperty = AvaloniaProperty.Register<AddRepairControl, float>(
        nameof(Tolerance));

    public float Tolerance
    {
        get => GetValue(ToleranceProperty);
        set => SetValue(ToleranceProperty, value);
    }

    public static readonly StyledProperty<ToleranceType> ToleranceTypeProperty = AvaloniaProperty.Register<AddRepairControl, ToleranceType>(
        nameof(ToleranceType));

    public ToleranceType ToleranceType
    {
        get => GetValue(ToleranceTypeProperty);
        set => SetValue(ToleranceTypeProperty, value);
    }

    #endregion
    
    // == private fields ==
    private readonly List<ToleranceType> toleranceTypes = Enum.GetValues<ToleranceType>().ToList();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        ComboBox toleranceBox = e.NameScope.Find<ComboBox>("toleranceTypeBox");
        toleranceBox.ItemsSource = toleranceTypes;
        toleranceBox.SelectedItem = toleranceTypes.First(t => t == ToleranceType.Km);
    }
}