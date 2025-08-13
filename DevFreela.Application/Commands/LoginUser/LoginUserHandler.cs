using DevFreela.Application.Models;
using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Autentication;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly DevFreelaDbContext _context;
        private readonly IUserRepository _userRepository;
        public LoginUserHandler(IAuthService authService, DevFreelaDbContext context, IUserRepository userRepository)
        {
            _authService = authService;
            _context = context;
            _userRepository = userRepository;
        }
        public async Task<ResultViewModel<LoginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            var user = await _userRepository.Login(request.Email, hash);

            if (user == null)
            {
                return ResultViewModel<LoginViewModel>.Error("Erro de login");
            }

            var token = _authService.GenerateToken(user.Email, user.Role);

            var viewModel = new LoginViewModel(token);

            var result = ResultViewModel<LoginViewModel>.Success(viewModel);

            return result;
        }
    }
}
