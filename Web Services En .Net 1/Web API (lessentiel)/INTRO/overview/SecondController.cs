using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HelloWebAPI
{
    public class SecondController : ApiController
    {
        public String Get()
        {
            return "This is the second controller";
        }

    }
}