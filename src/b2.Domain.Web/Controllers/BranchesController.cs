using System;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace b2.Domain.Web.Controllers
{
    public class BranchesController : Controller
    {
        private readonly BranchCommandHandler _handler;
        
        public BranchesController(BranchCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public string Create([FromBody] CreateBranchCommand command)
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