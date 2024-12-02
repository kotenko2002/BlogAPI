using BlogAPI.IntegrationTests.Infrastructure;
using BlogAPI.PL.Models.Auth;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BlogAPI.IntegrationTests
{
    public class AuthControllerTests : BaseControllerTest
    {
        private const string _baseUrl = "auth";

        [Test]
        public Task AuthController_RegisterOwner_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                var requestModel = new RegisterRequest()
                {
                    FirstName = "TestF",
                    LastName = "TestL",
                    Username = "test",
                    Email = "test@test.com",
                    Phone = "0985459234",
                    Password = "Qwerty123!"
                };
                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync($"{_baseUrl}/register", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }

        [Test]
        public Task AuthController_Login_ShouldReturnOk()
        {
            return PerformTest(async (client) =>
            {
                // Arrange
                await GenerateTokenAndSetAsHeader(username: userName1);

                var requestModel = new LoginRequest()
                {
                    Username = userName1,
                    Password = "Qwerty123!"
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync($"{_baseUrl}/login", content);

                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }
    }
}
