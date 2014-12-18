using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace CalarieEtSportMVC.Api
{
    public class UserService : Service
    {
        public object POST(AddUser request)
        {
            //Add the user
            return new AddUserResponse { UserId = 255 };
        }
             
    }


    public class AddUserResponse
    {
        public long UserId { get; set; }

    }

    [Route("/users", "POST")]
    public class AddUser
    {
        public string Name { get; set; }
        public int Goal { get; set; }
    }

   
}