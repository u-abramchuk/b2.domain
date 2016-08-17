using System;
using System.Threading.Tasks;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace b2.Domain.Web.Controllers
{
    public class WorkItemsController : Controller
    {
        private readonly WorkItemCommandHandler _handler;

        public WorkItemsController (WorkItemCommandHandler handler)
        {
          _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFromTask([FromBody] CreateWorkItemFromTaskCommand command)
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

        [HttpPost]
        public async Task<IActionResult> CreateFromBranch([FromBody] CreateWorkItemFromBranchCommand command)
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

        [HttpPost]
        public async Task<IActionResult> AssignTask([FromBody] AssignTaskToWorkItemCommand command)
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