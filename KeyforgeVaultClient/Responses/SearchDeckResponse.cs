using System.Collections.Generic;
using System.Runtime.Serialization;
using KeyforgeVaultClient.Responses.ResponseModels;

namespace KeyforgeVaultClient.Responses
{
    [DataContract]
    internal class SearchDeckResponse
    {
        [DataMember(Name = "data")]
        public List<ResponseDeck> Decks { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "_linked")]
        public SearchDeckLinked Linked { get; set; }
    }

    [DataContract]
    internal class SearchDeckLinked
    {
        [DataMember(Name = "houses")]
        public IList<ResponseHouse> Houses { get; set; }

        [DataMember(Name = "cards")]
        public IList<ResponseCard> Cards { get; set; }
    }
}
