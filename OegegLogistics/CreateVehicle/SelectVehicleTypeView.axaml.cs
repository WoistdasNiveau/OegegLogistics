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
using Svg;

namespace OegegLogistics.CreateVehicle;

[ViewFor<SelectVehicleTypeViewModel>]
public partial class SelectVehicleTypeView : UserControl
{
    // == private fields ==
    private double _width;
    private double _height;
    private IServiceProvider _serviceProvider;
    private bool _isLocoShowing;
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
            var gridSize = SvgGrid.Bounds;

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
        return;
        
        Animation animation = new Animation()
        {
            Duration = TimeSpan.FromSeconds(1),
            Easing = new ExponentialEaseOut(),
            FillMode = FillMode.Forward, 
            PlaybackDirection = PlaybackDirection.Reverse,
            Children =
            {
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, -_width + baseGrid.ColumnDefinitions[0].ActualWidth + SvgCanvas.Bounds.Width / 2),
                        new Setter(ScaleTransform.ScaleXProperty, 1),
                        new Setter(ScaleTransform.ScaleYProperty, 1)
                    },
                    KeyTime = TimeSpan.FromSeconds(0)
                },
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, (_width - SvgCanvas.Bounds.Width) / 2),
                        new Setter(TranslateTransform.YProperty, - SvgCanvas.Bounds.Width * 0.05),
                        new Setter(ScaleTransform.ScaleXProperty, 2.5),
                        new Setter(ScaleTransform.ScaleYProperty, 2.5),
                    },
                    KeyTime = TimeSpan.FromSeconds(1)
                },
            }
        };
        await animation.RunAsync(SvgGrid);
        selectBorder.IsVisible = true;
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
                    Setters = { new Setter(TranslateTransform.XProperty, -_width + baseGrid.ColumnDefinitions[0].ActualWidth + SvgCanvas.Bounds.Width / 2) },
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
                        new Setter(TranslateTransform.XProperty, -_width + baseGrid.ColumnDefinitions[0].ActualWidth + SvgCanvas.Bounds.Width / 2),
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
                        new Setter(TranslateTransform.XProperty, (_width - SvgCanvas.Bounds.Width) / 2),
                        new Setter(TranslateTransform.YProperty, - SvgCanvas.Bounds.Width * 0.05),
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
        if(_isLocoShowing)
            return;

        _slideAnimation.PlaybackDirection = PlaybackDirection.Normal;
        await _slideAnimation.RunAsync(SvgGrid);
        _isLocoShowing = true;
    }

    private async void LocomotiveTapped(object? sender, TappedEventArgs e)
    {
        _zoomAnimation.PlaybackDirection = PlaybackDirection.Normal;
        await _zoomAnimation.RunAsync(SvgGrid);
        selectBorder.IsVisible = false;
        return;
        TranslateTransform translateTransform = GetTransform<TranslateTransform>((SvgGrid.RenderTransform as TransformGroup)!);
        ScaleTransform scaleTransform = GetTransform<ScaleTransform>((SvgGrid.RenderTransform as TransformGroup)!);
        
        Animation animation = new Animation()
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
                        new Setter(TranslateTransform.XProperty, -_width + baseGrid.ColumnDefinitions[0].ActualWidth + SvgCanvas.Bounds.Width / 2),
                        new Setter(ScaleTransform.ScaleXProperty, 1d),
                        new Setter(ScaleTransform.ScaleYProperty, 1d)
                    },
                    KeyTime = TimeSpan.FromSeconds(0)
                },
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, (_width - SvgCanvas.Bounds.Width) / 2),
                        new Setter(TranslateTransform.YProperty, - SvgCanvas.Bounds.Width * 0.05),
                        new Setter(ScaleTransform.ScaleXProperty, 2.5d),
                        new Setter(ScaleTransform.ScaleYProperty, 2.5d),
                    },
                    KeyTime = TimeSpan.FromSeconds(1)
                },
            }
        };
        selectBorder.IsVisible = false;
        await animation.RunAsync(SvgGrid);
    }

    private T GetTransform<T>(TransformGroup transformGroup) where T : Transform
    {
        return transformGroup.Children.OfType<T>().FirstOrDefault()!;
    }
    
    private void UpdateOverlayPosition()
    {
        double canvasWidth = SvgCanvas.Bounds.Width;
        double canvasHeight = SvgCanvas.Bounds.Height;

        OverlayTextBox.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        var textSize = OverlayTextBox.DesiredSize;

        double x = canvasWidth * _relativeTextBoxX - textSize.Width / 2;
        double y = canvasHeight * _relativeTextBoxY - textSize.Height / 2;

        Canvas.SetLeft(OverlayTextBox, x);
        Canvas.SetTop(OverlayTextBox, y);
    }

    private void OverlayTextBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        bool indexToLast = OverlayTextBox.CaretIndex == OverlayTextBox.Text.Length;
        string text = Regex.Replace(OverlayTextBox.Text.Replace(" ", "").Replace("-", ""), "[a-zA-Z]", "");
        string newText= string.Empty;
        
        for (int i = 0; i < text.Length; i++)
        {
            if (i == 2 || i == 4 || i == 8 && text[i] != ' ')
            {
                newText += " ";
            }
            else if (i == 11)
            {
                newText += " - ";
            }
            newText += text[i];
        }
        OverlayTextBox.Text = newText;
        if(indexToLast)
            OverlayTextBox.CaretIndex = OverlayTextBox.Text?.Length ?? 0;
    }

    private void OverlayTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab ||
            e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Enter)
        {
            base.OnKeyDown(e);
        }
        else
        {
            var keyString = e.Key.ToString();
            if (!Regex.IsMatch(keyString, @"^D[0-9]$") && !Regex.IsMatch(keyString, @"^NumPad[0-9]$"))
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }
    }
}