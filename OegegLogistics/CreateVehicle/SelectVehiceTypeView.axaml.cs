using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;

namespace OegegLogistics.CreateVehicle;

public partial class SelectVehiceTypeView : UserControl
{
    // == private fields ==
    private double _width;
    private double _startX;
    private double _endX;
    public SelectVehiceTypeView()
    {
        InitializeComponent();
        
        SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        TranslateTransform translateTransform = LocoSvg.RenderTransform as TranslateTransform ?? new TranslateTransform();
        LocoSvg.RenderTransform = translateTransform;
        
        _width = LocoSvg.Bounds.Width;
        _startX = -_width -20;
        var grid = this.Content as Grid;
        double col0Width = grid?.ColumnDefinitions[0].ActualWidth ?? Bounds.Width * 0.3;

        _endX = (col0Width - _width) / 2;
        
        translateTransform.X = _startX;
    }

    private async void InputElement_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (LocoSvg.RenderTransform is TranslateTransform transform)
        {
            Animation animation = new Animation()
            {
                Duration = TimeSpan.FromSeconds(1),
                Easing = new CubicEaseOut(),
                FillMode = FillMode.Forward,
                Children =
                {
                    new KeyFrame()
                    {
                        Cue = new Cue(0d),
                        Setters =
                        {
                            new Setter(TranslateTransform.XProperty, _startX)
                        }
                    },
                    new KeyFrame()
                    {
                        Cue = new Cue(1d),
                        Setters =
                        {
                            new Setter(TranslateTransform.XProperty, _endX)
                        }
                    }
                }
            };
            await animation.RunAsync(LocoSvg);
        }
    }
}