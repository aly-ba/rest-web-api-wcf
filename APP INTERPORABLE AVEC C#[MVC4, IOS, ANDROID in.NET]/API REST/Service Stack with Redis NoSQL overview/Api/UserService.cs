using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace CalarieEtSportMVC.Api
{
    public class UserService : Service
    {
        public IRepository Repository { get; set; }

        public object Post(AddUser request)
        {
            var id = Repository.AddUser(request.Name, request.Goal);
            //Add the user
            return new AddUserResponse { UserId = 255 };
        }

        public object Get(UserService request)
        {
            return  new UsersResponse{Users = Repository.GetUsers()};
        }

        public object Post(AddCalories request)
        {
            var user = Repository.GetUsers(request.UserId);
            user.Total +=request.Amount;
            Repository.UpdateUse(user);
            return new AddCaloriesResponse{NewTotal = user.Total};
        }
             
    }

    public class AddCalories
    {
        public long UserId { get; set; }
    }

    [Route("/user/{userid}", "POST")]
    public class UsersResponse
    {
        public IEnumerable<User> Users { get; set; }
        public int Amount { get; set; }
    }

    [Route("/users", "GET")]
    public class Users
    {

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