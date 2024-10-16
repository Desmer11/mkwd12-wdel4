using Lamazon.Services.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IGeoTrackerService
    {
        string GetCoutryFlagUrl(string countryCode);
        Task<IpGeoInfo> GetIpGeoInfoAsync(string ipAddress);
    }
}
