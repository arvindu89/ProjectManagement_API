using ProjectManagementAPI.DataAccess;
using ProjectManagementAPI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagementAPI.Controllers
{
    public class TaskController : ApiController
    {
        [HttpGet]
        [ActionName("Search")]
        public HttpResponseMessage SearchTask(int? projectID = null, string sortField = null, bool? ascending = null)
        {
            try
            {
                Repository repository = new Repository();
                bool isAscending = ascending.HasValue ? ascending.Value : true;
                return Request.CreateResponse(HttpStatusCode.OK, repository.SearchTask(projectID, sortField, isAscending));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetTask")]
        public HttpResponseMessage GetTask(int taskID)
        {
            try
            {
                Repository repository = new Repository();                
                return Request.CreateResponse(HttpStatusCode.OK, repository.GetTask(taskID));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [ActionName("GetParentList")]
        public HttpResponseMessage GetParentList(string parent = null)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.GetParentList(parent));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }


        [HttpPost]
        [ActionName("AddParent")]
        public HttpResponseMessage AddParentTask([FromBody]ParentTaskModel parentTaskModel)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.AddParentTask(parentTaskModel));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPut]
        [ActionName("UpdateTask")]
        public HttpResponseMessage UpdateTask([FromBody]TaskModel task)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.UpdateTask(task));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("AddTask")]
        public HttpResponseMessage AddTask([FromBody]TaskModel task)
        {
            try
            {
                Repository repository = new Repository();
                return Request.CreateResponse(HttpStatusCode.OK, repository.AddNewTask(task));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }

}
