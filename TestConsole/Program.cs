using System;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new KeyforgeVaultClient.KeyforgeVaultClient(null);

            var houses = await client.GetHouses();

            Console.WriteLine($"Found {houses.Count} houses");
            foreach (var house in houses)
            {
                Console.WriteLine($"House: {house.Name}");
            }

            Console.WriteLine("Hello World!");
        }
    }
}
