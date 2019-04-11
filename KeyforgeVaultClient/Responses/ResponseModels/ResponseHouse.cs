using System;
using System.Runtime.Serialization;

namespace KeyforgeVaultClient.Responses.ResponseModels
{
    [DataContract]
    internal class ResponseHouse
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "image")]
        public Uri LogoUrl { get; set; }
    }
}