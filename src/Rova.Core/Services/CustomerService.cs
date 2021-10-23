using System;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Rova.Core.Extensions;
using Rova.Core.Features.Customers.CreateCustomer;

namespace Rova.Core.Services
{
    public class CustomerService 
    {
        private readonly IHttpClientFactory ClientFactory;

        public CustomerService(IHttpClientFactory clientFactory)  
        {
            ClientFactory = clientFactory;
        }

        public async Task<SingleResult<Guid>> CreateCustomer(CreateCustomerCommand command)
        {
            var route = "/methods/customer.create";
            var content = command.ToJsonStringContent();

            var client = ClientFactory.CreateClient("Default");

            var response  = await client.PostAsync(route, content);
            var responseBody = await response.Content.ReadAsStringAsync();
           
            var rst = JsonSerializer.Deserialize<SingleResult<Guid>>(responseBody);
            return rst;
        }
    }
}

