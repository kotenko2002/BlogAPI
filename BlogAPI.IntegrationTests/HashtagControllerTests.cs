using BlogAPI.IntegrationTests.Infrastructure;
using BlogAPI.PL.Models.Hashtags;
using System.Net;

namespace BlogAPI.IntegrationTests
{
    public class HashtagControllerTests : BaseControllerTest
    {
        private const string _baseUrl = "hashtags";

        [Test]
        public Task HashtagController_GetHashtags_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange & Act
                var response = await client.GetAsync($"{_baseUrl}");

                var halls = await GetResponseContent<List<HashtagResponse>>(response);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(halls.Count, Is.EqualTo(4));
            });
        }
    }
}
