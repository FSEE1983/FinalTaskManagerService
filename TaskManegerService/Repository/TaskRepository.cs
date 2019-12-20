using System.Collections.Generic;
using System.Linq;
using TaskManegerService.Models;
namespace TaskManegerService.Repository
{
    public class TaskRepository : ITaskRepository
    {

        public IEnumerable<Tasks> GetTask()
        {
            var listOfTasks = new List<Tasks>();
            using (var TaskDBcontext = new TaskDBEntities())
            {
                foreach (var task in TaskDBcontext.Tasks)
                {
                    listOfTasks.Add(new Tasks()
                    {
                        Task_ID = task.Task_ID,
                        TaskName = task.Task1,
                        Start_Date = task.Start_Date,
                        End_Date = task.End_Date,
                        Priority = task.Priority,                        
                        Parent_ID = task.Parent_ID,
                        ParentTask = task.Parent_ID.HasValue ? GetParentInfo(task.Parent_ID.Value) : null,
                        IsTaskEnded = task.IsTaskEnded,
                        Project_ID = task.Project_ID,
                        ProjectInfo = task.Project_ID.HasValue ? GetProjectInfo(task.Project_ID.Value) : null,
                    });                                        
                }

            }
            return listOfTasks;
        }
        
        public Tasks GetTask(int id)
        {
            Tasks objTask = null;
            using (var TaskDBcontext = new TaskDBEntities())
            {
                var task = TaskDBcontext.Tasks.FirstOrDefault(x => x.Task_ID == id);
                if (task != null)
                {
                    objTask = new Tasks()
                    {
                        Task_ID = task.Task_ID,
                        TaskName = task.Task1,
                        Start_Date = task.Start_Date,
                        End_Date = task.End_Date,
                        Priority = task.Priority,
                        Parent_ID = task.Parent_ID,
                        ParentTask = task.Parent_ID.HasValue ? GetParentInfo(task.Parent_ID.Value) : null,                        
                        IsTaskEnded = task.IsTaskEnded,
                        Project_ID = task.Project_ID,
                        ProjectInfo = task.Project_ID.HasValue ? GetProjectInfo(task.Project_ID.Value) : null,
                    };                  
                }
            }
            return objTask;
        }

        private ParentTasks GetParentInfo(int ID)
        {
            var ObjModel = new ParentTasks();
            using (var ObjDBcontext = new TaskDBEntities())
            {
                var pTask = ObjDBcontext.ParentTasks.FirstOrDefault(e => e.Parent_ID == ID);
                if (pTask != null)
                {
                    ObjModel = new ParentTasks()
                    {
                        Parent_ID = pTask.Parent_ID,
                        Parent_Task = pTask.Parent_Task,
                        Project_ID = pTask.Project_ID
                    };
                }
            }
            return ObjModel;
        }

        private ProjectInfo GetProjectInfo(int ProjectID)
        {
            var ObjModel = new ProjectInfo();           
            using (var ObjDBcontext = new TaskDBEntities())
            {

                var model = ObjDBcontext.Projects.FirstOrDefault(x => x.Project_ID == ProjectID);
                if (model != null)
                {
                    ObjModel = new ProjectInfo()
                    {
                        Project_ID = model.Project_ID,
                        Project1 = model.Project1,
                        Start_Date = model.Start_Date,
                        End_Date = model.End_Date,
                        Priority = model.Priority,
                        User_ID = model.User_ID
                    };
                }
            }
            return ObjModel;
        }

        public void AddTask(Tasks model)
        {
            using (var TaskDBcontext = new TaskDBEntities())
            {
                var task = new Task();
                task.Task_ID = model.Task_ID;
                task.Task1 = model.TaskName;
                task.Start_Date = model.Start_Date;
                task.End_Date = model.End_Date;
                task.Priority = model.Priority;
                task.Parent_ID = model.Parent_ID;                
                task.IsTaskEnded = model.IsTaskEnded;
                task.Project_ID = model.Project_ID;
                TaskDBcontext.Tasks.Add(task);
                TaskDBcontext.SaveChanges();
            }

        }
        
        public void UpdateTask(int id, Tasks model)
        {
            using (var TaskDBcontext = new TaskDBEntities())
            {
                var task = TaskDBcontext.Tasks.FirstOrDefault(x => x.Task_ID == id);
                if (task != null)
                {
                    task.Task_ID = model.Task_ID;
                    task.Task1 = model.TaskName;
                    task.Start_Date = model.Start_Date;
                    task.End_Date = model.End_Date;
                    task.Priority = model.Priority;
                    task.Parent_ID = model.Parent_ID;                    
                    task.IsTaskEnded = model.IsTaskEnded;
                    task.Project_ID = model.Project_ID;
                    TaskDBcontext.SaveChanges();
                }
            }

        }
        
        public void EndTask(int id)
        {
            using (var TaskDBcontext = new TaskDBEntities())
            {
                var task = TaskDBcontext.Tasks.FirstOrDefault(x => x.Task_ID == id);
                if (task != null)
                {
                    task.IsTaskEnded = 1;
                    TaskDBcontext.SaveChanges();
                }
            }

        }
    }
}