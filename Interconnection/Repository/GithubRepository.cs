using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Interconnection.Repository
{
    internal class GithubRepository : IGithubRepository
    {
        private const string BaseUrl = "https://api.github.com";
        private const string BaseUrl2 = "https://localhost:7286";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public GithubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<GithubUser> GetUser(string username)
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("User-Agent", "C# App");

            var user = await client
                .GetFromJsonAsync<GithubUser>($"{BaseUrl}/users/{username}");

            return user;
        }

        public async Task<string> GetToken()
        {
            var client = _httpClientFactory.CreateClient();
            var requestToken = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BaseUrl2}/auth/generatepublictoken"),
            };
            client.DefaultRequestHeaders.Add("clientid", _configuration["PublicToken:ClientId"]);
            client.DefaultRequestHeaders.Add("clientsecret", _configuration["PublicToken:ClientSecret"]);

            var response = await client.SendAsync(requestToken);
            var result = await response.Content.ReadAsStringAsync();
            dynamic token = JsonConvert.DeserializeObject(result);
            var finalToken = token?.token;
            return finalToken;
        }

        public async Task<List<Student>> GetStudentInterkoneksi()
        {
            var client = _httpClientFactory.CreateClient();
            var token = await GetToken();

            //client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var student = await client
                .GetFromJsonAsync<List<Student>>($"{BaseUrl2}/student");

            return student;
        }
    }
}
