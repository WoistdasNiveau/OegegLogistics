using CommunityToolkit.Mvvm.ComponentModel;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.Shared;

public class PageModel
{
    public PageState PageState { get; set; } = PageState.Middle;
    public uint TotalPages { get; set; } = 10;
    public uint PreviousPage { get; private set; }
    public uint NextPage { get; private set; }

    public uint CurrentPage
    {
        get => GetCurrentPage();
        set => SetCurrentPage(value);
    }

    private uint _currentPage;

    private void SetCurrentPage(uint pageNumber)
    {
        PreviousPage = pageNumber > 1 ? pageNumber - 1 : 1;
        _currentPage = pageNumber;
        NextPage = pageNumber < TotalPages ? pageNumber + 1 : TotalPages;
        
        PageState = PreviousPage == 1 ? PageState.Start 
            : NextPage == TotalPages ? PageState.End
            : PageState.Middle;
    }

    private uint GetCurrentPage()
    {
        return _currentPage;
    }
}