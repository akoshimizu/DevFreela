using DevFreela.Application.Models;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        public CompleteProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetById(request.Id);

            if (project == null)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            project.Complete();
            
            await _projectRepository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
