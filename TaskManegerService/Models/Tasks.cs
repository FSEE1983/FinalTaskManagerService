using System;

namespace TaskManegerService.Models
{
    public class Tasks
    {
        public int Task_ID { get; set; }        
        public Nullable<int> Parent_ID { get; set; }
        public string TaskName { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> IsTaskEnded { get; set; }
        public  ParentTasks ParentTask { get; set; }
        public int? Project_ID { get; set; }
        public ProjectInfo ProjectInfo { get; set; }
    }
}