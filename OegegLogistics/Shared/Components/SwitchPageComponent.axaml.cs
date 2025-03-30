using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace OegegLogistics.Shared.Components;

public class SwitchPageComponent : TemplatedControl
{
    // == styled properties ==

    #region StyledProperties

    public static readonly StyledProperty<uint> MaxPageNumberProperty = AvaloniaProperty.Register<SwitchPageComponent, uint>(
        nameof(MaxPageNumber),
        defaultValue: 1);

    public uint MaxPageNumber
    {
        get => GetValue(MaxPageNumberProperty);
        set
        {
            SetValue(MaxPageNumberProperty, value);
            UpdatePageButtons();
        }
    }

    public static readonly StyledProperty<uint> CurrentPageNumberProperty = AvaloniaProperty.Register<SwitchPageComponent, uint>(
        nameof(CurrentPageNumber),
        defaultValue: 1);

    public uint CurrentPageNumber
    {
        get => GetValue(CurrentPageNumberProperty);
        set
        {
            SetValue(CurrentPageNumberProperty, value);
            UpdatePageButtons();
        }
    }

    #endregion
    
    // == private fields ==
    private AutoCompleteBox _enterPageBox;
    private Button _prevPageButton;
    private Button _nextPageButton;
    private readonly List<Button> _pageButtons = new();
    
    // == private methods ==
    private void UpdatePageButtons()
    {
        this.IsVisible = MaxPageNumber > 0;
        if(!IsVisible)
            return;
        
        _enterPageBox.ItemsSource = Enumerable.Range(1, (int)MaxPageNumber);
        
        if (MaxPageNumber <= 7)
        {
            for (int i = 1; i <= 7; i++)
            {
                _pageButtons[i-1].Content = i;
            }
            _pageButtons.Skip((int)MaxPageNumber).ToList().ForEach(button => button.IsVisible = false);
        }
        else
        {
            if (CurrentPageNumber < 5)
            {
                for (int i = 1; i <= 5; i++)
                {
                    _pageButtons[i-1].Content = i;
                }

                _pageButtons[5].Content = "...";
                _pageButtons[6].Content = MaxPageNumber;
            }
            else if (CurrentPageNumber > MaxPageNumber - 4)
            {
                for (int i = 0; i < 5; i++)
                {
                    _pageButtons[6 - i].Content = MaxPageNumber - i;
                    _pageButtons[1].Content = "...";
                    _pageButtons[0].Content = 1;
                }
            }
            else
            {
                _pageButtons[0].Content = 1;
                _pageButtons[1].Content = "...";
                _pageButtons[2].Content = CurrentPageNumber - 1;
                _pageButtons[3].Content = CurrentPageNumber;
                _pageButtons[4].Content = CurrentPageNumber + 1;
                _pageButtons[5].Content = "...";
                _pageButtons[6].Content = MaxPageNumber;
            }
            
            _pageButtons.ForEach(t => t.IsVisible = true);
            _pageButtons.Where(t => t.Content == "...").ToList().ForEach(button => button.IsEnabled = false);
        }
        
        SetButtonStyle();
    }
    
    private void SetButtonStyle()
    {
        Button? currentButton = _pageButtons.FirstOrDefault(t => t.Content?.ToString() == CurrentPageNumber.ToString());
        if(currentButton == null)
            return;
        
        currentButton.FontSize = 16;
        currentButton.Opacity = 1;
        
        _pageButtons.Where(t => t.Content?.ToString() != CurrentPageNumber.ToString()).ToList().ForEach(f =>
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
                CurrentPageNumber = CurrentPageNumber == MaxPageNumber ? CurrentPageNumber : CurrentPageNumber + 1;
                break;
            case "<":
                 CurrentPageNumber = CurrentPageNumber == 1 ? CurrentPageNumber : CurrentPageNumber - 1;
                break;
        }
    }
    
    private void EnterPageBox_OnDropDownClosed(object? sender, EventArgs e)
    {
        if(sender is not AutoCompleteBox autoCompleteBox || autoCompleteBox.SelectedItem == null || autoCompleteBox.IsDropDownOpen)
            return;
        CurrentPageNumber = uint.Parse(autoCompleteBox.SelectedItem.ToString());
    }
    
    private void PageLabelTapped(object? sender, RoutedEventArgs e)
    {
        if(sender is not Button label || label.Content == null || label.Content.Equals("...") ||
           !uint.TryParse(label.Content.ToString(), out var pageNumber) || (pageNumber > MaxPageNumber))
            return;
        
        if(pageNumber <= 0 || pageNumber > MaxPageNumber)
            return;
        
        CurrentPageNumber = pageNumber;
    }
    
    // == override methods ==
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        if(this.GetVisualChildren().First() is not StackPanel stackPanel)
            return;

        List<Control> visualChildren = stackPanel.Children.ToList();
        StackPanel enterPagePanel = (visualChildren[0] as StackPanel)!;
        StackPanel choosePagePanel = (visualChildren[1] as StackPanel)!;

        _enterPageBox = enterPagePanel.Children.OfType<AutoCompleteBox>().First();
        _enterPageBox.DropDownClosed += EnterPageBox_OnDropDownClosed;
        
        StackPanel pageButtonPanel = choosePagePanel.Children.OfType<StackPanel>().First();
        
        _prevPageButton = choosePagePanel.Children.OfType<Button>().First(t => (string)t.Content! == "<");
        _prevPageButton.Click += SwitchPageButtonClicked;
        
        _nextPageButton = choosePagePanel.Children.OfType<Button>().First(t => (string)t.Content! == ">");
        _nextPageButton.Click += SwitchPageButtonClicked;
        
        pageButtonPanel.Children.OfType<Button>().ToList().ForEach(t => _pageButtons.Add(t));
        _pageButtons.ForEach(t => t.Click += PageLabelTapped);
        
        UpdatePageButtons();
    }
}