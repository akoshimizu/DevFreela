using DevFreela.Core.Entities;

namespace DevFreela.Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAll(ProjectsGetAll request);
        Task<Project?> GetDetailsById(int id);
        Task<Project?> GetById(int id);
        Task<int> Add(Project project);
        Task Update(Project project);
        Task AddComment(ProjectComment comment);
        Task<bool> Exists(int id);


    }
}
