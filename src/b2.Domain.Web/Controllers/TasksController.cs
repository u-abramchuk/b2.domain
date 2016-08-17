using System;
using System.Threading.Tasks;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        {
            try
            {
                await _handler.Handle(command);

                return Content("ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}