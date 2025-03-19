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
            UpdateMaxPage(value);
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
        MaxPage = 100;
        
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

    private void UpdateMaxPage(uint maxPage)
    {
        maxPageLabel.Content = maxPage;
    }
    
    private void PageLabelTapped(object? sender, TappedEventArgs e)
    {
        uint pageNumber;
        if(sender is not Label label || label.Content == null || label.Content.Equals("...") || !uint.TryParse(label.Content.ToString(), out pageNumber))
            return;
        
        if(pageNumber <= 0 || pageNumber > MaxPage)
            return;
        
        CurrentPage = pageNumber;
        HandelPageNumberEdgeCase();
    }

    private void HandelPageNumberEdgeCase()
    {
        if (CurrentPage >= MaxPage - 2)
        {
            pageLables[5].Content = MaxPage - 1;
            pageLables[4].Content = MaxPage - 2;
            pageLables[3].Content = MaxPage - 3;
            pageLables[2].Content = MaxPage - 4;
            pageLables[1].Content = "...";

            pageLables.First(t => t.Content.ToString() == CurrentPage.ToString()).FontSize = 16;
            pageLables.Where(t => t.Content.ToString() != CurrentPage.ToString()).ToList().ForEach(f => f.FontSize = 14);
        }
        else if (CurrentPage <= 3)
        {
            pageLables.Skip(1)
                .Take(4)
                .ToList()
                .ForEach(label => label.Content =pageLables.IndexOf(label) + 1);
            pageLables[5].Content = "...";
            
            pageLables.First(t => t.Content.ToString() == CurrentPage.ToString()).FontSize = 16;
            pageLables.Where(t => t.Content.ToString() != CurrentPage.ToString()).ToList().ForEach(f => f.FontSize = 14);
        }
        else
        {
            List<Label> labels = pageLables.Skip(1).ToList();
            labels[0].Content = "...";
            labels[1].Content = CurrentPage - 1;
            labels[2].Content = CurrentPage;
            labels[3].Content = CurrentPage + 1;
            labels[4].Content = "...";
            
            pageLables.First(t => t.Content.ToString() == CurrentPage.ToString()).FontSize = 16;
            pageLables.Where(t => t.Content.ToString() != CurrentPage.ToString()).ToList().ForEach(f => f.FontSize = 14);
        }
    }
    #endregion
    
}