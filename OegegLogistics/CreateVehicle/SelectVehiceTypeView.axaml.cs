using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

[ViewFor<CreateVehicleViewModel>]
public partial class SelectVehiceTypeView : UserControl
{
    // == private fields ==
    private double _width;
    private double _height;
    private IServiceProvider _serviceProvider;
    private bool _isLocoShowing;
    private double _relativeTextBoxX = 0.47;
    private double _relativeTextBoxY = 0.61;
    public SelectVehiceTypeView(IServiceProvider serviceProvider, CreateVehicleViewModel createVehicleViewModel)
    {
        InitializeComponent();
        DataContext = createVehicleViewModel;
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

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        Svg.Margin = new Thickness(0,0, 0, 0);
        _width = e.NewSize.Width;
        _height = e.NewSize.Height;
    }

    private async void InputElement_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if(_isLocoShowing)
            return;
        
        _isLocoShowing = true;
        TranslateTransform translateTransform = GetTransform<TranslateTransform>((SvgGrid.RenderTransform as TransformGroup)!);

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
                        new Setter(TranslateTransform.XProperty, -_width + baseGrid.ColumnDefinitions[0].ActualWidth + SvgCanvas.Bounds.Width / 2)
                    },
                    KeyTime = TimeSpan.FromSeconds(1)
                },
            }
        };
        
        await animation.RunAsync(SvgGrid);
    }

    private async void LocomotiveTapped(object? sender, TappedEventArgs e)
    {
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
                        new Setter(TranslateTransform.XProperty, translateTransform.X),
                        new Setter(ScaleTransform.ScaleXProperty, scaleTransform.ScaleX),
                        new Setter(ScaleTransform.ScaleYProperty, scaleTransform.ScaleY)
                    },
                    KeyTime = TimeSpan.FromSeconds(0)
                },
                new KeyFrame()
                {
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, (_width - SvgCanvas.Bounds.Width) / 2),
                        new Setter(TranslateTransform.YProperty, translateTransform.Y - SvgCanvas.Bounds.Width * 0.05),
                        new Setter(ScaleTransform.ScaleXProperty, 2.5),
                        new Setter(ScaleTransform.ScaleYProperty, 2.5),
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