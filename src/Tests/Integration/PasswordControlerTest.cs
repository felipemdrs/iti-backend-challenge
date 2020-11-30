using Domain.Commands;
using Domain.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Integration
{
    public class PasswordControlerTest : IClassFixture<AppTestFixture>, IDisposable
    {
        readonly AppTestFixture _fixture;
        readonly HttpClient _client;

        public PasswordControlerTest(AppTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
        }

        public void Dispose() => _fixture.Output = null;

        [Fact]
        public async Task ShouldBeReturnBadRequest()
        {
            var json = JsonConvert.SerializeObject(new ValidatePassword()
            {
                Password = null
            });

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var result = await _client.PostAsync("/api/password/validate", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ShouldBeReturnTrue()
        {
            var json = JsonConvert.SerializeObject(new ValidatePassword()
            {
                Password = "Abcdefg$1"
            });

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var result = await _client.PostAsync("/api/password/validate", stringContent);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<TransferModel<bool>>(content);

            Assert.True(data.Data);
        }

        [Fact]
        public async Task ShouldBeReturnFalse()
        {
            var json = JsonConvert.SerializeObject(new ValidatePassword()
            {
                Password = "Abcdefg1"
            });

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var result = await _client.PostAsync("/api/password/validate", stringContent);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<TransferModel<bool>>(content);

            Assert.False(data.Data);
        }
    }
}
