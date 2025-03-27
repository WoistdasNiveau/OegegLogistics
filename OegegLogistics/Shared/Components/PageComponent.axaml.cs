using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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
            enterPageBox.ItemsSource = Enumerable.Range(1, (int)value);
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
            
            HandelPageNumberEdgeCase();
            enterPageBox.SelectedItem = value;
        }
    }

    #endregion
    
    // == private fields ==
    private List<Button> pageLables;

    public PageComponent()
    {
        InitializeComponent();
        pageLables = new List<Button>
        {
            firstPageLabel,
            startDotLabel,
            previousPageLabel,
            currentPageLabel,
            nextPageLabel,
            endDotLabel,
            maxPageLabel
        };
        
        HandelPageNumberEdgeCase();
    }
    
    // == private methods ==

    #region private methods

    private void UpdateMaxPage(uint maxPage)
    {
        maxPageLabel.Content = maxPage;
    }
    
    private void PageLabelTapped(object? sender, RoutedEventArgs e)
    {
        uint pageNumber;
        if(sender is not Button label || label.Content == null || label.Content.Equals("...") || !uint.TryParse(label.Content.ToString(), out pageNumber))
            return;
        
        if(pageNumber <= 0 || pageNumber > MaxPage)
            return;
        
        CurrentPage = pageNumber;
    }

    private void HandelPageNumberEdgeCase()
    {
        if (MaxPage <= 3)
        {
            bool otherLabelsVisible = MaxPage > 3;
            currentPageLabel.IsVisible = otherLabelsVisible;
            nextPageLabel.IsVisible = otherLabelsVisible;
            endDotLabel.IsVisible = otherLabelsVisible;
            maxPageLabel.IsVisible = otherLabelsVisible;
        }
        if (CurrentPage >= MaxPage - 2)
        {
            pageLables[5].Content = MaxPage - 1;
            pageLables[4].Content = MaxPage - 2;
            pageLables[3].Content = MaxPage - 3;
            pageLables[2].Content = MaxPage - 4;
            pageLables[1].Content = "...";
        }
        else if (CurrentPage <= 3)
        {
            pageLables.Skip(1)
                .Take(4)
                .ToList()
                .ForEach(label => label.Content =pageLables.IndexOf(label) + 1);
            pageLables[5].Content = "...";
        }
        else
        {
            List<Button> labels = pageLables.Skip(1).ToList();
            labels[0].Content = "...";
            labels[1].Content = CurrentPage - 1;
            labels[2].Content = CurrentPage;
            labels[3].Content = CurrentPage + 1;
            labels[4].Content = "...";
        }
        
        SetButtonStyle();
    }

    private void SetButtonStyle()
    {
        Button currentButton = pageLables.First(t => t.Content.ToString() == CurrentPage.ToString());
        currentButton.FontSize = 16;
        currentButton.Opacity = 1;
        
        pageLables.Where(t => t.Content?.ToString() != CurrentPage.ToString()).ToList().ForEach(f =>
        {
            f.FontSize = 14;
            f.Opacity = 0.5;
            f.IsEnabled = !f.Content?.Equals("...") ?? false;
        });
    }
    
    private void SwitchPageButtonClicked(object? sender, RoutedEventArgs e)
    {
        if(sender is not Button button)
            return;

        switch (button.Content)
        {
            case ">":
                CurrentPage++;
                break;
            case "<":
                CurrentPage--;
                break;
        }
    }
    
    private void EnterPageBox_OnDropDownClosed(object? sender, EventArgs e)
    {
        if(sender is not AutoCompleteBox autoCompleteBox || autoCompleteBox.SelectedItem == null || autoCompleteBox.IsDropDownOpen)
            return;
        CurrentPage = uint.Parse(autoCompleteBox.SelectedItem.ToString());
    }
    
    #endregion
}