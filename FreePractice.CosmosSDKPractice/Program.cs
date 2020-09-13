using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;

namespace FreePractice.CosmosSDKPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            string checkFormat = DateTime.Now.ToString("yyyMMdd");
            string checkFormat2 = DateTime.Now.ToString("yyyyMMddhhmmss.ffff");
            

            int storeId = 0;
            int modulus = (storeId % 100);
            string result = (storeId % 100).ToString().PadLeft(2, '0');
            // Console.WriteLine("Hello World!");
            QueryFromDocument();

        }

        private static void QueryFromDocument()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string endpoint = config["endpoint"];
            string key = config["key"];

            using (var client = new CosmosClient(endpoint, key))
            {
                var container = client.GetContainer("Dart", "Alerts");



            }
        }
    }
}
