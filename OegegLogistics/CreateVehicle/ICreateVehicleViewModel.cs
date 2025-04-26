using System.Threading.Tasks;

namespace OegegLogistics.CreateVehicle;

public interface ICreateVehicleViewModel
{
    public Task Continue();
    public Task Return();
}