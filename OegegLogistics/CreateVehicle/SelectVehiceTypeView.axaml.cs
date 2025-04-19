using System;
using System.Linq;
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
using Svg;

namespace OegegLogistics.CreateVehicle;

public partial class SelectVehiceTypeView : UserControl
{
    // == private fields ==
    private double _width;
    private double _height;
    private IServiceProvider _serviceProvider;
    public SelectVehiceTypeView(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        
        SizeChanged += OnSizeChanged;
        Svg.SizeChanged += (sender, args) =>
        {
            if (Svg.Bounds.Height > 0 && Svg.Bounds.Width > 0)
            {
                SvgGrid.MaxHeight = Svg.Bounds.Height;
                SvgGrid.MaxWidth = Svg.Bounds.Width;   
            }
        };
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        Svg.Margin = new Thickness(0,0, 0, 0);
        _width = e.NewSize.Width;
        _height = e.NewSize.Height;

        if(SvgGrid.RenderTransform is not TransformGroup transformGroup
           || transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault() is not { } translateTransform
           || transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault() is not { } scaleTransform)
            return;
        
        
        
        return;

        Animation animation = new Animation()
        {
            Duration = TimeSpan.FromSeconds(1),
            Children =
            {
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter() { Property = Avalonia.Svg.Skia.Svg.MarginProperty, Value = new Thickness(0) }
                    },
                    KeyTime = TimeSpan.FromSeconds(0)
                },
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter()
                            { Property = Avalonia.Svg.Skia.Svg.MarginProperty, Value = new Thickness(-300, 0, 0, 0) }
                    },
                    KeyTime = TimeSpan.FromSeconds(1)
                }
            }
        };

        animation.RunAsync(Svg);
    }

    private async void InputElement_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if(SvgGrid.RenderTransform is not TransformGroup transformGroup
           || transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault() is not { } translateTransform
           || transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault() is not { } scaleTransform)
            return;

        Animation animation = new Animation()
        {
            Duration = TimeSpan.FromSeconds(1),
            Easing = new ExponentialEaseOut(),
            FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame()
                {
                    Setters = { new Setter(TranslateTransform.XProperty, -_width) },
                    KeyTime = TimeSpan.FromSeconds(0)
                },
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, -_width + baseGrid.ColumnDefinitions[0].Width.Value + Svg.Width / 2)
                    },
                    KeyTime = TimeSpan.FromSeconds(1)
                },
            }
        };
        
        animation.RunAsync(SvgGrid);
    }

    private async void LocomotiveTapped(object? sender, TappedEventArgs e)
    {
    }
}