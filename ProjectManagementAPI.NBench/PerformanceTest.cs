using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NBench;
using NBench.Util;
using ProjectManagementAPI.Controllers;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.NBench
{
    public class PerformanceTest
    {
        private Counter _counter;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }
        
        [PerfBenchmark(Description ="Test User Search performance",NumberOfIterations =13,RunMode =RunMode.Throughput,
            RunTimeMilliseconds =1000,SkipWarmups =false,TestMode =TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [ElapsedTimeAssertion(MaxTimeMilliseconds =1500)]
        public void SearchUserPerformance()
        {
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.SearchUser();
        }

        [PerfBenchmark(Description = "Fetch UserDetails", NumberOfIterations = 13, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, SkipWarmups = false, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetUserPerformance()
        {
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetUser(1);
        }

        [PerfBenchmark(Description = "Search Project Performance", NumberOfIterations = 13, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, SkipWarmups = false, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1500)]
        public void SearchProjectPerformance()
        {
            ProjectController controller = new ProjectController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.SearchProject();            
        }

        [PerfBenchmark(Description = "Get Project Performance", NumberOfIterations = 13, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, SkipWarmups = false, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetProjectPerformance()
        {
            ProjectController controller = new ProjectController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetProject(1);            
        }

        [PerfBenchmark(Description = "Search Task Performance", NumberOfIterations = 13, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, SkipWarmups = false, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1500)]
        public void SearchTaskPerformance()
        {
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.SearchTask();            
        }

        [PerfBenchmark(Description = "Get Task Performance", NumberOfIterations = 13, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, SkipWarmups = false, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1500)]
        public void GetTaskPerformance()
        {
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetTask(0);            
        }

        [PerfBenchmark(Description = "Get Task Performance", NumberOfIterations = 13, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, SkipWarmups = false, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1500)]
        public void GetParentListPerformance()
        {
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            HttpResponseMessage searchResultResponse = new HttpResponseMessage();
            searchResultResponse = controller.GetParentList();            
        }

        [PerfCleanup]
        public void Cleanup()
        {

        }               
    }
}
