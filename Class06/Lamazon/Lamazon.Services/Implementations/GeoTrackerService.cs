using Lamazon.Services.Interfaces;
using Lamazon.Services.Models;
using Newtonsoft.Json;

namespace Lamazon.Services.Implementations
{
    public class GeoTrackerService : IGeoTrackerService
    {
        private readonly HttpClient _httpClient;

        public GeoTrackerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GetCoutryFlagUrl(string countryCode)
        {
            return $"https://flagcdn.com/w320/{countryCode.ToLower()}.png";
        }

        public async Task<IpGeoInfo> GetIpGeoInfoAsync(string ipAddress)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"http://ip-api.com/json/{ipAddress}");
            var jsonData = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IpGeoInfo>(jsonData);
        }
    }
}
