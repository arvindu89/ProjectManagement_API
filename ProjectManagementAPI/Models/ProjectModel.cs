using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAPI.Models
{
    public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> NoOfTask { get; set; }
        public Nullable<int> CompletedTask { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
    }
}