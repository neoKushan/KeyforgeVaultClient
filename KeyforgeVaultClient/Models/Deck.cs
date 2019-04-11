using System.Collections.Generic;

namespace KeyforgeVaultClient.Models
{
    public class Deck
    {
        public string Name { get; set; }

        public int Expansion { get; set; }

        public int PowerLevel { get; set; }

        public int Chains { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public string Id { get; set; }

        // Leaving these commented out for now unless there's a need to allow 
        // people to pass in credentials
        //public bool IsMyDeck { get; set; }

        //public bool IsMyFavorite { get; set; }

        //public bool IsOnMyWatchlist { get; set; }

        public int CasualWins { get; set; }

        public int CasualLosses { get; set; }

        public IList<Card> Cards { get; set; }

        public IList<House> Houses { get; set; }
    }
}