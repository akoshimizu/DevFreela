using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;

        public InsertSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
