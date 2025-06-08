using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
using Mvvm.Navigation;
using OegegLogistics.ViewModels.Enums;
using Svg;

namespace OegegLogistics.CreateVehicle;

[ViewFor<SelectVehicleTypeViewModel>]
public partial class SelectVehicleTypeView : UserControl
{
    // == private fields ==
    private double _width;
    private double _height;
    private IServiceProvider _serviceProvider;
    private AnimationState _animationState = AnimationState.Idle;
    private double _relativeTextBoxX = 0.47;
    private double _relativeTextBoxY = 0.61;

    private Animation _slideAnimation;
    private Animation _zoomAnimation;
    public SelectVehicleTypeView(IServiceProvider serviceProvider, SelectVehicleTypeViewModel selectVehicleTypeViewModel)
    {
        InitializeComponent();
        DataContext = selectVehicleTypeViewModel;
        selectVehicleTypeViewModel.ReturnClicked += SelectVehicleTypeViewModelOnReturnClicked;
        _serviceProvider = serviceProvider;
        
        SizeChanged += OnSizeChanged;
        
        SvgGrid.SizeChanged += (_, _) =>
        {
            Rect gridSize = SvgGrid.Bounds;

            SvgCanvas.Width = gridSize.Width;
            SvgCanvas.Height = gridSize.Height;

            SvgViewbox.Width = gridSize.Width;
            SvgViewbox.Height = gridSize.Height;

            Canvas.SetLeft(SvgViewbox, 0);
            Canvas.SetTop(SvgViewbox, 0);
        };
        SvgCanvas.SizeChanged += (_, _) => UpdateOverlayPosition();
    }

    private async void SelectVehicleTypeViewModelOnReturnClicked(object? sender, EventArgs e)
    {
        _zoomAnimation.PlaybackDirection = PlaybackDirection.Reverse;
        await _zoomAnimation.RunAsync(SvgGrid);
        selectBorder.IsVisible = true;
        _animationState = AnimationState.Idle;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        Svg.Margin = new Thickness(0,0, 0, 0);
        _width = e.NewSize.Width;
        _height = e.NewSize.Height;
        
        _slideAnimation = new Animation()
        {
            Duration = TimeSpan.FromSeconds(1),
            Easing = new ExponentialEaseOut(),
            FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame
                {
                    Setters = { new Setter(TranslateTransform.XProperty, -_width) },
                    KeyTime = TimeSpan.FromSeconds(0)
                },
                new KeyFrame
                {
                    Setters = { new Setter(TranslateTransform.XProperty, -this.Bounds.Center.X /2)},
                    KeyTime = TimeSpan.FromSeconds(1)
                }
            }
        };
        
        _zoomAnimation = new Animation()
        {
            Duration = TimeSpan.FromSeconds(1),
            Easing = new ExponentialEaseOut(),
            FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, -this.Bounds.Center.X /2),
                        new Setter(TranslateTransform.YProperty, 0),
                        new Setter(ScaleTransform.ScaleXProperty, 1d),
                        new Setter(ScaleTransform.ScaleYProperty, 1d)
                    },
                    KeyTime = TimeSpan.FromSeconds(0)
                },
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, (this.Bounds.Center.X - SvgCanvas.Bounds.Center.X * 0.75) / 2),
                        new Setter(TranslateTransform.YProperty,  this.Bounds.Center.Y), //somehow adding values here does not change anything at all
                        new Setter(ScaleTransform.ScaleXProperty, 2.5d),
                        new Setter(ScaleTransform.ScaleYProperty, 2.5d),
                    },
                    KeyTime = TimeSpan.FromSeconds(1)
                },
            }
        };
    }

    private async void InputElement_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (_animationState != AnimationState.Idle)
            return;
        
        _animationState = AnimationState.SlideRunning;
        _slideAnimation.PlaybackDirection = PlaybackDirection.Normal;
        await _slideAnimation.RunAsync(SvgGrid);
        
        _animationState = AnimationState.SlideCompleted;
    }

    private async void LocomotiveTapped(object? sender, TappedEventArgs e)
    {
        if (_animationState != AnimationState.SlideCompleted)
            return;
        
        _animationState = AnimationState.ZoomRunning;
        _zoomAnimation.PlaybackDirection = PlaybackDirection.Normal;
        await _zoomAnimation.RunAsync(SvgGrid);
        
        _animationState = AnimationState.ZoomCompleted;
        selectBorder.IsVisible = false;
    }
    
    private void UpdateOverlayPosition()
    {
        double canvasWidth = SvgCanvas.Bounds.Width;
        double canvasHeight = SvgCanvas.Bounds.Height;

        OverlayTextBox.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        Size textSize = OverlayTextBox.DesiredSize;

        double x = canvasWidth * _relativeTextBoxX - textSize.Width / 2;
        double y = canvasHeight * _relativeTextBoxY - textSize.Height / 2;

        Canvas.SetLeft(OverlayTextBox, x);
        Canvas.SetTop(OverlayTextBox, y);
    }
}