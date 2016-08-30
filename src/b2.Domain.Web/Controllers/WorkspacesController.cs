using System;
using System.Threading.Tasks;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace b2.Domain.Web.Controllers
{
    public class WorkspacesController : Controller
    {
        private readonly WorkspaceCommandHandler _handler;

        public WorkspacesController(WorkspaceCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkspaceCommand command)
        {
            return await ProcessCommand(async () => await _handler.Handle(command));
        }

        private async Task<IActionResult> ProcessCommand(Func<Task<Guid>> action)
        {
            try
            {
                var id = await action();

                return Success(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult Success(Guid id)
        {
            return Json(new
            {
                id = id,
                status = "ok"
            });
        }
    }
}