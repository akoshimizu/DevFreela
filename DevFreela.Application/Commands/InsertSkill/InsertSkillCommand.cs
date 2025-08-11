using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InsertSkillCommand : IRequest<ResultViewModel>
    {
        public string Description { get; set; }

        public InsertSkillCommand(string description)
        {
            Description = description;
        }
    }
}
