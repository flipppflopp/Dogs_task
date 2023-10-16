using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DB.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Features.AddDog;
using Services.Features.GetDogs;
using Services.Interfaces;
using Services.Services;

namespace Dogs.Controllers
{
    [ApiController]
    [Route("")]
    public class DogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> Ping()
        {
            return Content("Dogshouseservice.Version1.0.1", "text/plain");
        }

        [HttpGet]
        [Route("dogs")]
        public async Task<IActionResult> Get([FromQuery]GetDogs.Command cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
        
        [HttpPost]
        [Route("dog")]
        public async Task<IActionResult> Post(AddDog.Command cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}