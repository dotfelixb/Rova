using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Rova.Core.Extensions;
using Rova.Core.Features.Users.CreateUser;
using Rova.Model.Domain;

namespace Rova.Core.Services
{
    public class UserService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ListResult<UserExtended>> ListUser(int offset = 0, int limit = 1000)
        {
            var route = $"/methods/users.list?offset={offset}&limit={limit}";

            var client = _clientFactory.CreateClient("Default");

            var response = await client.GetAsync(route);
            var responseBody = await response.Content.ReadAsStringAsync();

            var rst = JsonSerializer.Deserialize<ListResult<UserExtended>>(responseBody);

            return rst;
        }
        
        public async Task<SingleResult<Guid>> CreateUser(CreateUserCommand command)
        {
            var route = "/methods/users.create";
            var content = command.ToJsonStringContent();

            var client = _clientFactory.CreateClient("Default");

            var response = await client.PostAsync(route, content);
            var responseBody = await response.Content.ReadAsStreamAsync();

            var rst = JsonSerializer.Deserialize<SingleResult<Guid>>(responseBody);
            return rst;
        }
    }
}