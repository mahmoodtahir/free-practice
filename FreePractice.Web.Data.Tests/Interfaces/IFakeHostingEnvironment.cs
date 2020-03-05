using Microsoft.AspNetCore.Hosting;

namespace FreePractice.Web.Data.Tests.Interfaces
{
    public interface IFakeHostingEnvironment : IHostingEnvironment
    {
        bool IsDevelopment();
    }
}
