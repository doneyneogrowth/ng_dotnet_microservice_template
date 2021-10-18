using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NgTemplate.API.DTOs;
using NgTemplate.API.DTOs.Enums;
using NgTemplate.API.Services;

namespace NgTemplate.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }
        [Produces("application/json")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(IEnumerable<Demo>), 200)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _demoService.GetAllDemos();
            return Ok(response);
        }

        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(Demo), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await _demoService.GetDemo(id);
            if (response.InternalError)
            {
                return StatusCode(500, response.Message);
            }
            if (response.NotFound)
            {
                return NotFound();
            }
            return Ok(response.Data as Demo);
        }

        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(Demo), 200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> Post(Demo demo)
        {
            var response = await _demoService.AddDemo(demo);
            if (response.InternalError)
            {
                return StatusCode(500, response.Message);
            }
            return Ok(response.Data as Demo);
        }

        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(Demo), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut]
        public async Task<ActionResult> Put(Demo demo)
        {
            var response = await _demoService.UpdateDemo(demo);
            if (response.InternalError)
            {
                return StatusCode(500, response.Message);
            }
            if (response.NotFound)
            {
                return NotFound();
            }
            return Ok(response.Data as Demo);
        }

        [Produces("application/json")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _demoService.DeleteDemo(id);
            if (response == ResourceOperationResult.Success)
            {
                return Ok();
            }
            if (response == ResourceOperationResult.NotFound)
            {
                return NotFound();
            }
            return StatusCode(500);
        }
    }
}