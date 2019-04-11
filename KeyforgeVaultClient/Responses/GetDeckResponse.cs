﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using KeyforgeVaultClient.Responses.ResponseModels;

namespace KeyforgeVaultClient.Responses
{
    [DataContract]
    internal class GetDeckResponse
    {
        [DataMember(Name = "data")]
        public ResponseDeck Deck { get; set; }

        [DataMember(Name = "_linked")]
        public DeckLinked Linked { get; set; }
    }

    [DataContract]
    internal class DeckLinked
    {
        [DataMember(Name = "houses")]
        public IList<ResponseHouse> Houses { get; set; }

        [DataMember(Name = "cards")]
        public IList<ResponseCard> Cards { get; set; }
    }
}