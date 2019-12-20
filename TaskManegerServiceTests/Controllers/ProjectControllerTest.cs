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
    public class ProjectControllerTest
    {
        ProjectController controller;
        Mock<IProjectRepository> repository = new Mock<IProjectRepository>();
        List<ProjectInfo> projectList = new List<ProjectInfo>();
        Users userInfo;

        [TestInitialize()]
        public void InitializeTest()
        {
            userInfo = new Users()
            {
                User_ID = 100,
                First_Name = String.Format("User First Name"),
                Last_Name = String.Format("User Last Name"),
                Employee_ID = "1001"
            };

            for (int i = 100; i < 500; i = i + 100)
            {
                projectList.Add(new ProjectInfo()
                {
                    Project_ID = i,
                    Project1 = string.Format("Project Name - {0}", i),
                    Start_Date = new DateTime(),
                    End_Date = new DateTime().AddDays(1),
                    Priority = i ,
                    UserInfo = userInfo,
                    User_ID = 1001
                });
            }

            repository.Setup(x => x.Get()).Returns(() => projectList);
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => projectList.Where(x => x.Project_ID == id).FirstOrDefault());
            repository.Setup(x => x.Add(It.IsAny<ProjectInfo>())).Callback((ProjectInfo project) => projectList.Add(project));

            repository.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<ProjectInfo>())).Callback((int id, ProjectInfo project) =>
            {
                foreach (ProjectInfo item in projectList)
                {
                    if (item.Project_ID == id)
                    {
                        item.Project1 = project.Project1;
                        item.Start_Date = project.Start_Date;
                        item.End_Date = project.End_Date;
                    }
                }
            });

            repository.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) => projectList.RemoveAt(projectList.IndexOf(projectList.Where(x => x.Project_ID == id).FirstOrDefault())));

            controller = new ProjectController(repository.Object);
        }


        [TestMethod()]
        public void ProjectGetTest()
        {
            var ProjectList = controller.Get();
            Assert.IsNotNull(ProjectList, null);

        }

        [TestMethod()]
        public void ProjectGetTestById()
        {
            var projectinfo = controller.Get(200);
            Assert.IsNotNull(projectinfo);
        }

        [TestMethod()]
        public void ProjectAddTest()
        {
            controller.Post(new ProjectInfo()
            {
                Project_ID = 600,
                Project1 = string.Format("Project Name test"),
                Start_Date = new DateTime(),
                End_Date = new DateTime().AddDays(1),
                Priority = 10,
                UserInfo = userInfo,
                User_ID = 1001
            });
            Assert.IsNotNull(controller.Get(600));
        }

        [TestMethod()]
        public void ProjectUpdateTest()
        {
            controller.Put(100, new ProjectInfo()
            {
                Project_ID = 100,
                Project1 = string.Format("Project Name test"),
                Start_Date = new DateTime(),
                End_Date = new DateTime().AddDays(1),
                Priority = 10,
                UserInfo = userInfo,
                User_ID = 1001
            });
            Assert.AreEqual(controller.Get(100).Project1, "Project Name test");
        }

        [TestMethod()]
        public void ProjectDeleteTest()
        {
            controller.Delete(200);
            Assert.IsNull(controller.Get(200));
        }
    }
}


