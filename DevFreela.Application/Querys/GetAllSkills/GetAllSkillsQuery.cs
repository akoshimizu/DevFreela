using DevFreela.Core.Entities;
using MediatR;
namespace DevFreela.Application.Querys.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<List<Skill>>
    {
    }
}
