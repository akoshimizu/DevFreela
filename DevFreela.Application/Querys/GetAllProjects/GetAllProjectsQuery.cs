using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Querys.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public GetAllProjectsQuery(string search, int page, int size)
        {
            this.search = search;
            this.page = page;
            this.size = size;
        }

        public string search { get; set; }
        public int page { get; set; }
        public int size { get; set; }

        public ProjectsGetAll ToEntity()
            => new(search, page, size);
    }
}
