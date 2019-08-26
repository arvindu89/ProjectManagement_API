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
    public class UserControllerTests
    {
        [TestMethod()]
        public void SearchUserTest()
        {
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.SearchUser();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void GetUserTest()
        {
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetUser(0);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.DeleteUser(0);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void UpdateUserTest()
        {

            UserModel userModel = new UserModel();
            userModel.FirstName = "User 1";
            userModel.LastName ="LasteName 1 test";
            userModel.ProjectID = 1;
            userModel.UserID = 1;            

            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.UpdateUser(userModel);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }

        [TestMethod()]
        public void AddUserTest()
        {
            UserModel userModel = new UserModel();
            userModel.FirstName = "User 1";
            userModel.LastName = "LasteName 1 test";
            userModel.ProjectID = 1;
            
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.AddUser(userModel);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, searchResultResponse.StatusCode);
        }
    }
}