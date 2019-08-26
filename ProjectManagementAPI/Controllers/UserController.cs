using ProjectManagementAPI.DataAccess;
using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagementAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [ActionName("Search")]
        public HttpResponseMessage SearchUser(string userName = null, string sortField = null, bool? ascending = null)
        {
            try
            {
                Repository repository = new Repository();
                bool isAscending = ascending.HasValue? ascending.Value : true;
                return Request.CreateResponse(HttpStatusCode.OK, repository.SearchUserList(userName, sortField, isAscending));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetUser")]
        public HttpResponseMessage GetUser(int userID)
        {
            try
            {
                Repository repository = new Repository();                
                return Request.CreateResponse(HttpStatusCode.OK, repository.GetUser(userID));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("Delete")]
        public HttpResponseMessage DeleteUser(int userID)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.DeleteUser(userID));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPut]
        [ActionName("Update")]
        public HttpResponseMessage UpdateUser([FromBody]UserModel user)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.UpdateUser(user));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Add")]
        public HttpResponseMessage AddUser([FromBody]UserModel user)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.AddUser(user));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
