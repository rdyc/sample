using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Application.Commands;
using Sample.Api.Application.Queries;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPersonQueries _personQueries;

        public PersonController(IMediator mediator, IPersonQueries personQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _personQueries = personQueries ?? throw new ArgumentNullException(nameof(personQueries));
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var persons = await _personQueries.GetAsync();

                return Ok(persons);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Add new person
        /// </summary>
        /// <param firstName="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePersonCommand command)
        {
            bool commandResult = false;

            var id = Guid.NewGuid();
            var requestPersonCreate = new IdentifiedCommand<CreatePersonCommand, bool>(new CreatePersonCommand(command.FirstName, command.LastName, command.Email), id);
            commandResult = await _mediator.Send(requestPersonCreate);

            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();

            /* var result = await _mediator.Send(command);

            return result ? (IActionResult)Accepted() : (IActionResult)BadRequest(); */
        }


        /* [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] UpdatePersonCommand command, [FromHeader(FirstName = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestPersonUpdate = new IdentifiedCommand<UpdatePersonCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestPersonUpdate);
            }

            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        } */
    }
}