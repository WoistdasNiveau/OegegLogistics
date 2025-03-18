using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace OegegLogistics.Shared.Components;

public partial class PageComponent : Border
{
    // == Bindable Properties ==
    #region Bindable Properties

    public static readonly StyledProperty<uint> MaxPageProperty =
        AvaloniaProperty.Register<PageComponent, uint>(
            nameof(MaxPage));

    public uint MaxPage
    {
        get => GetValue(MaxPageProperty);
        set
        {
            if(value < 1)
                return;
            SetValue(MaxPageProperty, value);
            UpdateMaxMage(value);
        }
    }
    
    public static readonly StyledProperty<uint> CurrentPageProperty =
        AvaloniaProperty.Register<PageComponent, uint>(
            nameof(CurrentPage),
            defaultValue: 1);

    public uint CurrentPage
    {
        get => GetValue(CurrentPageProperty);
        set
        {
            if(value == CurrentPage || value > MaxPage || value == 0)
                return;
            SetValue(CurrentPageProperty, value);
            UpdateCurrentPage(value);
        }
    }

    #endregion
    
    // == private fields ==
    private List<Label> pageLables;

    public PageComponent()
    {
        InitializeComponent();
        
        pageLables = new List<Label>
        {
            firstPageLabel,
            startDotLabel,
            previousPageLabel,
            currentPageLabel,
            nextPageLabel,
            endDotLabel,
            maxPageLabel
        };
    }
    
    // == private methods ==

    #region private methods

    private void UpdateCurrentPage(uint currentPage)
    {
        previousPageLabel.Content = currentPage - 1;
        currentPageLabel.Content = currentPage;
        nextPageLabel.Content = currentPage + 1;
    }

    private void UpdateMaxMage(uint maxPage)
    {
        nextPageLabel.Content = maxPage;
    }
    
    private void PageLabelTapped(object? sender, TappedEventArgs e)
    {
        uint pageNumber;
        if(sender is not Label label || label.Content == null || label.Content.Equals("...") || !uint.TryParse(label.Content.ToString(), out pageNumber))
            return;

        if (pageNumber == 1 || pageNumber == MaxPage)
        {
            HandelPageNumberEdgeCase(pageNumber == 1);
            return;
        }
        
        UpdateCurrentPage(pageNumber);
    }

    private void HandelPageNumberEdgeCase(bool isFirstPage = true)
    {
        if (isFirstPage)
        {
            pageLables.Skip(1)
                .ToList()
                .ForEach((t => t.Content = pageLables.IndexOf(t) + 2));
        }
        else
        {
            pageLables.Skip(2)
                .ToList()
                .ForEach((t => t.Content = pageLables.Count - pageLables.IndexOf(t)));
        }
    }
    #endregion
    
}