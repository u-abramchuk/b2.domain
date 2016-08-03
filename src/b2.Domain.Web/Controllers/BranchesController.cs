using System;
using System.Threading.Tasks;
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
        public async Task<string> Create([FromBody] CreateBranchCommand command)
        {
            try
            {
                await _handler.Handle(command);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}