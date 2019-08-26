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
    public class ProjectController : ApiController
    {
        [HttpGet]
        [ActionName("Search")]
        public HttpResponseMessage SearchProject(string projectName = null, string sortField = null, bool? ascending = null)
        {
            try
            {
                Repository repository = new Repository();
                bool isAscending = ascending.HasValue ? ascending.Value : true;
                return Request.CreateResponse(HttpStatusCode.OK, repository.SearchProjectList(projectName, sortField, isAscending));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }


        [HttpGet]
        [ActionName("GetProject")]
        public HttpResponseMessage GetProject(int projectID)
        {
            try
            {
                Repository repository = new Repository();               
                return Request.CreateResponse(HttpStatusCode.OK, repository.GetProject(projectID));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("Delete")]
        public HttpResponseMessage DeleteProject(int projectID)
        {
            try
            {
                Repository repository = new Repository();                
                return Request.CreateResponse(HttpStatusCode.OK, repository.DeleteProject(projectID));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPut]
        [ActionName("Update")]
        public HttpResponseMessage UpdateProject([FromBody]ProjectModel project)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.UpdateProject(project));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Add")]
        public HttpResponseMessage AddProject([FromBody]ProjectModel project)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.AddProject(project));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
