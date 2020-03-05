using FreePractice.Web.Data.Tests.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace FreePractice.Web.Data.Tests
{
    public class CompanyServiceShould
    {
        private readonly Mock<IFakeHostingEnvironment> mockedEnv;

        public CompanyServiceShould()
        {
            this.mockedEnv = new Mock<IFakeHostingEnvironment>();
        }

        [Fact]
        [Trait("Category", "Reject")]
        public async Task HaveSentAnEmailAfterRejectingACompany()
        {
            this.mockedEnv.Setup(env => env.IsDevelopment()).Returns(true);

            Services.CompanyService sut = new Services.CompanyService(this.mockedEnv.Object);

            var result = await sut.Reject(0);

            Assert.True(result);
        }


    }
}
