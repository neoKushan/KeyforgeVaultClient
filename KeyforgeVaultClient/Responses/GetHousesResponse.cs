using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using KeyforgeVaultClient.Models;
using KeyforgeVaultClient.Responses.ResponseModels;

namespace KeyforgeVaultClient.Responses
{
    [DataContract]
    internal class GetHousesResponse
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "data")]
        public List<ResponseHouse> Houses { get; set; }
    }
}
