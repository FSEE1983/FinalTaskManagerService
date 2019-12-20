using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManegerService.Controllers;
using TaskManegerService.Models;
using TaskManegerService.Repository;

namespace TaskManegerServiceTests.Controllers
{
    [TestClass()]
    public class UsersControllerTest
    {
        UsersController controller;
        Mock<IUsersRepository> repository = new Mock<IUsersRepository>();
        List<Users> UsersList = new List<Users>();

        [TestInitialize()]
        public void InitializeTest()
        {
            for (int i = 100; i < 500; i = i + 100)
            {
                UsersList.Add(new Users()
                {
                    User_ID = i,
                    First_Name = String.Format("User First Name-{0}", i),
                    Last_Name = String.Format("User Last Name-{0}", i),
                    Employee_ID = String.Format("Employe ID-{0}", i)
                });
            }

            repository.Setup(x => x.Get()).Returns(() => UsersList);
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => UsersList.Where(x => x.User_ID == id).FirstOrDefault());
            repository.Setup(x => x.Add(It.IsAny<Users>())).Callback((Users user) => UsersList.Add(user));

            repository.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Users>())).Callback((int id, Users user) =>
            {
                foreach (Users item in UsersList)
                {
                    if (item.User_ID == id)
                    {
                        item.First_Name = user.First_Name;
                        item.Last_Name = user.Last_Name;
                        item.Employee_ID = user.Employee_ID;
                    }
                }
            });

            repository.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) => UsersList.RemoveAt(UsersList.IndexOf(UsersList.Where(x => x.User_ID == id).FirstOrDefault())));

            controller = new UsersController(repository.Object);
        }


        [TestMethod()]
        public void UsersGetTest()
        {
            var UsersList = controller.Get();
            Assert.IsNotNull(UsersList, null);

        }

        [TestMethod()]
        public void UsersGetTestById()
        {
            var userinfo = controller.Get(200);
            Assert.IsNotNull(userinfo);
        }

        [TestMethod()]
        public void UsersAddTest()
        {
            controller.Post(new Users
            {
                User_ID = 600,
                First_Name = String.Format("User First Name Test"),
                Last_Name = String.Format("User Last Name Test"),
                Employee_ID = String.Format("Employe ID Test")
            });
            Assert.IsNotNull(controller.Get(600));
        }

        [TestMethod()]
        public void UsersUpdateTest()
        {
            controller.Put(100, new Users
            {
                User_ID = 100,
                First_Name = String.Format("User First Name Test"),
                Last_Name = String.Format("User Last Name Test"),
                Employee_ID = String.Format("Employe ID Test")
            });
            Assert.AreEqual(controller.Get(100).First_Name, "User First Name Test");
        }

        [TestMethod()]
        public void UsersDeleteTest()
        {
            controller.Delete(200);
            Assert.IsNull(controller.Get(200));
        }
    }
}


