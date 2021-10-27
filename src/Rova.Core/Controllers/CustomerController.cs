using System.Text.Json;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rova.Core.Features.Customers.CreateCustomer;
using Rova.Core.Features.Customers.GetCustomer;
using Rova.Core.Features.Customers.ListCustomer;

namespace Rova.Core.Controllers
{
    public class CustomerController : MethodsController
    {
        private readonly ILogger<CustomerController> Logger;
        private readonly IMediator Mediatr;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediatr)
        {
            Logger = logger;
            Mediatr = mediatr;
        }

        [HttpGet("customers.get", Name = nameof(GetCustomer))]
        [ProducesResponseType(typeof(SingleResult<Guid>), 201)]
        [ProducesResponseType(typeof(ErrorResult), 404)]
        public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerCommand model)
        {
            var result = await Mediatr.Send(model);
            var rst = JsonSerializer.Serialize(result);

            if (!result.Ok)
            {
                return NotFound(rst);
            }

            return Ok(rst);
        }

        [HttpGet("customers.list", Name = nameof(ListCustomer))]
        [ProducesResponseType(typeof(SingleResult<Guid>), 201)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> ListCustomer([FromQuery] ListCustomerCommand model)
        {
            var result = await Mediatr.Send(model);
            var rst = JsonSerializer.Serialize(result);

            return Ok(rst);
        }

        [HttpPost("customers.create", Name = nameof(CreateCustomer))]
        [ProducesResponseType(typeof(SingleResult<Guid>), 201)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand model)
        {
            var result = await Mediatr.Send(model);
            var rst = JsonSerializer.Serialize(result);

            if (!result.Ok)
            {
                return BadRequest(rst);
            }

            var baseUri = $"{Request.Scheme}://{Request.Host}";
            var uri = $"{baseUri}/methods/customer.get?customerId={result.Data}";

            return Created(uri, rst);
        }
    }
}
