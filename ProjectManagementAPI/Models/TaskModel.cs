using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAPI.Models
{
    public class TaskModel
    {
        public int? TaskID { get; set; }
        public int? ParentID { get; set; }
        public int? ProjectID { get; set; }
        public string TaskName { get; set; }
        public string ParentTaskName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public bool Status { get; set; }
        public string ProjectName { get; set; }        
        public int? UserID { get; set; }
        public string UserName { get; set; }
        
        
        
    }
}