using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using MediatR;
namespace DevFreela.Application.Querys.GetAllSkills
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, List<Skill>>
    {
        private readonly ISkillRepository _skillRepository;

        public GetAllSkillsHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<List<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetAll();
            return skills;
        }
    }
}
