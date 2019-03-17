namespace KeyforgeVaultClient.Models
{
    public class Card
    {
        public string Id { get; set; }

        public string CardTitle { get; set; }

        public string House { get; set; }

        public string CardType { get; set; }

        public string FrontImage { get; set; }

        public string CardText { get; set; }

        public string Traits { get; set; }

        public int Amber { get; set; }

        public int Power { get; set; }

        public int Armor { get; set; }

        public string Rarity { get; set; }

        public string FlavorText { get; set; }

        public int CardNumber { get; set; }

        public int Expansion { get; set; }

        public bool IsMaverick { get; set; }
    }
}