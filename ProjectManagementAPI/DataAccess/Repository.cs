using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAPI.DataAccess
{
    public class Repository
    {
        private ExampleData_USInsuranceEntities _entity;

        public Repository()
        {
            _entity = new ExampleData_USInsuranceEntities();
        }

        #region Task
        public List<TaskModel> SearchTask(int? projectID, string sortField, bool ascending)
        {
            var query = (from t in _entity.Tasks
                         join p in _entity.ParentTasks on t.Parent_ID equals p.Parent_ID into parentgp
                         from parent in parentgp.DefaultIfEmpty()
                         join pr in _entity.Projects on t.Project_ID equals pr.Project_ID into projectgp
                         from project in projectgp.DefaultIfEmpty()
                         join u in _entity.Users on t.Task_ID equals u.Task_ID into usergp
                         from usr in usergp.DefaultIfEmpty()
                         select new TaskModel()
                         {
                             TaskID = t.Task_ID,
                             TaskName = t.Task1,
                             EndDate = t.End_Date,
                             StartDate = t.Start_Date,
                             Status = t.Status,
                             ParentID = t.Parent_ID,
                             ParentTaskName = parent == null ? null : parent.Parent_Task,
                             Priority = t.Priority,
                             ProjectID = t.Project_ID,
                             ProjectName = project == null ? null : project.Project1,
                             UserID = usr == null ? null : (int?)usr.User_ID,
                             UserName = usr == null ? null : usr.First_Name + " " + usr.Last_Name
                         });

            if (projectID != null && projectID !=0)
            {
                query = query.Where(i => i.ProjectID == projectID);
            }

            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "startdate")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.StartDate);
                }
                else
                {
                    query = query.OrderByDescending(i => i.StartDate);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "enddate")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.EndDate);
                }
                else
                {
                    query = query.OrderByDescending(i => i.EndDate);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "priority")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.Priority);
                }
                else
                {
                    query = query.OrderByDescending(i => i.Priority);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "status")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.Status);
                }
                else
                {
                    query = query.OrderByDescending(i => i.Status);
                }
            }

            List<TaskModel> taskModelList = query.ToList();
            return taskModelList;
        }

        public TaskModel GetTask(int taskID)
        {
            var query = (from t in _entity.Tasks
                         join p in _entity.ParentTasks on t.Parent_ID equals p.Parent_ID into parentgp
                         from parent in parentgp.DefaultIfEmpty()
                         join pr in _entity.Projects on t.Project_ID equals pr.Project_ID into projectgp
                         from project in projectgp.DefaultIfEmpty()
                         join u in _entity.Users on t.Task_ID equals u.Task_ID into usergp
                         from usr in usergp.DefaultIfEmpty()
                         where t.Task_ID == taskID
                         select new TaskModel()
                         {
                             TaskID = t.Task_ID,
                             TaskName = t.Task1,
                             EndDate = t.End_Date,
                             StartDate = t.Start_Date,
                             Status = t.Status,
                             ParentID = t.Parent_ID,
                             ParentTaskName = parent == null? null : parent.Parent_Task,
                             Priority = t.Priority,
                             ProjectID = t.Project_ID,
                             ProjectName = project == null ? null : project.Project1,
                             UserID = usr == null ? null : (int?)usr.User_ID,
                             UserName = usr == null ? null : usr.First_Name + " " + usr.Last_Name
                         });

            return query.FirstOrDefault();
        }
        public List<ParentTaskModel> GetParentList(string parentTask)
        {
            var query = (from p in _entity.ParentTasks
                                                 select new ParentTaskModel
                                                 {
                                                     ParentID = p.Parent_ID,
                                                     ParentTaskName = p.Parent_Task
                                                 });
            if(!string.IsNullOrEmpty(parentTask))
            {
                query = query.Where(s => s.ParentTaskName.Contains(parentTask));
            }
            return query.ToList();
        }

        public bool AddParentTask(ParentTaskModel parentTaskModel)
        {
            try
            {
                ParentTask parentTask = new ParentTask();
                parentTask.Parent_Task = parentTaskModel.ParentTaskName;

                _entity.ParentTasks.Add(parentTask);
                _entity.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public int AddNewTask(TaskModel taskModel)
        {
            Task task = new Task();
            task.Task1 = taskModel.TaskName;
            task.Project_ID = taskModel.ProjectID;
            task.Status = taskModel.Status;
            task.Parent_ID = taskModel.ParentID;
            task.Priority = taskModel.Priority;
            task.Start_Date = taskModel.StartDate;
            task.End_Date = taskModel.EndDate;
            _entity.Tasks.Add(task);
            _entity.SaveChanges();

            User newUser = (from t in _entity.Users
                            where t.User_ID == taskModel.UserID
                            select t).FirstOrDefault();
            
            if (newUser != null)
            {
                newUser.Task_ID = task.Task_ID;
                newUser.Project_ID = taskModel.ProjectID;
            }

            _entity.SaveChanges();

            return task.Task_ID;
        }

        public bool UpdateTask(TaskModel taskModel)
        {
            Task task = (from t in _entity.Tasks
                         where t.Task_ID == taskModel.TaskID
                         select t).FirstOrDefault();

            User oldUser = (from t in _entity.Users
                         where t.Task_ID == taskModel.TaskID
                         select t).FirstOrDefault();
            User newUser = (from t in _entity.Users
                            where t.User_ID == taskModel.UserID
                            select t).FirstOrDefault();

            if (task != null)
            {
                task.Task1 = taskModel.TaskName;
                task.Status = taskModel.Status;
                task.Parent_ID = taskModel.ParentID;
                task.Project_ID = taskModel.ProjectID;
                task.Priority = taskModel.Priority;
                task.Start_Date = taskModel.StartDate;
                task.End_Date = taskModel.EndDate;

                if (oldUser != null)
                {
                    oldUser.Task_ID = null;
                    oldUser.Project_ID = null;
                }

                if (newUser != null)
                {
                    newUser.Task_ID = taskModel.TaskID;
                    newUser.Project_ID = taskModel.ProjectID;
                }

                _entity.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region Project
        public List<ProjectModel> SearchProjectList(string projectName, string sortField, bool ascending)
        {
            var query = (from p in _entity.Projects
                         join u in _entity.Users on p.Project_ID equals u.Project_ID into usergp
                         from usr in usergp.DefaultIfEmpty()
                         select new ProjectModel()
                         {
                             ProjectName = p.Project1,
                             EndDate = p.End_Date,
                             StartDate = p.Start_Date,
                             Priority = p.Priority,
                             ProjectID = p.Project_ID,
                             UserID = usr == null? null: (int?) usr.User_ID,
                             UserName = usr == null ? null : usr.First_Name + " " + usr.Last_Name,
                             NoOfTask = p.Tasks.Count(),
                             CompletedTask = p.Tasks.Where(s => s.Status == true).Count()
                         });

            if (!string.IsNullOrEmpty(projectName))
            {
                query = query.Where(s => s.ProjectName.Contains(projectName));
            }

            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "startdate")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.StartDate);
                }
                else
                {
                    query = query.OrderByDescending(i => i.StartDate);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "enddate")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.EndDate);
                }
                else
                {
                    query = query.OrderByDescending(i => i.EndDate);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "priority")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.Priority);
                }
                else
                {
                    query = query.OrderByDescending(i => i.Priority);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "completed")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.CompletedTask);
                }
                else
                {
                    query = query.OrderByDescending(i => i.CompletedTask);
                }
            }
            return query.ToList();
        }

        public ProjectModel GetProject(int projectID)
        {
            var query = (from p in _entity.Projects
                         join u in _entity.Users on p.Project_ID equals u.Project_ID into usergp
                         from usr in usergp.DefaultIfEmpty()
                         where p.Project_ID == projectID
                         select new ProjectModel()
                         {
                             ProjectName = p.Project1,
                             EndDate = p.End_Date,
                             StartDate = p.Start_Date,
                             Priority = p.Priority,
                             ProjectID = p.Project_ID,
                             UserID = usr == null ? null : (int?)usr.User_ID,
                             UserName = usr == null ? null : usr.First_Name + " " + usr.Last_Name,
                             NoOfTask = p.Tasks.Count(),
                             CompletedTask = p.Tasks.Where(s => s.Status == true).Count()
                         });

            return query.FirstOrDefault();
        }

        public bool DeleteProject(int projectID)
        {
            var project = _entity.Projects.Where(s => s.Project_ID == projectID).FirstOrDefault();
            if (project != null)
            {
                var user = _entity.Users.Where(s => s.Project_ID == project.Project_ID).FirstOrDefault();
                user.Project_ID = null;

                var tasks = _entity.Tasks.Where(s => s.Project_ID == project.Project_ID).ToList();
                foreach(Task task in tasks)
                {
                    task.Project_ID = null;
                }
                
                _entity.Projects.Remove(project);
                _entity.SaveChanges();

                return true;
            }
            return false;
        }

        public bool AddProject(ProjectModel projectModel)
        {
            Project project = new Project();

            if (projectModel != null)
            {
                project.Project1 = projectModel.ProjectName;
                project.End_Date = projectModel.EndDate;
                project.Start_Date = projectModel.StartDate;
                project.Priority = projectModel.Priority;
                _entity.Projects.Add(project);
                _entity.SaveChanges();

                User user = _entity.Users.Where(s => s.Project_ID == projectModel.ProjectID).FirstOrDefault();
                if (user != null)
                {
                    user.Project_ID = null;
                    _entity.SaveChanges();
                }

                user = _entity.Users.Where(s => s.User_ID == projectModel.UserID).FirstOrDefault();
                if (user != null)
                {
                    user.Project_ID = project.Project_ID;
                    _entity.SaveChanges();
                }             

                return true;
            }
            return false;
        }

        public bool UpdateProject(ProjectModel projectModel)
        {
            var project = _entity.Projects.Where(s => s.Project_ID == projectModel.ProjectID).FirstOrDefault();
             
            if (project != null)
            {
                project.Project1 = projectModel.ProjectName;
                project.End_Date = projectModel.EndDate;
                project.Start_Date = projectModel.StartDate;
                project.Priority = projectModel.Priority;
                _entity.SaveChanges();

                User user = _entity.Users.Where(s => s.Project_ID == projectModel.ProjectID).FirstOrDefault();
                if (user != null)
                {
                    user.Project_ID = null;
                    _entity.SaveChanges();
                }

                user = _entity.Users.Where(s => s.User_ID == projectModel.UserID).FirstOrDefault();
                if (user != null)
                {
                    user.Project_ID = project.Project_ID;
                    _entity.SaveChanges();
                }
                return true;
            }
            return false;
        }

        #endregion

        #region User
        public List<UserModel> SearchUserList(string userName, string sortField, bool ascending)
        {
            var query = (from p in _entity.Users
                         select new UserModel()
                         {
                             UserID = p.User_ID,
                             EmployeeID = p.Employee_ID,
                             FirstName = p.First_Name,
                             LastName = p.Last_Name,
                             ProjectID = p.Project_ID,
                             TaskID = p.Task_ID
                         });

            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(s => (s.FirstName.Contains(userName) || s.LastName.Contains(userName)));
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "firstname")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.FirstName);
                }
                else
                {
                    query = query.OrderByDescending(i => i.FirstName);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "lastname")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.LastName);
                }
                else
                {
                    query = query.OrderByDescending(i => i.LastName);
                }
            }
            if (!string.IsNullOrEmpty(sortField) && sortField.ToLower() == "employeeid")
            {
                if (ascending)
                {
                    query = query.OrderBy(i => i.EmployeeID);
                }
                else
                {
                    query = query.OrderByDescending(i => i.EmployeeID);
                }
            }

            return query.ToList();
        }

        public bool AddUser(UserModel userModel)
        {
            User user = new User();
            try
            {
                user.First_Name = userModel.FirstName;
                user.Last_Name = userModel.LastName;
                user.Employee_ID = userModel.EmployeeID;
                user.Project_ID = userModel.ProjectID != 0 ? userModel.ProjectID : null;
                user.Task_ID = userModel.TaskID != 0 ? userModel.TaskID : null;
                _entity.Users.Add(user);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(UserModel userModel)
        {
            var user = _entity.Users.Where(s => s.User_ID == userModel.UserID).FirstOrDefault();
            if (user != null)
            {
                user.First_Name = userModel.FirstName;
                user.Last_Name = userModel.LastName;
                user.Employee_ID = userModel.EmployeeID;
                user.Project_ID = userModel.ProjectID;
                user.Task_ID = userModel.TaskID;
                _entity.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            var user = _entity.Users.Where(s => s.User_ID == id).FirstOrDefault();
            if (user != null)
            {
                _entity.Users.Remove(user);
                _entity.SaveChanges();
                return true;
            }
            return false;
        }

        public UserModel GetUser(int id)
        {
            User user = _entity.Users.Where(s => s.User_ID == id).FirstOrDefault();
            if (user != null)
            {
                UserModel userModel = new UserModel();
                userModel.EmployeeID = user.Employee_ID;
                userModel.FirstName = user.First_Name;
                userModel.LastName = user.Last_Name;
                userModel.UserID = user.User_ID;
                userModel.ProjectID = user.Project_ID;
                userModel.TaskID = user.Task_ID;
                return userModel;
            }
            return null;
        }
        #endregion
    }
}