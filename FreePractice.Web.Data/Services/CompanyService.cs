using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FreePractice.Web.Data.Services
{
    public class CompanyService : Interfaces.ICompanyService
    {
        private readonly IHostingEnvironment _env;

        public CompanyService(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<bool> Reject(int id)
        {
            // TODO: rejection logic

            if (_env.IsDevelopment())
            {
                // Send an email only if it is Dev env.
                return await Task.FromResult(true);
            }

            return false;
        }
    }

}
