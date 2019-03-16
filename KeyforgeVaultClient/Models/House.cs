using System;
using System.Runtime.Serialization;

namespace KeyforgeVaultClient.Models
{
    public class House
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Uri LogoUrl { get; set; }
    }
}
