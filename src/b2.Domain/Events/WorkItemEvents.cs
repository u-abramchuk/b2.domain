// using System;
// using b2.Domain.Core;

// namespace b2.Domain.Events
// {
//      public class WorkItemCreatedFromTask : Event
//     {
//         public WorkItemCreatedFromTask(Guid id, Guid taskId) : base(id)
//         {
//             TaskId = taskId;
//         }

//         public Guid TaskId { get; }
//     }

//     public class WorkItemCreatedFromBranch : Event
//     {
//         public WorkItemCreatedFromBranch(Guid id, Guid branchId) : base(id)
//         {
//             BranchId = branchId;
//         }

//         public Guid BranchId { get; }
//     }

//     public class TaskAssignedToWorkItem : Event
//     {
//         public TaskAssignedToWorkItem(Guid id, Guid taskId) : base(id)
//         {
//             TaskId = taskId;
//         }

//         public Guid TaskId { get; }
//     }

//     public class BranchAssignedToWorkItem : Event
//     {
//         public BranchAssignedToWorkItem(Guid id, Guid branchId) : base(id)
//         {
//             BranchId = branchId;
//         }

//         public Guid BranchId { get; }
//     }
// }