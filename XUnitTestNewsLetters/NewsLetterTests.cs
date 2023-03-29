using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestNewsLetters
{
    public class NewsLetterTests : IClassFixture<TestFixture<Program>>
    {
        private HttpClient Client;

        public NewsLetterTests(TestFixture<Program> fixture)
        {
            Client = fixture.Client;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer","eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyQGV4YW1wbGUuY29tIiwianRpIjoiOTY0NGM5MWYtNzlkYy00ODYxLTlmZmMtMWVlZTJjZTNlOWVkIiwiaWF0IjoxNjgwMDYxNTQ5LCJpZCI6IjJhZjcxODNiLTU4MzEtNDgwYy1iMjY2LTc0MDJlYjQ3NGJkNyIsIm5iZiI6MTY4MDA2MTU0OCwiZXhwIjoxNjg1MjQ1NTQ4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyMDIvIiwiYXVkIjoidGVzdCJ9.Y6UtX6HHNyzcWP09WIlvRUVVwUAZXeWgF2NSU2it1eQ");
        }

        [Fact]
        public async Task WhenCreateNewsLetterReturnSucess()
        {
            // Arrange
            var request = new
            {
                Url = "api/NewsLetter/upsert",
                Body = new 
                {
                    Title = "Test title",
                    HtmlBody = "<p>contenido</p>"
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WhenCreateNewsLetterWithErrorReturnBadRequest()
        {
            // Arrange
            var request = new
            {
                Url = "api/NewsLetter/upsert",
                Body = new
                {
                    Title = ""
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
