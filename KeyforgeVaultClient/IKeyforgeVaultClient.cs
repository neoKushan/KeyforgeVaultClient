using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KeyforgeVaultClient.Models;

namespace KeyforgeVaultClient
{
    public interface IKeyforgeVaultClient
    {
        Task<List<House>> GetHouses();
        Task<House> GetHouseByName(string name);
        Task<Deck> GetDeck(Guid id);

        Task<int> GetTotalDeckCount();

        IEnumerable<Deck> SearchDecks(string name, int minimumPower, int maximumPower, int minimumChains, int maximumChains, IList<House> houses);
    }
}