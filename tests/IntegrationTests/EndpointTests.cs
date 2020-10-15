﻿namespace IntegrationTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    //using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WebApp;
    using Xunit;

    public class EndpointTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public EndpointTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateSwaggerJsonTest()
        {
            var response = await this.client.GetAsync("/swagger/v1/swagger.json").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.NotEmpty(stringResponse);

            using (var outputFile = new StreamWriter("../../../../../src/WebApp/swagger.json"))
            {
                outputFile.WriteLine("//----------------------");
                outputFile.WriteLine("// <auto-generated>");
                outputFile.WriteLine($"//     Generated by {this.GetType().Namespace}.CreateSwaggerJsonTest");
                outputFile.WriteLine("// </auto-generated>");
                outputFile.WriteLine("//----------------------");
                outputFile.WriteLine(string.Empty);

                outputFile.WriteLine(stringResponse);
            }
        }

        // TODO: create swagger.json based typed httpclient/ts client >> https://stu.dev/generating-typed-client-for-httpclientfactory-with-nswag/

        [Fact]
        public async Task WeatherForecastGetTest()
        {
            var response = await this.client.GetAsync("api/v1/weatherforecasts").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(stringResponse);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task UserProfileGetTest()
        {
            var response = await this.client.GetAsync("api/v1/userprofile").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringResponse);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        //        [Fact]
        //        public async Task WeatherForecastPostTest()
        //        {
        //            var command = new WeatherForecastCreateCommand("??", "??");
        //#pragma warning disable CA2000 // Dispose objects before losing scope
        //            var response = await this.client.PostAsync("/users", this.CreateJsonContent(command)).ConfigureAwait(false);
        //#pragma warning restore CA2000 // Dispose objects before losing scope

        //            response.EnsureSuccessStatusCode();
        //            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            var result = JsonConvert.DeserializeObject<UserCreateCommandResponse>(stringResponse);

        //            Assert.True(response.Headers.Contains("Location"));
        //            Assert.NotEmpty(response.Headers.GetValues("Location"));
        //            Assert.Null(result);
        //        }

        //        private HttpContent CreateJsonContent(object obj)
        //        {
        //            var json = JsonConvert.SerializeObject(obj);
        //            var content = new StringContent(json, Encoding.UTF8, "application/json");

        //#pragma warning disable IDE0059 // Unnecessary assignment of a value
        //            // required due to https://github.com/dotnet/aspnetcore/issues/18463
        //            var contentLenth = content.Headers.ContentLength;
        //#pragma warning restore IDE0059 // Unnecessary assignment of a value

        //            return content;
        //        }
    }
}
