using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagementAPI.Controllers;
using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers.Tests
{
    [TestClass()]
    public class TaskControllerTests
    {
        [TestMethod()]
        public void SearchTaskTest()
        {
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.SearchTask();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void GetTaskTest()
        {
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetTask(0);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void GetParentListTest()
        {
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetParentList();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void AddParentTaskTest()
        {
            ParentTaskModel parenttask = new ParentTaskModel();

            
            parenttask.ParentTaskName = "Parent Task1test";

            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.AddParentTask(parenttask);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void UpdateTaskTest()
        {
            TaskModel task = new TaskModel();
            task.TaskID = 1;
            task.TaskName = "Task 1";
            task.StartDate = DateTime.Now;
            task.EndDate = DateTime.Now.AddDays(2);
            task.Status = false;
            task.Priority = 10;
            task.ParentID = 1;
            task.ParentTaskName = "Parent Task1";
            
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.UpdateTask(task);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void AddTaskTest()
        {
            TaskModel task = new TaskModel();
            task.TaskID = 1;
            task.TaskName = "Task 1test";
            task.StartDate = DateTime.Now;
            task.EndDate = DateTime.Now.AddDays(2);
            task.Status = false;
            task.Priority = 10;
            task.ParentID = 1;
            task.ParentTaskName = "Parent Task1";

            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.AddTask(task);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }
    }
}