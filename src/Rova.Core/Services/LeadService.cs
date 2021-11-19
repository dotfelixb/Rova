using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Rova.Core.Extensions;
using Rova.Core.Features.Leads.CreateLead;
using Rova.Model.Domain;

namespace Rova.Core.Services
{
    public class LeadService
    {
        private readonly IHttpClientFactory ClientFactory;

        public LeadService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<ListResult<LeadExtended>> ListLead(int offset = 0, int limit = 1000)
        {
            var route = $"/methods/leads.list?offset={offset}&limit={limit}";

            var client = ClientFactory.CreateClient("Default");

            var response = await client.GetAsync(route);
            var responseBody = await response.Content.ReadAsStringAsync();

            var rst = JsonSerializer.Deserialize<ListResult<LeadExtended>>(responseBody);

            return rst;
        }

        public async  Task<SingleResult<Guid>> CreateLead(CreateLeadCommand command)
        {
            var route = "/methods/leads.create";
            var content = command.ToJsonStringContent();

            var client = ClientFactory.CreateClient("Default");

            var response = await client.PostAsync(route, content);
            var responseBody = await response.Content.ReadAsStreamAsync();

            var rst = JsonSerializer.Deserialize<SingleResult<Guid>>(responseBody);
            return rst;
        }
    }
}