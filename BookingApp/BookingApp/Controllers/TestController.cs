using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class TestController : ApiController
    {
        BAContext ctx = new BAContext();

        [HttpGet]
        [Route("products")]
        public IHttpActionResult GetTest()
        {
            return Ok("Get test was successfull");
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}