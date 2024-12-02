using BlogAPI.IntegrationTests.Infrastructure;
using BlogAPI.PL.Models.Posts;
using System.Net;
using System.Net.Http.Headers;

namespace BlogAPI.IntegrationTests
{
    public class PostControllerTests : BaseControllerTest
    {
        private const string _baseUrl = "posts";

        [Test]
        public Task PostController_CreatePost_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: userName1);

                var requestModel = new CreatePostRequest()
                {
                    CategoryId = 1,
                    Title = "test",
                    Description = "test",
                    Photo = GetPhoto("jpg1.jpg")
                };
                var content = GetMultipartFormData(requestModel);

                // Act
                var response = await client.PostAsync($"{_baseUrl}", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }

        private MultipartFormDataContent GetMultipartFormData(CreatePostRequest model)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(model.CategoryId.ToString()), nameof(model.CategoryId) },
                { new StringContent(model.Title), nameof(model.Title) },
                { new StringContent(model.Description), nameof(model.Description) },
            };

            if (model.HashtagIds != null && model.HashtagIds.Any())
            {
                var hashtagIdsAsJson = System.Text.Json.JsonSerializer.Serialize(model.HashtagIds);
                content.Add(new StringContent(hashtagIdsAsJson, System.Text.Encoding.UTF8, "application/json"), nameof(model.HashtagIds));
            }

            var streamContent = new StreamContent(model.Photo.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(model.Photo.ContentType);
            content.Add(streamContent, nameof(model.Photo), model.Photo.FileName);

            return content;
        }
    }
}
