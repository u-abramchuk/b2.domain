// using b2.Domain.Commands;
// using b2.Domain.Core;
// using b2.Domain.Entities;

// namespace b2.Domain.CommandHandlers
// {
//     public class BranchCommandHandler
//     {
//         private readonly Repository _repository;
//         public BranchCommandHandler(Repository repository)
//         {
//             _repository = repository;
//         }

//         public async System.Threading.Tasks.Task Handle(CreateBranchCommand command)
//         {
//             var branch = new Branch(command.Id);

//             await _repository.Save(branch);
//         }
//     }
// }