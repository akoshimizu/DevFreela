using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;
using NSubstitute;

namespace DevFreela.Tests.Application
{
    public class DeleteProjectHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Delete_Success_NSubstitute()
        {
            var project = new Project("Projeto A", "Descrição do Projeto A", 1, 2, 5000);
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            await repository.Received(1).GetById(1);
            await repository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_NSubstitute()
        {
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            var handler = new DeleteProjectHandler(repository);
            var command = new DeleteProjectCommand(1);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSuccess);
            Assert.Equal("Projeto não encontrado.", result.Message);
            await repository.Received(1).GetById(Arg.Any<int>());
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectExists_Delete_Success_Moq()
        {
            var project = new Project("Projeto A", "Descrição do Projeto A", 1, 2, 5000);
            
            var repository = Mock.Of<IProjectRepository>(p =>
                p.GetById(It.IsAny<int>()) == Task.FromResult(project) &&
                p.Update(It.IsAny<Project>()) == Task.CompletedTask
                );

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            Mock.Get(repository).Verify(r => r.GetById(1), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Once);

        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_Moq()
        {
            var repository = Mock.Of<IProjectRepository>(p =>
                p.GetById(It.IsAny<int>()) == Task.FromResult((Project?)null));

            var handler = new DeleteProjectHandler(repository);
            var command = new DeleteProjectCommand(1);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSuccess);
            Assert.Equal("Projeto não encontrado.", result.Message);

            Mock.Get(repository).Verify(r => r.GetById(1), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Never);

        }
    }
}
