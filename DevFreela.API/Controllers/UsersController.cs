using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.InsertUserSkills;
using DevFreela.Application.Querys.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, InsertUserSkillsCommand command)
        {
            var result = await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            return Ok(description);
        }
    }
}
