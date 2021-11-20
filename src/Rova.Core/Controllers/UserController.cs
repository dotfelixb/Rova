using System;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rova.Core.Features.Users.CreateUser;
using Rova.Core.Features.Users.ListUser;
using Rova.Model.Domain;

namespace Rova.Core.Controllers
{
    public class UserController : MethodsController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediator = mediatr;
        }

        [HttpGet("users.list", Name = nameof(ListUser))]
        [ProducesResponseType(typeof(ListResult<UserExtended>), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> ListUser([FromQuery] ListUserCommand model)
        {
            var result = await _mediator.Send(model);
            var rst = JsonSerializer.Serialize(result);

            return Ok(rst);
        }
        
        [HttpPost("users.create", Name = nameof(CreateUser))]
        [ProducesResponseType(typeof(SingleResult<Guid>), 201)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand model)
        {
            var result = await _mediator.Send(model);
            var rst = JsonSerializer.Serialize(result);

            if (!result.Ok)
            {
                return BadRequest(rst);
            }
            
            var baseUri = $"{Request.Scheme}://{Request.Host}";
            var uri = $"{baseUri}/methods/users.get?userid={result.Data}";

            return Created(uri, rst);
        }
    }
}