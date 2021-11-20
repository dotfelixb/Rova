using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Rova.Core.Extensions;
using Rova.Core.Features.Roles.CreateRole;
using Rova.Model.Domain;

namespace Rova.Core.Services
{
    public class RoleService
    {
        private readonly IHttpClientFactory _clientFactory;

        public RoleService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ListResult<RoleExtended>> ListRole(int offset = 0, int limit = 1000)
        {
            var route = $"/methods/roles.list?offset={offset}&limit={limit}";

            var client = _clientFactory.CreateClient("Default");

            var response = await client.GetAsync(route);
            var responseBody = await response.Content.ReadAsStringAsync();

            var rst = JsonSerializer.Deserialize<ListResult<RoleExtended>>(responseBody);

            return rst;
        }
        
        public async Task<SingleResult<Guid>> CreateRole(CreateRoleCommand command)
        {
            var route = "/methods/roles.create";
            var content = command.ToJsonStringContent();

            var client = _clientFactory.CreateClient("Default");

            var response = await client.PostAsync(route, content);
            var responseBody = await response.Content.ReadAsStreamAsync();

            var rst = JsonSerializer.Deserialize<SingleResult<Guid>>(responseBody);
            return rst;
        }
    }
}