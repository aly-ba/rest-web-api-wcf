using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Route("")]
        [Route("api/test")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("{id}")]
        [Route("api/test/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(id);
        }

        
    }
}
