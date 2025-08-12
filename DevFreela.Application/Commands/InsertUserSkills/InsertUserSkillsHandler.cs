using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    public class InsertUserSkillsHandler : IRequestHandler<InsertUserSkillsCommand, ResultViewModel<List<UserSkill>>>
    {
        private readonly IUserRepository _userRepository;

        public InsertUserSkillsHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultViewModel<List<UserSkill>>> Handle(InsertUserSkillsCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds.Select(s => new UserSkill(request.Id, s)).ToList();

            await _userRepository.AddUserSkills(userSkills);

            return ResultViewModel<List<UserSkill>>.Success(userSkills);
        }
    }
}
