using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    public class InsertUserSkillsCommand : IRequest<ResultViewModel<List<UserSkill>>>
    {
        public InsertUserSkillsCommand(int[] skillIds, int id)
        {
            SkillIds = skillIds;
            Id = id;
        }

        public int[] SkillIds { get; set; }
        public int Id { get; set; }
    }
}
