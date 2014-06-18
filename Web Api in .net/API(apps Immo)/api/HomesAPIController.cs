using CodedHomes.Data;
using CodedHomes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodedHomes.Web.Controllers
{
    [Authorize]
    public class HomesAPIController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();
        //Get
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Home> Get()
        {
            return this._unit.Homes.GetAll();
        }
        //GEet by id
        [HttpGet]
        [System.Web.Http.Authorize(Roles = "admin, manger, user")]
        public Home Get(int id)
        {
            Home home = this._unit.Homes.GetById(id);
            if (home == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return home;
        }

        //Update
        [System.Web.Http.Authorize(Roles = "admin, manger, user")]
        public HttpResponseMessage Put(int id, Home home)
        {
            if (!ModelState.IsValid)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != home.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Home existingHome = this._unit.Homes.GetById(id);
            _unit.Homes.Detach(existingHome);

            // Keep original CreatedOn value
            home.CreatedOn = existingHome.CreatedOn;

            this._unit.Homes.Update(home);

            try
            {
                this._unit.SaveChanges();

                // Return an explicit value to avoid the fail callback
                // being incorrectly invoked.
                return Request.
                    CreateResponse(HttpStatusCode.OK,
                    "{success:'true', verb:'PUT'}");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Post
        [HttpPost]
        [System.Web.Http.Authorize(Roles = "admin, manger, user")]
        public HttpResponseMessage Post(Home home)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unit.Homes.Add(home);
                    this._unit.SaveChanges();

                    HttpResponseMessage result =
                        Request.CreateResponse(HttpStatusCode.Created, home);

                    result.Headers.Location =
                        new Uri(Url.Link("DefaultApi", new { id = home.Id }));

                    return result;
                }
                else
                {
                    return Request.
                        CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Delete
        [HttpDelete]
        [System.Web.Http.Authorize(Roles = "admin, manager")]
        public HttpResponseMessage Delete(int id)
        {
            Home home = this._unit.Homes.GetById(id);

            if (home == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this._unit.Homes.Delete(home);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, home);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }
    }
}