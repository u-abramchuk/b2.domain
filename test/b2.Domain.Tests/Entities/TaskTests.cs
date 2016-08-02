using System;
using b2.Domain.Entities;
using Xunit;

namespace b2.Domain.Tests.Entities
{
    public class TaskTests
    {
        [Fact]
        public void CreateTask()
        {
            var id = Guid.NewGuid();
            var name = "task";
            var url = "http://task";
            var status = "new";
            var task = new Task(id, name, url, status);

            Assert.Equal(id, task.Id);
            Assert.Equal(name, task.Name);
            Assert.Equal(url, task.Url);
            Assert.Equal(status, task.Status);
        }

        [Fact]
        public void ChangeTaskStatus()
        {
            var task = new Task(Guid.NewGuid(), "task", "http://task", "new");
            var newStatus = "in progress";

            task.ChangeStatus(newStatus);

            Assert.Equal(newStatus, task.Status);
        }
    }
}