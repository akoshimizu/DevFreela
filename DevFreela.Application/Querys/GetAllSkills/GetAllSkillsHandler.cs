using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace DevFreela.Application.Querys.GetAllSkills
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, List<Skill>>
    {
        private readonly DevFreelaDbContext _context;

        public GetAllSkillsHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _context.Skills.ToListAsync();
            return skills;
        }
    }
}
