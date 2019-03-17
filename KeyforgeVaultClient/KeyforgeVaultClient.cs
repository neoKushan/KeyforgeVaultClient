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

            var match = response.Houses.FirstOrDefault(x =>
                string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return MapResponseToHouse(match);
        }

        public async Task<Deck> GetDeck(Guid id)
        {
            var houseSerializer = new DataContractJsonSerializer(typeof(GetDeckResponse));

            var stream = _client.GetStreamAsync($"decks/{id}/?links=cards,notes,houses");

            var response = houseSerializer.ReadObject(await stream) as GetDeckResponse;

            return MapResponseToDeck(response);
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

        private IList<House> GetHouseList(IList<ResponseHouse> linkedHouses)
        {
            return linkedHouses.Select(MapResponseToHouse).ToList();
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
                IsMyDeck = deckResponse.IsMyDeck,
                IsMyFavorite = deckResponse.IsMyFavorite,
                IsOnMyWatchlist = deckResponse.IsOnMyWatchlist,
                CasualLosses = deckResponse.CasualLosses,
                CasualWins = deckResponse.CasualWins,
                Cards = GetCardsList(response.Deck.Links.Cards, response.Linked.Cards),
                Houses = GetHouseList(response.Linked.Houses)
            };
        }
    }
}