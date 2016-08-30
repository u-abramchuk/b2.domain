using System;
using b2.Domain.Entities;
using Xunit;

namespace b2.Domain.Tests.Entities
{
    public class WorkspaceTests
    {
        [Fact]
        public void CreateWorkspace()
        {
            var id = Guid.NewGuid();
            var name = "workspace";
            var workspace = new Workspace(id, name);

            Assert.Equal(id, workspace.Id);
            Assert.Equal(name, workspace.Name);
        }
    }
}