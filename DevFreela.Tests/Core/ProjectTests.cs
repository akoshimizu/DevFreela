using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Tests.Core
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Success()
        {
            var project = new Project("Projeto Teste", "Descrição Teste", 1, 2, 5000);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.True(project.Status == ProjectStatusEnum.InProgress);
        }

        [Fact]
        public void ProjectIsInInvalidState_Start_ThrowException()
        {
            var project = new Project("Projeto Teste", "Descrição Teste", 1, 2, 5000);
            project.Start();

            //Act + Assert
            Action? start = project.Start;
            var exception = Assert.Throws<InvalidOperationException>(start);

            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }
    }
}
