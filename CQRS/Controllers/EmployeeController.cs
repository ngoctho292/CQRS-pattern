using CQRS.Models;
using CQRS.AppAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.AppAPI.Commands;

namespace CQRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetData() => await _mediator.Send(new GetData.Query());

        [HttpGet("{id}")]
        public async Task<Employee> GetDataById(int id) => await _mediator.Send(new GetDataById.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateData.Command command)
        {
            var createEmployeeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetData), new { id = createEmployeeId }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee( int id, UpdateData.Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteData(int id)
        {
            await _mediator.Send(new DeleteData.Command { Id = id });
            return NoContent();
        }
    }
}
