using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeyforgeVaultClient.Responses.ResponseModels
{
    [DataContract]
    internal class Links
    {
        [DataMember(Name = "houses")] public IList<string> Houses { get; set; }

        [DataMember(Name = "cards")] public IList<string> Cards { get; set; }
    }

    [DataContract]
    internal class ResponseDeck
    {
        [DataMember(Name = "name")] public string Name { get; set; }

        [DataMember(Name = "expansion")] public int Expansion { get; set; }

        [DataMember(Name = "power_level")] public int PowerLevel { get; set; }

        [DataMember(Name = "chains")] public int Chains { get; set; }

        [DataMember(Name = "wins")] public int Wins { get; set; }

        [DataMember(Name = "losses")] public int Losses { get; set; }

        [DataMember(Name = "id")] public string Id { get; set; }

        [DataMember(Name = "is_my_deck")] public bool IsMyDeck { get; set; }

        [DataMember(Name = "is_my_favorite")] public bool IsMyFavorite { get; set; }

        [DataMember(Name = "is_on_my_watchlist")]
        public bool IsOnMyWatchlist { get; set; }

        [DataMember(Name = "casual_wins")] public int CasualWins { get; set; }

        [DataMember(Name = "casual_losses")] public int CasualLosses { get; set; }

        [DataMember(Name = "_links")] public Links Links { get; set; }
    }
}