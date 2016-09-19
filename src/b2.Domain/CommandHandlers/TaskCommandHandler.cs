// using b2.Domain.Commands;
// using b2.Domain.Core;
// using b2.Domain.Entities;

// namespace b2.Domain.CommandHandlers
// {
//     public class TaskCommandHandler
//     {
//         private readonly Repository _repository;

//         public TaskCommandHandler(Repository repository)
//         {
//             _repository = repository;
//         }

//         public async System.Threading.Tasks.Task Handle(CreateTaskCommand command)
//         {
//             var task =  new Task(command.Id, command.Name, command.Url, command.Status);

//             await _repository.Save(task);
//         }
//     }
// }