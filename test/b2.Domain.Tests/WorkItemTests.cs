using System;
using Xunit;

namespace b2.Domain.Tests
{
    public class WorkItemTests
    {
        [Fact]
        public void CreateWorkItemFromTask()
        {
            var id = "test-id";
            var defaultStatus = "new";
            var task = new Task("task-id", "name", "http://task", "new");
            var workItem = new WorkItem(id, task);

            Assert.Equal(id, workItem.Id);
            Assert.Equal(defaultStatus, workItem.Status);
            Assert.Equal(task.Id, workItem.TaskId);
        }

        [Fact]
        public void CreateWorkItemFromBranch()
        {
            var id = "test-id";
            var status = "new";
            var branchId = "branch-id";
            var branch = new Branch(branchId);
            var workItem = new WorkItem(id, branch);

            Assert.Equal(id, workItem.Id);
            Assert.Equal(status, workItem.Status);
            Assert.Equal(branchId, workItem.BranchId);
        }

        [Fact]
        public void CannotChangeTask()
        {
            var id = "test-id";
            var initialTask = new Task("task-id1", "name", "http://task", "new");
            var workItem = new WorkItem(id, initialTask);
            var task = new Task("task-id2", "name", "http://task", "new");
            var ex = Assert.Throws<InvalidOperationException>(() =>
                workItem.AssignTask(task)
            );

            Assert.Equal("Cannot change task", ex.Message);
        }

        [Fact]
        public void CannotChangeBranch()
        {
            var id = "test-id";
            var workItem = new WorkItem(id, new Branch("branch-id1"));
            var branch = new Branch("branch-id2");
            var ex = Assert.Throws<InvalidOperationException>(() =>
                workItem.AssignBranch(branch)
            );

            Assert.Equal("Cannot change branch", ex.Message);
        }

        // [Fact]
        // public void CannotChangeTaskStateWhenTaskIsNotSet()
        // {
        //     var workItem = new WorkItem("workitem-id", new Branch("branch-id1"));
        //     var ex = Assert.Throws<InvalidOperationException>(() => 
        //         workItem.SetTaskState("wrong")
        //     );

        //     Assert.Equal("Cannot change task state as task is not set", ex.Message);
        // }
    }
}
