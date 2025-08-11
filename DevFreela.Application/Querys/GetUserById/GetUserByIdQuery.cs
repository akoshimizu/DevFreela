using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Querys.GetUserById
{
    public class GetUserByIdQuery : IRequest<ResultViewModel<UserViewModel>>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
