using System;

namespace TaskManegerService.Models
{
    public class ProjectInfo
    {
        public int Project_ID { get; set; }
        public string Project1 { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int? Priority { get; set; }
        public int? User_ID { get; set; }
        public Users UserInfo { get; set; } 
    }
}