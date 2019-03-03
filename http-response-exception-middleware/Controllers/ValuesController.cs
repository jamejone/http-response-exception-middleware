using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using http_response_exception_middleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace http_response_exception_middleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            throw new BadRequestException("Oh dear...");
            
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            throw new NotFoundException("Huh?");

            //return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm] string value)
        {
            throw new Exception("Not an IHttpException, so will result in an internal server error.");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                // Here's an example of what NOT to do.
                // Everything in this if statement could be replaced with:
                // throw new BadRequestException("value is a required parameter");
                var responseObject = new {
                    message = "value is a required parameter"
                };
                string responseBody = Newtonsoft.Json.JsonConvert.SerializeObject(responseObject);
                return BadRequest(responseBody);
                // throw new BadRequestException("value is a required parameter");
            }

            return Ok();
        }
    }
}
