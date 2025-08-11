using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertProject
{
    public class ValidateInsertProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public ValidateInsertProjectCommandBehavior(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = await _context.Users.AnyAsync(u => u.Id == request.IdClient);
            var freelancerExists = await _context.Users.AnyAsync(u => u.Id == request.IdFreelancer);

            if(!clientExists || !freelancerExists)
            {
                return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos");
            }

            return await next();
        }        
    }
}
