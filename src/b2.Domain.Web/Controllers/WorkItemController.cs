using System;
using Microsoft.AspNetCore.Mvc;

namespace b2.Domain.Web.Controllers
{
    [Route("api/[controller]")]
    public class WorkItemController : Controller
    {
        private readonly WorkItemCommandHandler _handler;

        public WorkItemController (WorkItemCommandHandler handler)
        {
          _handler = handler;
        }

        [HttpPost]
        public string CreateFromTask([FromBody] CreateWorkItemFromTaskCommand command)
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

        [HttpPost]
        public string CreateFromBranch([FromBody] CreateWorkItemFromBranchCommand command)
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

        [HttpPost]
        public string AssignTask([FromBody] AssignTaskToWorkItemCommand command)
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