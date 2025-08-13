using DevFreela.Application.Commands.InsertSkill;
using DevFreela.Application.Querys.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    [Authorize]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var skills = _context.Skills.ToList();

            var skills = await _mediator.Send(new GetAllSkillsQuery());
            return Ok(skills);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand command)
        {
            //var skill = new Skill(request.Description);

            var skill = await _mediator.Send(command);

            return NoContent();
        }
    }
}
