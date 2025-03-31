using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using OegegLogistics.Shared;
using OegegLogistics.ViewModels.Enums;
using HttpRequestMessage = OegegLogistics.Shared.ImmutableHttp.HttpRequestMessage;

namespace OegegLogistics.Vehicles;

public partial class VehiclesViewModel : BaseViewModel
{
    // == Observable Properties ==
    [ObservableProperty]
    private ObservableCollection<VehicleDisplayViewModel> _vehicles = new()
    {
        new VehicleDisplayViewModel()
        {
            PublicId = Guid.CreateVersion7(),
            UICNumber = "185891202",
            Name = "Schlieren",
            Type = "Personenwaggon",
            Status = VehicleStatus.FullyOperable,
            Location = "Gleis 3",
            WorkCount = 0,
            Priority = Priority.Medium,
            ResponsiblePerson = "Franz"
        }
    };

    [ObservableProperty]
    private PageModel _currentPage = new PageModel();
    
    // == private fields ==
    private readonly HttpClient _client;
    
    // == constructor ==

    public VehiclesViewModel(HttpClient client)
    {
        _client = client;
    }
    
    public VehiclesViewModel()
    {
    }

    // == private methods ==
    private async Task GetVehiclesAsync()
    {
        Object response = await HttpRequestMessage.Empty
            .Method(HttpMethod.Get)
            .Endpoint("Vehicles")
            .PageSize(20)
            .PageNumber(1)
            .VehicleType(VehicleType.All)
            .Authorization("token")
            .ExecuteAsync<object>(_client);
    }
}