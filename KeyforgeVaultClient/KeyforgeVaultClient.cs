using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using KeyforgeVaultClient.Models;
using KeyforgeVaultClient.Responses;
using KeyforgeVaultClient.Responses.ResponseModels;

namespace KeyforgeVaultClient
{
    public class KeyforgeVaultClient : IKeyforgeVaultClient
    {
        private readonly HttpClient _client;

        public KeyforgeVaultClient(HttpClient client = null)
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

            if (response == null)
            {
                return new List<House>();
            }

            return response.Houses.Select(MapResponseToHouse).ToList();
        }

        public async Task<House> GetHouseByName(string name)
        {
            var houseSerializer = new DataContractJsonSerializer(typeof(GetHousesResponse));

            var stream = _client.GetStreamAsync("decks/houses/");

            var response = houseSerializer.ReadObject(await stream) as GetHousesResponse;

            if (response == null)
            {
                return new House();
            }

            var match = response.Houses.FirstOrDefault(x =>
                string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return MapResponseToHouse(match);
            
        }

        public async Task<Deck> GetDeck(Guid id)
        {
            var responseSerializer = new DataContractJsonSerializer(typeof(GetDeckResponse));

            var stream = _client.GetStreamAsync($"decks/{id}/?links=cards,houses");

            var response = responseSerializer.ReadObject(await stream) as GetDeckResponse;

            return MapResponseToDeck(response);
        }

        public async Task<int> GetTotalDeckCount()
        {
            var response = await SearchDecksByPage(); // Seach with no params set
            return response.Count;
        }

        public IEnumerable<Deck> SearchDecks(string name = "", 
            int minimumPower = 0, 
            int maximumPower = 11, 
            int minimumChains = 0, 
            int maximumChains = 24,
            IList<House> houses = null)
        {
            var housesToSearch = GetHouseNamesFromList(houses);

            var totalDecks = int.MaxValue;
            var count = 0;
            var page = 1; // Start at the first page

            while (count < totalDecks)
            {
                var response = SearchDecksByPage(page, name, minimumPower, maximumPower, minimumChains, maximumChains, housesToSearch).Result; // We can't do async IEnumerable (yet) so we have to do this syncronously for now
                totalDecks = response.Count;

                foreach (var deck in response.Decks)
                {
                    // Yield and return a single deck, let the caller decide when 
                    // they have had enough
                    yield return new Deck
                    {
                        Id = deck.Id,
                        Name = deck.Name,
                        Expansion = deck.Expansion,
                        PowerLevel = deck.PowerLevel,
                        Chains = deck.Chains,
                        Wins = deck.Wins,
                        Losses = deck.Losses,
                        //IsMyDeck = deck.IsMyDeck,
                        //IsMyFavorite = deck.IsMyFavorite,
                        //IsOnMyWatchlist = deck.IsOnMyWatchlist,
                        CasualLosses = deck.CasualLosses,
                        CasualWins = deck.CasualWins,
                        Cards = GetCardsList(deck.Links.Cards, response.Linked.Cards),
                        Houses = GetHouseList(deck.Links.Houses, response.Linked.Houses)
                    };

                    count++;
                }

                // Move onto the next page
                page++;
            }
        }

        private async Task<SearchDeckResponse> SearchDecksByPage(
            int page = 1,
            string name = "",
            int minimumPower = 0,
            int maximumPower = 11,
            int minimumChains = 0,
            int maximumChains = 24,
            string housesToSearch = "")
        {
            var responseSerializer = new DataContractJsonSerializer(typeof(SearchDeckResponse));

            var stream = await _client
                .GetStreamAsync(
                    $"decks/?page={page}&page_size=25&search={name}&power_level={minimumPower},{maximumPower}&chains={minimumChains},{maximumChains}&houses={housesToSearch}&ordering=-date&links=cards,houses");

            var response = responseSerializer.ReadObject(stream) as SearchDeckResponse;
            return response;
        }

        private string GetHouseNamesFromList(IList<House> houses)
        {
            if (houses == null)
            {
                return "";
            }

            var houseString = "";
            for (var i = 0; i < houses.Count; i++)
            {
                houseString += houses[i].Id;

                if (i < houses.Count)
                {
                    houseString += ",";
                }
            }

            return houseString;
        }

        private House MapResponseToHouse(ResponseHouse response)
        {
            return new House
            {
                Id = response.Id,
                Name = response.Name,
                LogoUrl = response.LogoUrl
            };
        }

        private IList<Card> GetCardsList(IList<string> cards, IList<ResponseCard> linkedCards)
        {
            return cards.Select(x => MapResponseToCard(linkedCards.First(y => y.Id == x))).ToList();
        }

        private IList<House> GetHouseList(IList<string> houses, IList<ResponseHouse> linkedHouses)
        {
            return houses.Select(x => MapResponseToHouse(linkedHouses.First(y => y.Id == x))).ToList();
        }

        private Card MapResponseToCard(ResponseCard response)
        {
            return new Card
            {
                Id = response.Id,
                Amber = response.Amber,
                Armor = response.Armor,
                CardNumber = response.CardNumber,
                CardText = response.CardText,
                CardTitle = response.CardTitle,
                CardType = response.CardType,
                Expansion = response.Expansion,
                FlavorText = response.FlavorText,
                FrontImage = response.FrontImage,
                House = response.House,
                Rarity = response.Rarity,
                Power = response.Power,
                Traits = response.Traits,
                IsMaverick = response.IsMaverick
            };
        }

        private Deck MapResponseToDeck(GetDeckResponse response)
        {
            var deckResponse = response.Deck;

            return new Deck
            {
                Id = deckResponse.Id,
                Name = deckResponse.Name,
                Expansion = deckResponse.Expansion,
                PowerLevel = deckResponse.PowerLevel,
                Chains = deckResponse.Chains,
                Wins = deckResponse.Wins,
                Losses = deckResponse.Losses,
                //IsMyDeck = deckResponse.IsMyDeck,
                //IsMyFavorite = deckResponse.IsMyFavorite,
                //IsOnMyWatchlist = deckResponse.IsOnMyWatchlist,
                CasualLosses = deckResponse.CasualLosses,
                CasualWins = deckResponse.CasualWins,
                Cards = GetCardsList(response.Deck.Links.Cards, response.Linked.Cards),
                Houses = GetHouseList(deckResponse.Links.Houses, response.Linked.Houses)
            };
        }
    }
}