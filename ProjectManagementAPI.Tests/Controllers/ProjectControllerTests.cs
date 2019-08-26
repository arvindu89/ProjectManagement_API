using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagementAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementAPI.Models;
using System.Net.Http;

namespace ProjectManagementAPI.Controllers.Tests
{
    [TestClass()]
    public class ProjectControllerTests
    {
        [TestMethod()]
        public void SearchProjectTest()
        {
            ProjectController controller = new ProjectController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.SearchProject();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void GetProjectTest()
        {
            ProjectController controller = new ProjectController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetProject(0);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void DeleteProjectTest()
        {
            ProjectController controller = new ProjectController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.DeleteProject(0);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void UpdateProjectTest()
        {
            ProjectModel projectModel = new ProjectModel();
            projectModel.ProjectID = 1;
            projectModel.ProjectName = "Project 1";
            projectModel.StartDate = DateTime.Now;
            projectModel.EndDate = DateTime.Now.AddDays(2);
            projectModel.Priority = 7;
            projectModel.UserID = 1;
            
            ProjectController controller = new ProjectController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.UpdateProject(projectModel);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void AddProjectTest()
        {
            ProjectModel projectModel = new ProjectModel();            
            projectModel.ProjectName = "Project 1 test";
            projectModel.StartDate = DateTime.Now;
            projectModel.EndDate = DateTime.Now.AddDays(2);
            projectModel.Priority = 7;
            projectModel.UserID = 1;

            ProjectController controller = new ProjectController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.AddProject(projectModel);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }
    }
}