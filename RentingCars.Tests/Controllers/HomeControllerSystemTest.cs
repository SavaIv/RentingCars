using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Threading.Tasks;
using Xunit;

namespace RentingCars.Tests.Controllers
{
    public class HomeControllerSystemTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public HomeControllerSystemTest(WebApplicationFactory<Program> _factory)
        {
            factory = _factory;
        }

        [Fact]
        public async Task IndexShouldReturnCorrectStatusCode()
        {
            // Arange
            var client = factory.CreateClient();

            // Act
            var result = await client.GetAsync("/");

            // Assert
            Assert.True(result.IsSuccessStatusCode);

            var response = await result.Content.ReadAsStringAsync();
        }
    }
}
