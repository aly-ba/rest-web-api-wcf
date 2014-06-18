using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TrainingCompany.Controllers
{
    public class CoursesController : ApiController
    {

        //reuse and rename the get on Allcourses
        [HttpGet]
        public IEnumerable<course> AllCourses()
        {
            return courses;

        }

        //public IEnumerable<course> Get()
        //{
            //return courses;
        //}

        //get  courses by id
        public course Get(int id)
        {
            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();
            //if null - I Should return a 404
            return ret;
        }

        //adding a new courses with HttpreponseMessage
        public HttpResponseMessage Post([FromBody] course c)
        {
            c.id = courses.Count;
            courses.Add(c);
            //I should return a 201 with a location header
            var msg = Request.CreateResponse(
                HttpStatusCode.Created);
            msg.Headers.Location =
                new Uri(Request.RequestUri + c.id.ToString());
            return msg;
        }
        
        //adding a new courses
        //public void post([FromBody] course c)
        //{
            //c.id = courses.Count;
            //courses.Add(c);
            //I should return a 201 with a location header
        }

        //update cource
        public void Put(int id, course course)
        {
            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();
                    
             ret.title = course.title;
        }
        //delete a course
        public void delete(int id)
        {
            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();
            courses.Remove(ret);
        }

        static List<course> courses =InitCourses();

        public static List<course> InitCourses() 
        {
            var ret = new List<course>();
            ret.Add(new course {id=0, title="Web ApI"});
            ret.Add(new course {id =1, title ="Mobile Apps with HTML5"});
            return ret;


        }


    }

    //class course
    //lowercase for naturel JSON
    public class course
    {
        public int id;
        public string title;
    }


}
