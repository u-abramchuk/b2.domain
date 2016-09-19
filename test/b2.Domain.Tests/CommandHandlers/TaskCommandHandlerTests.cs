// using System;
// using System.Threading.Tasks;
// using b2.Domain.CommandHandlers;
// using b2.Domain.Commands;
// using b2.Domain.Events;
// using Xunit;

// namespace b2.Domain.Tests.CommandHandlers
// {
//     public class TaskCommandHandlerTests : BaseCommandHandlerTests
//     {
//         private readonly TaskCommandHandler _handler;

//         public TaskCommandHandlerTests()
//         {
//             _handler = new TaskCommandHandler(Repository);
//         }

//         public async Task CreateTask()
//         {
//             var id = Guid.NewGuid();
//             var name = "task";
//             var url = "http://task";
//             var status = "new";

//             var command = new CreateTaskCommand(id, name, url, status);

//             await _handler.Handle(command);

//             var storedEvent = GetStoredEvent<TaskCreated>(id);

//             Assert.Equal(id, storedEvent.Id);
//             Assert.Equal(name, storedEvent.Name);
//             Assert.Equal(url, storedEvent.Url);
//             Assert.Equal(status, storedEvent.Status);

//             var publishedEvent = GetPublishedEvent<TaskCreated>(id);

//             Assert.Equal(id, publishedEvent.Id);
//             Assert.Equal(name, publishedEvent.Name);
//             Assert.Equal(url, publishedEvent.Url);
//             Assert.Equal(status, publishedEvent.Status);
//         }
//     }
// }