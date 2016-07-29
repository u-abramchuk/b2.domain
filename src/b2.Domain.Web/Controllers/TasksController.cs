using System;
using Microsoft.AspNetCore.Mvc;

namespace b2.Domain.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskCommandHandler _handler;
        public TasksController(TaskCommandHandler handler)
        {
            _handler = handler;
        }

        public IActionResult Test()
        {
            return new NoContentResult();
        }

        [HttpPost]
        public string Create([FromBody] CreateTaskCommand command)
        {
            try
            {
                _handler.Handle(command);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}