using System.Collections.Generic;
using System.Linq;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        public void Add(ProjectInfo model)
        {
            using (var ObjDBcontext = new TaskDBEntities())
            {
                var ObjModel = new Project();
                ObjModel.Project1 = model.Project1;
                ObjModel.Start_Date = model.Start_Date;
                ObjModel.End_Date = model.End_Date;
                ObjModel.End_Date = model.End_Date;
                ObjModel.Priority = model.Priority;
                ObjModel.User_ID = model.User_ID;
                ObjDBcontext.Projects.Add(ObjModel);
                ObjDBcontext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var ObjDBcontext = new TaskDBEntities())
            {
                var model = ObjDBcontext.Projects.FirstOrDefault(x => x.Project_ID == id);
                if (model != null)
                {
                    ObjDBcontext.Projects.Remove(model);
                    ObjDBcontext.SaveChanges();
                }
            }
        }

        public IEnumerable<ProjectInfo> Get()
        {
            var ListInfo = new List<ProjectInfo>();
            using (var ObjDBcontext = new TaskDBEntities())
            {
                foreach (var model in ObjDBcontext.Projects)
                {
                    ListInfo.Add(new ProjectInfo()
                    {
                        Project_ID = model.Project_ID,
                        Project1 = model.Project1,
                        Start_Date = model.Start_Date,
                        End_Date = model.End_Date,
                        Priority = model.Priority,
                        User_ID = model.User_ID,
                        UserInfo = model.User_ID.HasValue ? getUserInfo(model.User_ID) : null 
                    });
                }

            }

            return ListInfo;
        }
        private Users getUserInfo(int? UserID)
        {
            var objuser = new Users();
            using (var ObjDBcontext = new TaskDBEntities())
            {
                var user = ObjDBcontext.Users.FirstOrDefault(x => x.User_ID == UserID);
                if (user != null)
                {
                    objuser = new Users()
                    {
                        User_ID = user.User_ID,
                        First_Name = user.First_Name,
                        Last_Name = user.Last_Name,
                        Employee_ID = user.Employee_ID
                    };
                }
            }
            return objuser;
        }

        public ProjectInfo Get(int id)
        {
            ProjectInfo ObjModel = null;
            using (var ObjDBcontext = new TaskDBEntities())
            {
                var model = ObjDBcontext.Projects.FirstOrDefault(x => x.User_ID == id);
                if (model != null)
                {
                    ObjModel = new ProjectInfo()
                    {
                        Project_ID = model.Project_ID,
                        Project1 = model.Project1,
                        Start_Date = model.Start_Date,
                        End_Date = model.End_Date,
                        Priority = model.Priority,
                        User_ID = model.User_ID,
                        UserInfo = model.User_ID.HasValue ? getUserInfo(model.User_ID) : null 
                    };
                }
            }
            return ObjModel;
        }


        public void Update(int id, ProjectInfo model)
        {
            using (var ObjDBcontext = new TaskDBEntities())
            {
                var ObjModel = ObjDBcontext.Projects.FirstOrDefault(x => x.Project_ID == id);
                if (ObjModel != null)
                {
                    ObjModel.Project1 = model.Project1;
                    ObjModel.Start_Date = model.Start_Date;
                    ObjModel.End_Date = model.End_Date;
                    ObjModel.End_Date = model.End_Date;
                    ObjModel.Priority = model.Priority;
                    ObjModel.User_ID = model.User_ID;
                    ObjDBcontext.SaveChanges();
                }
            }
        }
    }
}

