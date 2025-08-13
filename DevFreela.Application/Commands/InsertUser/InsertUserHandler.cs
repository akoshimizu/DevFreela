using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Autentication;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public InsertUserHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }
        public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var hashPass = _authService.ComputeHash(request.Password);
            var user = new User(request.FullName, request.Email, request.BirthDate, hashPass, request.Role);

            await _userRepository.Add(user);

            return ResultViewModel.Success();
        }
    }
}
