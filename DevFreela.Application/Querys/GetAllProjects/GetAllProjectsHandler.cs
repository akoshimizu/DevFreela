using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Querys.GetAllProjects
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly DevFreelaDbContext _context;
        public GetAllProjectsHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                    .Include(p => p.Client)
                    .Include(p => p.Freelancer)
                    .Where(p => !p.IsDeleted && (request.search == "" || p.Title.Contains(request.search) || p.Description.Contains(request.search)))
                    .Skip(request.page * request.size)
                    .Take(request.size)
                    .ToListAsync();

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
