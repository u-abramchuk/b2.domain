// using b2.Domain.Commands;
// using b2.Domain.Core;
// using b2.Domain.Entities;

// namespace b2.Domain.CommandHandlers
// {
//     public class WorkItemCommandHandler
//     {
//         private readonly Repository _repository;

//         public WorkItemCommandHandler(Repository repository)
//         {
//             _repository = repository;
//         }

//         public async System.Threading.Tasks.Task Handle(CreateWorkItemFromTaskCommand command)
//         {
//             var task = await _repository.GetById<Entities.Task>(command.TaskId);

//             var workItem = new WorkItem(command.WorkItemId, task);

//             await _repository.Save(workItem);
//         }

//         public async System.Threading.Tasks.Task Handle(CreateWorkItemFromBranchCommand command)
//         {
//             var branch = await _repository.GetById<Branch>(command.BranchId);

//             var workItem = new WorkItem(command.Id, branch);

//             await _repository.Save(workItem);
//         }

//         public async System.Threading.Tasks.Task Handle(AssignTaskToWorkItemCommand command)
//         {
//             var task = await _repository.GetById<Entities.Task>(command.TaskId);
//             var workItem = await _repository.GetById<WorkItem>(command.Id);

//             workItem.AssignTask(task);

//             await _repository.Save(workItem);
//         }
//     }
// }