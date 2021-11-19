using System;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rova.Core.Features.Leads.CreateLead;
using Rova.Core.Features.Leads.ListLead;
using Rova.Model.Domain;

namespace Rova.Core.Controllers
{
    public class LeadController : MethodsController
    {
        private readonly ILogger<LeadController> Logger;
        private readonly IMediator Mediatr;

        public LeadController(ILogger<LeadController> logger, IMediator mediatr)
        {
            Logger = logger;
            Mediatr = mediatr;
        }

        [HttpGet("leads.list", Name = nameof(ListLead))]
        [ProducesResponseType(typeof(ListResult<LeadExtended>), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> ListLead([FromQuery] ListLeadCommand model)
        {
            var result = await Mediatr.Send(model);
            var rst = JsonSerializer.Serialize(result);

            return Ok(rst);
        }

        [HttpPost("leads.create", Name = nameof(CreateLead))]
        [ProducesResponseType(typeof(SingleResult<Guid>), 201)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> CreateLead([FromBody] CreateLeadCommand model)
        {
            var result = await Mediatr.Send(model);
            var rst = JsonSerializer.Serialize(result);

            if (!result.Ok)
            {
                return BadRequest(rst);
            }

            var baseUri = $"{Request.Scheme}://{Request.Host}";
            var uri = $"{baseUri}/methods/leads.get?leadid={result.Data}";

            return Created(uri, rst);
        }
    }
}