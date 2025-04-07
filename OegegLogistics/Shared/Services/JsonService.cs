using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace OegegLogistics.Shared.Services;

public class JsonService
{
    public const string INTEROPERABILITIES_LOCATION = "../../../../Uic_Data/Interoperability.json";
    public const string COUNTRYCODES_LOCATION = "../../../../Uic_Data/CountryCodes.json";
    public const string TYPE_LOCATION = "../../../../Uic_Data/Types.json";
    public const string VELOCITYHEATING_LOCATION = "../../../../Uic_Data/VelocityHeating.json";
    // == public methods ==
    public async Task<Dictionary<string, string>> ReadJsonFileAsync(string filePath)
    {
        try
        {
            await using FileStream stream = File.OpenRead(filePath);
            Dictionary<string, string>? result = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream);
            return result ?? new Dictionary<string, string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
