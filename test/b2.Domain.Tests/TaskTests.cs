using Xunit;

namespace b2.Domain.Tests
{
    public class TaskTests
    {
        [Fact]
        public void CreateTask()
        {
            var id = "task-id";
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
            var task = new Task("task-id", "task", "http://task", "new");
            var newStatus = "in progress";

            task.ChangeStatus(newStatus);

            Assert.Equal(newStatus, task.Status);
        }
    }
}