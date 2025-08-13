using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.InsertUserSkills;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Models;
using DevFreela.Application.Querys.GetUserById;
using DevFreela.Infrastructure.Autentication;
using DevFreela.Infrastructure.Notifications;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        private readonly DevFreelaDbContext _context;
        private readonly IAuthService _authService;

        public UsersController(IMediator mediator, IMemoryCache cache, IEmailService emailService, DevFreelaDbContext context, IAuthService authService)
        {
            _mediator = mediator;
            _cache = cache;
            _emailService = emailService;
            _context = context;
            _authService = authService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
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

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("password-recovery/request")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryRequestInputModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user == null) BadRequest();

            var code = new Random().Next(100000,  999999);

            var cacheKey = $"RecoveryCode:{model.Email}";
            _cache.Set(cacheKey, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(user.Email, "Codigo de recuperação", $"Seu Código de recuperação é: {code}");

            return NoContent();
        }

        [HttpPost("password-recovery/validate")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidadeRecoveryCode(ValidateRecoveryCodeInputModel model)
        {
            var cacheKey = $"RecoveryCode:{model.Email}";

            if(!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("password-recovery/change")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordInputModel model)
        {
            var cacheKey = $"RecoveryCode:{model.Email}";

            if (!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
            {
                return BadRequest();
            }

            _cache.Remove(cacheKey);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null) BadRequest();

            var hash = _authService.ComputeHash(model.NewPassword);
            user.UpdatePassword(hash);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
