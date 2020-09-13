using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FreePractice.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        string kvSecretPath = "RSG_UK_WCNP_RetailInsight/CERT";
        string chSecretPath = "cubbyhole/";

        public WeatherForecastController()
        { }

        [HttpGet]
        public void GetAsync()
        {
            // EntityFrameworkConnectionTest();

            IAuthMethodInfo authMethod = new TokenAuthMethodInfo("s.Dfg02Jl4MxuD7SMjBGccldga");
            var vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200/", authMethod);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);

            // await GetSecretsViaHttp();

            // await this.GetKeyValueAsync(vaultClient);

            // await this.WriteKeyValueAsync(vaultClient);

            // await this.GetCubbyholeAsync(vaultClient);
        }

        private void EntityFrameworkConnectionTest()
        {
            var temp = new Data.DBFuncStore("Server=192.168.100.49;Database=RedBull_Epos;User Id=mtahir;Password=Mytestlogintwo123");
        }

        private async Task GetSecretsViaHttp()
        {
            // Creating an instance of the HTTP Client
            HttpClient HttpClient = HttpClientFactory.Create();
            //Setting the URL to our HashiCorp Secret
            string url = "http://127.0.0.1:8200/v1/secret/RSG_UK_WCNP_RetailInsight/CERT";
            // Setting the Token Header and the Root Token
            HttpClient.DefaultRequestHeaders.Add("X-Vault-Token", "s.skm4UryFKyGHgrdlHuanvPPI");
            // Setting the Content-type to application/json
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Making the HTTP Get call to consult our Secret
            JObject json = JObject.Parse(await HttpClient.GetStringAsync(url));
            // Printing the response
            Console.WriteLine(json);
            // Storing the key-value pairs of our secret from the response
            JToken secrets = json["data"]["data"];
            // Validating the previous statement is true
            Console.WriteLine("\n" + secrets);
            // Storing our key-value pairs to a Dictionary for future data manipulation
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(secrets.ToString());
            // Looping through our key-value pairs
            foreach (var item in values)
            {
                // Printing our key-value pairs
                Console.WriteLine($"Key: {item.Key} Value: {item.Value}");
            }
        }


        /*=================== KeyValue ===================*/
        private async Task GetKeyValueAsync(IVaultClient vaultClient)
        {
            Secret<SecretData> kv1Secret2 = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(kvSecretPath);
            SecretData data = kv1Secret2.Data;
        }

        private async Task WriteKeyValueAsync(IVaultClient vaultClient)
        {
            var value = new Dictionary<string, string> { { "CERTSQLAUTHUSR", "username" }, { "CERTSQLAUTHPW", "password" } };
            var writtenValue = await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(kvSecretPath, data: value, checkAndSet: 0);
        }

        /*=================== Cubbyhole ===================*/
        private async Task GetCubbyholeAsync(IVaultClient vaultClient)
        {
            Secret<Dictionary<string, object>> secret = await vaultClient.V1.Secrets.Cubbyhole.ReadSecretAsync(chSecretPath);
            Dictionary<string, object> secretValues = secret.Data;
        }

    }
}
