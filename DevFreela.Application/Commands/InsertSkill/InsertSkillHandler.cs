using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel>
    {
        private readonly ISkillRepository _skillRepository;

        public InsertSkillHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            await _skillRepository.Add(skill);
            
            return ResultViewModel.Success();
        }
    }
}
