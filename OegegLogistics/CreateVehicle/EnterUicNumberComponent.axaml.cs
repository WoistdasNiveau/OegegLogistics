using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace OegegLogistics.CreateVehicle;

public class EnterUicNumberComponent : TemplatedControl
{
    # region StyledProperties
    public static readonly StyledProperty<string> SvgPathProperty = AvaloniaProperty.Register<EnterUicNumberComponent, string>(
        nameof(SvgPath));

    public string SvgPath
    {
        get => GetValue(SvgPathProperty);
        set => SetValue(SvgPathProperty, value);
    }

    public static readonly StyledProperty<string> WaterMarkProperty = AvaloniaProperty.Register<EnterUicNumberComponent, string>(
        nameof(WaterMark),
        defaultValue: "00 00 0000 000 - 0");

    public string WaterMark
    {
        get => GetValue(WaterMarkProperty);
        set => SetValue(WaterMarkProperty, value);
    }

    public static readonly StyledProperty<double> RelativeTextBoxXProperty = AvaloniaProperty.Register<EnterUicNumberComponent, double>(
        nameof(RelativeTextBoxX));

    public double RelativeTextBoxX
    {
        get => GetValue(RelativeTextBoxXProperty);
        set => SetValue(RelativeTextBoxXProperty, value);
    }

    public static readonly StyledProperty<double> RelativeTextBoxYProperty = AvaloniaProperty.Register<EnterUicNumberComponent, double>(
        nameof(RelativeTextBoxY));

    public double RelativeTextBoxY
    {
        get => GetValue(RelativeTextBoxYProperty);
        set => SetValue(RelativeTextBoxYProperty, value);
    }

    public static readonly StyledProperty<Rect> SvgCanvasBoundsProperty = AvaloniaProperty.Register<EnterUicNumberComponent, Rect>(
        nameof(SvgCanvasBounds));

    public Rect SvgCanvasBounds
    {
        get => GetValue(SvgCanvasBoundsProperty);
        set => SetValue(SvgCanvasBoundsProperty, value);
    }

    public static readonly StyledProperty<double> WidthProperty = AvaloniaProperty.Register<EnterUicNumberComponent, double>(
        nameof(Width));

    public double Width
    {
        get => GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }
    #endregion

    #region Private fields

    private Avalonia.Svg.Skia.Svg? _svg;
    private Grid? _svgGrid;
    private Canvas? _svgCanvas;
    private Viewbox? _svgViewbox;
    private TextBox? _overlayTextBox;
    
    #endregion
    public EnterUicNumberComponent()
    {
        SizeChanged += OnSizeChanged;
    }

    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _svg = e.NameScope.Find<Avalonia.Svg.Skia.Svg>("Svg");
        _svgGrid = e.NameScope.Find<Grid>("SvgGrid");
        _svgCanvas = e.NameScope.Find<Canvas>("SvgCanvas");

        _svgCanvas.PropertyChanged += (sender, args) =>
        {
            if (args.Property == Canvas.BoundsProperty)
            {
                Bounds = _svgCanvas.Bounds;
            }
        };
        
        _svgViewbox = e.NameScope.Find<Viewbox>("SvgViewbox");
        _overlayTextBox = e.NameScope.Find<TextBox>("OverlayTextBox");
        
        

        if (_svgGrid is not null && _svgCanvas is not null && _svgViewbox is not null)
        {
            _svgGrid.SizeChanged += (_, _) =>
            {
                var gridSize = _svgGrid.Bounds;

                _svgCanvas.Width = gridSize.Width;
                _svgCanvas.Height = gridSize.Height;

                _svgViewbox.Width = gridSize.Width;
                _svgViewbox.Height = gridSize.Height;

                Canvas.SetLeft(_svgViewbox, 0);
                Canvas.SetTop(_svgViewbox, 0);
            };

            _svgCanvas.SizeChanged += (_, _) => UpdateOverlayPosition();
        }
    }
    
    #region private methods 
    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        _svg.Margin = new Thickness(0,0, 0, 0);
        Width = e.NewSize.Width;
        //_height = e.NewSize.Height;
    }
    
    private void UpdateOverlayPosition()
    {
        double canvasWidth = _svgCanvas.Bounds.Width;
        double canvasHeight = _svgCanvas.Bounds.Height;

        // Make sure the TextBlock is measured
        _overlayTextBox.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        var textSize = _overlayTextBox.DesiredSize;

        // Convert percentage to absolute position
        double x = canvasWidth * RelativeTextBoxX - textSize.Width / 2;
        double y = canvasHeight * RelativeTextBoxY - textSize.Height / 2;

        Canvas.SetLeft(_overlayTextBox, x);
        Canvas.SetTop(_overlayTextBox, y);
    }
    
    #endregion
}