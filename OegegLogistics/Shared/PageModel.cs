using CommunityToolkit.Mvvm.ComponentModel;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.Shared;

public partial class PageModel : ObservableObject
{
    [ObservableProperty]
    public PageState _pageState = PageState.Middle;
    
    [ObservableProperty]
    public uint _totalPages = 100;

    [ObservableProperty]
    public uint _currentPage;
}