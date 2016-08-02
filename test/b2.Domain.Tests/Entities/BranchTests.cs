using System;
using b2.Domain.Entities;
using Xunit;

namespace b2.Domain.Tests.Entities
{
    public class BranchTests
    {
        [Fact]
        public void CreateBranch()
        {
            var id = Guid.NewGuid();
            var branch = new Branch(id);

            Assert.Equal(id, branch.Id);
        }
    }
}