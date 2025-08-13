using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using MediatR;
using Moq;
using NSubstitute;

namespace DevFreela.Tests.Application
{
    public class InsertProjectHandlerTests
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Success_NSubstitute()
        {
            var repository = Substitute.For<IProjectRepository>();
            var mediator = Substitute.For<IMediator>();

            repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(1));

            var command = new InsertProjectCommand
            {
                Title = "Projeto Teste",
                Description = "Descrição do Projeto Teste",
                TotalCost = 10000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var handler = new InsertProjectHandler(mediator,repository);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data);
            await repository.Received(1).Add(Arg.Any<Project>());
        }


        [Fact]
        public async Task InputDataAreOk_Insert_Success_Moq()
        {
            //var mock = new Mock<IProjectRepository>();
            //mock.Setup(r => r.Add(It.IsAny<Project>())).ReturnsAsync(1);

            var repository = Mock.Of<IProjectRepository>(r => r.Add(It.IsAny<Project>()) == Task.FromResult(1));
            var mediator = Mock.Of<IMediator>();

            var command = new InsertProjectCommand
            {
                Title = "Projeto Teste",
                Description = "Descrição do Projeto Teste",
                TotalCost = 10000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var handler = new InsertProjectHandler(mediator, repository);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data);

            //Utilizando Setup
            //mock.Verify(m => m.Add(It.IsAny<Project>()), Times.Once);

            //Utilizando Mock.Of
            Mock.Get(repository).Verify(m => m.Add(It.IsAny<Project>()), Times.Once);
        }
    }
}
