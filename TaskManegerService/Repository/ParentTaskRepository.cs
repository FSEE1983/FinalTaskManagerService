using System.Collections.Generic;
using System.Linq;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public class ParentTaskRepository : IParentTaskRepository
    {
        public IEnumerable<ParentTasks> GetTask()
        {
            var listOfTasks = new List<ParentTasks>();
            foreach (var task in new TaskDBEntities().ParentTasks)
            {
                listOfTasks.Add(new ParentTasks()
                {
                    Parent_ID = task.Parent_ID,
                    Parent_Task = task.Parent_Task,
                    Project_ID= task.Project_ID
                });
            };
            return listOfTasks;
        }


        public ParentTasks GetTask(int id)
        {
            ParentTasks Tasks = null;
            var task = new TaskDBEntities().ParentTasks.FirstOrDefault(x => x.Parent_ID == id);
            if (task != null)
            {
                Tasks = new ParentTasks()
                {
                    Parent_ID = task.Parent_ID,
                    Parent_Task = task.Parent_Task,
                    Project_ID= task.Project_ID
                };
            }
            return Tasks;
        }


        public void AddTask(ParentTasks model)
        {
            using (var TaskDBcontext = new TaskDBEntities())
            {
                var task = new ParentTask();
                task.Parent_ID = model.Parent_ID;
                task.Parent_Task = model.Parent_Task;
                task.Project_ID = model.Project_ID;
                TaskDBcontext.ParentTasks.Add(task);
                TaskDBcontext.SaveChanges();
            }                
        }


        public void UpdateTask(int id, ParentTasks model)
        {
            using (var TaskDBcontext = new TaskDBEntities())
            {
                var task = TaskDBcontext.ParentTasks.FirstOrDefault(x => x.Parent_ID == id);
                if (task != null)
                {
                    task.Parent_ID = model.Parent_ID;
                    task.Parent_Task = model.Parent_Task;
                    task.Project_ID = model.Project_ID;
                    TaskDBcontext.SaveChanges();
                }
            }
        }


        public void DeleteTask(int id)
        {
            using (var TaskDBcontext = new TaskDBEntities())
            {
                var task = TaskDBcontext.ParentTasks.FirstOrDefault(x => x.Parent_ID == id);
                if (task != null)
                {
                    TaskDBcontext.ParentTasks.Remove(task);
                    TaskDBcontext.SaveChanges();
                }
            }                
        }
    }
}