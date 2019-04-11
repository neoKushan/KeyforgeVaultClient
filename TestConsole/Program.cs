using System;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new KeyforgeVaultClient.KeyforgeVaultClient(null);

            var allDecks = client.SearchDecks();

            int count = 1;
            foreach (var deck in allDecks)
            {
                Console.WriteLine($"Deck {count++}: {deck.Name}");
            }
        }
    }
}
