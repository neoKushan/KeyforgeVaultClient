using System.Runtime.Serialization;

namespace KeyforgeVaultClient.Responses.ResponseModels
{
    [DataContract]
    internal class ResponseCard
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "card_title")]
        public string CardTitle { get; set; }

        [DataMember(Name = "house")]
        public string House { get; set; }

        [DataMember(Name = "card_type")]
        public string CardType { get; set; }

        [DataMember(Name = "front_image")]
        public string FrontImage { get; set; }

        [DataMember(Name = "card_text")]
        public string CardText { get; set; }

        [DataMember(Name = "traits")]
        public string Traits { get; set; }

        [DataMember(Name = "amber")]
        public int Amber { get; set; }

        [DataMember(Name = "power")]
        public int Power { get; set; }

        [DataMember(Name = "armor")]
        public int Armor { get; set; }

        [DataMember(Name = "rarity")]
        public string Rarity { get; set; }

        [DataMember(Name = "flavor_text")]
        public string FlavorText { get; set; }

        [DataMember(Name = "card_number")]
        public int CardNumber { get; set; }

        [DataMember(Name = "expansion")]
        public int Expansion { get; set; }

        [DataMember(Name = "is_maverick")]
        public bool IsMaverick { get; set; }
    }
}