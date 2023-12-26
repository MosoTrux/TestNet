using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestNet.Api.Models.Request;
using TestNet.Core.Options;

namespace TestNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GetByIdProductRequestModel request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddProductRequestModel request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateProductRequestModel request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }
    }
}
