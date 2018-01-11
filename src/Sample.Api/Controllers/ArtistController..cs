using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Sample.Api.Attributes;
using Sample.Api.Events;
using Sample.Api.Models;
using Sample.Api.Models.Errors;
using Sample.Api.Models.Requests;
using Sample.Api.Models.Validations;
using Sample.Api.Notifications;
//using Sample.Contract.Services;
//using Sample.Object.Domains;
using Ping = Sample.Api.Events.Ping;
using PingHandler = Sample.Api.Events.PingHandler;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    public class ArtistController : Controller
    {
        //private readonly IArtistService _artistService;
        //private readonly IUserService _userService;
        //private readonly IMediator _mediator;

        //public ArtistController(IArtistService service, IUserService userService)
        // public ArtistController(IArtistService service, IMediator mediator)
        // {
        //     _artistService = service;
        //     //_userService = userService;
        //     _mediator = mediator;
        // }

        // GET api/artist
        /// <summary>
        /// Get all artists
        /// </summary>
        /// <returns></returns>
        // [HttpGet]
        // public IEnumerable<ArtistModel> Get()
        // {
        //     return Mapper.Map<IEnumerable<ArtistModel>>(_artistService.Get());
        // }

        // GET api/artist/5
        /// <summary>
        /// Get artist by id
        /// </summary>
        /// <param firstName="id"></param>
        /// <returns></returns>
        // [HttpGet("{id}")]
        // public ArtistModel Get(long id)
        // {
        //     return Mapper.Map<ArtistModel>(_artistService.Get(id));
        // }

        // POST api/artist
        /// <summary>
        /// Create a new artist
        /// </summary>
        /// <param firstName="model"></param>
        // [HttpPost]
        // [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        // [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status500InternalServerError)]
        // [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status422UnprocessableEntity)]
        // [ValidateModel]
        // public async Task<IActionResult> Post([FromBody] ArtistPostModel model)
        // {
        //     try
        //     {
        //         //await _userService.CreateAsync("test@email.com");

        //         //throw new Exception("dasdsad");
        //         /*ArtistDto payload = Mapper.Map<ArtistDto>(model);

        //         _artistService.Create(payload);

        //         return Accepted(payload);*/

        //         var response = await _mediator.Send(new Ping());
        //         Debug.WriteLine(response); // "Pong"
               
        //         await _mediator.Send(new OneWay());
                
        //         await _mediator.Publish(new PingNotification());
                
        //         //await _mediator.Publish(new PingHandlerAsync() { Message = "Ping" });
                
        //         return Accepted();
        //     }
        //     catch (ArgumentException e)
        //     {
        //         return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
        //     }
        // }

        // PUT api/artist/5
        /// <summary>
        /// Modify existing artist
        /// </summary>
        /// <param firstName="id"></param>
        /// <param firstName="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/artist/5
        /// <summary>
        /// Remove existing artist
        /// </summary>
        /// <param firstName="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}