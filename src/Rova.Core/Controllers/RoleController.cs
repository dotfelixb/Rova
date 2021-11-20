using System;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rova.Core.Features.Roles.CreateRole;
using Rova.Core.Features.Roles.ListRole;
using Rova.Model.Domain;

namespace Rova.Core.Controllers
{
    public class RoleController : MethodsController
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IMediator _mediator;

        public RoleController(ILogger<RoleController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediator = mediatr;
        }
        
        [HttpGet("roles.list", Name = nameof(ListRole))]
        [ProducesResponseType(typeof(ListResult<RoleExtended>), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> ListRole([FromQuery] ListRoleCommand model)
        {
            var result = await _mediator.Send(model);
            var rst = JsonSerializer.Serialize(result);

            return Ok(rst);
        }

        [HttpPost("roles.create", Name = nameof(CreateRole))]
        [ProducesResponseType(typeof(SingleResult<Guid>), 201)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand model)
        {
            var result = await _mediator.Send(model);
            var rst = JsonSerializer.Serialize(result);

            if (!result.Ok)
            {
                return BadRequest(rst);
            }
            
            var baseUri = $"{Request.Scheme}://{Request.Host}";
            var uri = $"{baseUri}/methods/roles.get?roleid={result.Data}";

            return Created(uri, rst);
        }
    }
}