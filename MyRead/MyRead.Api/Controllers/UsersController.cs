using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRead.Api.Requests;
using MyRead.Application.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Register
            (RegisterUserRequest request, [FromServices] IMediator mediator, [FromServices] INotifier notifier, CancellationToken token)
        {
            var user = await mediator.Send(request, token);
            return notifier.HasError
                ? BadRequest(notifier.GetErros())
                : Created("users/{id}", user);
        }
    }
}