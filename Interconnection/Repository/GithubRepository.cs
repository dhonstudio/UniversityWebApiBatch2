using Application.Interfaces.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Interconnection.Repository
{
    internal class GithubRepository : IGithubRepository
    {
        private const string BaseUrl = "https://api.github.com";
        private readonly IHttpClientFactory _httpClientFactory;

        public GithubRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GithubUser> GetUser(string username)
        {
            var client = _httpClientFactory.CreateClient();

            var user = await client
                .GetFromJsonAsync<GithubUser>($"{BaseUrl}/users/{username}");

            return user;
        }
    }
}
