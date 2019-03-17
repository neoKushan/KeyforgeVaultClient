using System;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new KeyforgeVaultClient.KeyforgeVaultClient(null);

            //var houses = await client.GetHouses();

            //Console.WriteLine($"Found {houses.Count} houses");
            //foreach (var house in houses)
            //{
            //    Console.WriteLine($"House: {house.Name}");
            //}

            var deck = await client.GetDeck(new Guid("3b6bfd61-29f2-4839-bb4d-e3664424d5d5"));

            Console.WriteLine("Hello World!");
        }
    }
}
