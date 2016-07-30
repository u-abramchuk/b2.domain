using Xunit;

namespace b2.Domain.Tests
{
    public class BranchTests
    {
        [Fact]
        public void CreateBranch()
        {
            var id = "branch-id";
            var branch = new Branch(id);

            Assert.Equal(id, branch.Id);
        }
    }
}