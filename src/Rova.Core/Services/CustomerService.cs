using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rova.Core.Extensions;
using Rova.Core.Features.Customers.CreateCustomer;
using Rova.Model.Domain;

namespace Rova.Core.Services
{
    public class CustomerService
    {
        private readonly IHttpClientFactory ClientFactory;

        public CustomerService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<SingleResult<Customer>> GetCustomer(Guid customerId)
        {
            var route = $"/methods/customers.get?customerid={customerId}";

            var client = ClientFactory.CreateClient("Default");

            var response = await client.GetAsync(route);
            var responseBody = await response.Content.ReadAsStringAsync();

            var rst = JsonSerializer.Deserialize<SingleResult<Customer>>(responseBody);

            return rst;
        }

        public async Task<ListResult<Customer>> ListCustomer(int offset = 0, int limit = 1000)
        {
            var route = $"/methods/customers.list?offset={offset}&limit={limit}";

            var client = ClientFactory.CreateClient("Default");

            var response = await client.GetAsync(route);
            var responseBody = await response.Content.ReadAsStringAsync();

            var rst = JsonSerializer.Deserialize<ListResult<Customer>>(responseBody);

            return rst;
        }

        public async Task<SingleResult<Guid>> CreateCustomer(CreateCustomerCommand command)
        {
            var route = "/methods/customers.create";
            var content = command.ToJsonStringContent();

            var client = ClientFactory.CreateClient("Default");

            var response = await client.PostAsync(route, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var rst = JsonSerializer.Deserialize<SingleResult<Guid>>(responseBody);
            return rst;
        }
    }
}

