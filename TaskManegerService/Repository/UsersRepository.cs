using System.Collections.Generic;
using System.Linq;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public class UsersRepository : IUsersRepository
    {
        public IEnumerable<Users> Get()
        {
            var listOfusers = new List<Users>();
            var Users = new TaskDBEntities().Users; 
            foreach (var user in Users)
            {
                listOfusers.Add(new Users()
                {
                    User_ID = user.User_ID,
                    First_Name = user.First_Name,
                    Last_Name = user.Last_Name,
                    Employee_ID = user.Employee_ID
                });
            };

            return listOfusers;
        }


        public Users Get(int id)
        {
            Users objUsers = null;
            var user = new TaskDBEntities().Users.FirstOrDefault(x => x.User_ID == id);
            if (user != null)
            {
                objUsers = new Users()
                {
                    User_ID = user.User_ID,
                    First_Name = user.First_Name,
                    Last_Name = user.Last_Name,
                    Employee_ID = user.Employee_ID
                };
            }
            return objUsers;
        }


        public void Add(Users ObjModel)
        {
            var ObjDBcontext = new TaskDBEntities();
            var Objuser = new User();
            Objuser.First_Name = ObjModel.First_Name;
            Objuser.Last_Name = ObjModel.Last_Name;
            Objuser.Employee_ID = ObjModel.Employee_ID;
            ObjDBcontext.Users.Add(Objuser);
            ObjDBcontext.SaveChanges();
        }


        public void Update(int id, Users ObjModel)
        {
            var ObjDBcontext = new TaskDBEntities();
            var user = ObjDBcontext.Users.FirstOrDefault(x => x.User_ID == id);
            if (user != null)
            {
                user.First_Name = ObjModel.First_Name;
                user.Last_Name = ObjModel.Last_Name;
                user.Employee_ID = ObjModel.Employee_ID;
                ObjDBcontext.SaveChanges();
            }
        }


        public void Delete(int id)
        {
            var ObjDBcontext = new TaskDBEntities();
            var user = ObjDBcontext.Users.FirstOrDefault(x => x.User_ID == id);
            if (user != null)
            {
                ObjDBcontext.Users.Remove(user);
                ObjDBcontext.SaveChanges();
            }
        }
    }
}