using DevFreela.Core.Entities;

namespace DevFreela.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task Add(User user);
        Task AddUserSkills(List<UserSkill> skills);
        Task<User> Login(string email, string pass);
    }
}
