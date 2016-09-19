// using System;
// using b2.Domain.Core;
// using b2.Domain.Entities;
// using Xunit;

// namespace b2.Domain.Tests.Entities
// {
//     public class WorkItemTests
//     {
//         [Fact]
//         public void CreateWorkItemFromTask()
//         {
//             var id = Guid.NewGuid();
//             var defaultStatus = "new";
//             var task = new Task(Guid.NewGuid(), "name", "http://task", "new");
//             var workItem = new WorkItem(id, task);

//             Assert.Equal(id, workItem.Id);
//             Assert.Equal(defaultStatus, workItem.Status);
//             Assert.Equal(task.Id, workItem.TaskId);
//         }

//         [Fact]
//         public void CreateWorkItemFromBranch()
//         {
//             var id = Guid.NewGuid();
//             var status = "new";
//             var branchId = Guid.NewGuid();
//             var branch = new Branch(branchId);
//             var workItem = new WorkItem(id, branch);

//             Assert.Equal(id, workItem.Id);
//             Assert.Equal(status, workItem.Status);
//             Assert.Equal(branchId, workItem.BranchId);
//         }

//         [Fact]
//         public void CannotChangeTask()
//         {
//             var id = Guid.NewGuid();
//             var initialTask = new Task(Guid.NewGuid(), "name", "http://task", "new");
//             var workItem = new WorkItem(id, initialTask);
//             var task = new Task(Guid.NewGuid(), "name", "http://task", "new");
//             var ex = Assert.Throws<DomainException>(() =>
//                 workItem.AssignTask(task)
//             );

//             Assert.Equal("Cannot change task", ex.Message);
//         }

//         [Fact]
//         public void CannotChangeBranch()
//         {
//             var id = Guid.NewGuid();
//             var workItem = new WorkItem(id, new Branch(Guid.NewGuid()));
//             var branch = new Branch(Guid.NewGuid());
//             var ex = Assert.Throws<DomainException>(() =>
//                 workItem.AssignBranch(branch)
//             );

//             Assert.Equal("Cannot change branch", ex.Message);
//         }

//         // [Fact]
//         // public void CannotChangeTaskStateWhenTaskIsNotSet()
//         // {
//         //     var workItem = new WorkItem("workitem-id", new Branch("branch-id1"));
//         //     var ex = Assert.Throws<InvalidOperationException>(() => 
//         //         workItem.SetTaskState("wrong")
//         //     );

//         //     Assert.Equal("Cannot change task state as task is not set", ex.Message);
//         // }
//     }
// }
