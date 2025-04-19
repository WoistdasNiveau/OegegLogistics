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
    private double _startX;
    private double _endX;
    private IServiceProvider _serviceProvider;
    public SelectVehiceTypeView(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        
        SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        //Svg.Margin = new Thickness(0,0, 0, 0);
        //double width = e.NewSize.Width;
        //double height = e.NewSize.Height;
        //
        //Svg.Width = width;
        //Svg.Height = height;
        //
        //Canvas.SetLeft(Svg, width / 3 - width);
        //Canvas.SetBottom(Svg, height / 2 - height);
        
        TranslateTransform translateTransform = LocoSvg.RenderTransform as TranslateTransform ?? new TranslateTransform();
        LocoSvg.RenderTransform = translateTransform;
        
        _width = LocoSvg.Bounds.Width;
        _startX = -_width -20;
        var grid = this.Content as Grid;
        double col0Width = grid?.ColumnDefinitions[0].ActualWidth ?? Bounds.Width * 0.3;

        _endX = (-_width / 1.5 + col0Width) /2;
        
        translateTransform.X = _startX;
    }

    private async void InputElement_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        //Canvas.SetLeft(Svg, Svg.Width / 2);
        //Canvas.SetBottom(Svg, Svg.Height / 4);

        if (LocoSvg.RenderTransform is not TransformGroup group) return;
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

    private async void LocomotiveTapped(object? sender, TappedEventArgs e)
    {
        if (LocoSvg.RenderTransform is not TransformGroup group) return;
        var translate = group.Children.OfType<TranslateTransform>().FirstOrDefault();
        var scale = group.Children.OfType<ScaleTransform>().FirstOrDefault();

        if (translate == null || scale == null)
            return;

        var grid = this.Content as Grid;
        double viewWidth = grid?.Bounds.Width ?? Bounds.Width;
        double viewHeight = grid?.Bounds.Height ?? Bounds.Height;

        double locoWidth = LocoSvg.Bounds.Width;
        double locoHeight = LocoSvg.Bounds.Height;

        double targetScale = 2.0;

        double scaledWidth = locoWidth * targetScale;
        double scaledHeight = locoHeight * targetScale;

        // Position LocoSvg in the center while scaled
        double targetX = (viewWidth - scaledWidth) / 2;
        double targetY = (viewHeight - scaledHeight) / 2;

        Animation animation = new Animation()
        {
            Duration = TimeSpan.FromSeconds(0.8),
            Easing = new CubicEaseOut(),
            FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame()
                {
                    Cue = new Cue(0d),
                    Setters =
                    {
                        new Setter(ScaleTransform.ScaleXProperty, scale.ScaleX),
                        new Setter(ScaleTransform.ScaleYProperty, scale.ScaleY),
                        new Setter(TranslateTransform.XProperty, translate.X),
                        new Setter(TranslateTransform.YProperty, translate.Y)
                    }
                },
                new KeyFrame()
                {
                    Cue = new Cue(1d),
                    Setters =
                    {
                        new Setter(ScaleTransform.ScaleXProperty, targetScale),
                        new Setter(ScaleTransform.ScaleYProperty, targetScale),
                        new Setter(TranslateTransform.XProperty, targetX),
                        new Setter(TranslateTransform.YProperty, targetY)
                    }
                }
            }
        };

        await animation.RunAsync(LocoSvg);
    }
}