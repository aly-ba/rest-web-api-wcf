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
        
        public int Get(int id)
        {
            return id;
        }


        ////work with query string
        ////api/test/50 will not work 
        //public int Get(string number)
        //{
        //    return number;
        //}

        ////work for Query  string to string 
        //public int GetNumber(int id)
        //{
        //    return id;
        //}
    }
}
