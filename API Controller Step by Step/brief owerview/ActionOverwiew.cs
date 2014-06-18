using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class TestController : ApiController
    {

        //public HttpResponseMessage Get()
        //{
        //   //return  Request.CreateResponse(HttpStatusCode.OK);

        //   //return Request.CreateResponse(HttpStatusCode.NotFound);

        //   return Request.CreateResponse(HttpStatusCode.NotFound, 250);
        //}

        public class Sample
        {
            public int ID { get; set; }

            public string Description { get; set; }

        }
 

        public IHttpActionResult Get()
        {
            var sample = new Sample { ID = 10, Description = "Test Description" };
           return Ok(sample);
        }


        //public IHttpActionResult Post(int id)
        //{
        //    return Ok(id);
        //}

        /// <summary>
        /// Peut se faire à conditoin , de configurer le media type sinon error 415
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        //public IHttpActionResult Post([FromBody]Sample s)
        //{
        //    return Ok(s.ID);
        //}





        public IHttpActionResult Post([FromUri]Sample s)
        {
            return Ok(s.ID);
        }
    }
}
