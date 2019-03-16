using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using KeyforgeVaultClient.Models;
using KeyforgeVaultClient.Responses;
using KeyforgeVaultClient.Responses.ResponseModels;

namespace KeyforgeVaultClient
{
    public class KeyforgeVaultClient
    {
        private readonly HttpClient _client;
        
        public KeyforgeVaultClient(HttpClient client)
        {
            _client = client ?? new HttpClient();

            _client.BaseAddress = new Uri("https://www.keyforgegame.com/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<List<House>> GetHouses()
        {
            var houseSerializer = new DataContractJsonSerializer(typeof(GetHousesResponse));

            var stream = _client.GetStreamAsync("decks/houses/");

            var response = houseSerializer.ReadObject(await stream) as GetHousesResponse;

            return response.Houses.Select(MapResponseToHouse).ToList();
        }

        public async Task<House> GetHouseByName(string name)
        {
            var houseSerializer = new DataContractJsonSerializer(typeof(GetHousesResponse));

            var stream = _client.GetStreamAsync("decks/houses/");

            var response = houseSerializer.ReadObject(await stream) as GetHousesResponse;

            var match = response.Houses.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return MapResponseToHouse(match);
        }

        private House MapResponseToHouse(ResponseHouse response)
        {
            return new House()
            {
                Id = response.Id,
                Name = response.Name,
                LogoUrl = response.LogoUrl
            };
        }
    }
}
