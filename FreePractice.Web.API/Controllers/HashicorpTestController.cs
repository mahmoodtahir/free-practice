using Microsoft.AspNetCore.Mvc;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace FreePractice.Web.API.Controllers
{
    public class HashicorpTestController
    {
        [HttpGet]
        [Route("sampleone")]
        public async System.Threading.Tasks.Task<ActionResult> GetAsync()
        {
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo("s.FwVyy3qj5MxKm7PlbVsnRgiV");
            var vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200/", authMethod);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);

            Secret<SecretData> kv2Secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("RSG_UK_WCNP_RetailInsight/CERT");


            return null;
        }

    }
}
