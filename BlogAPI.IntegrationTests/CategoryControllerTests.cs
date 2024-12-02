using BlogAPI.IntegrationTests.Infrastructure;
using BlogAPI.PL.Models.Categories;
using System.Net;

namespace BlogAPI.IntegrationTests
{
    public class CategoryControllerTests : BaseControllerTest
    {
        private const string _baseUrl = "categories";

        [Test]
        public Task CategoryController_GetCategories_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange & Act
                var response = await client.GetAsync($"{_baseUrl}");

                var halls = await GetResponseContent<List<CategoryResponse>>(response);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(halls.Count, Is.EqualTo(2));
            });
        }
    }
}
