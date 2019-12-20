using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManegerService.Models;
using TaskManegerService.Repository;

namespace TaskManegerService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class ProjectController : ApiController
    {
        IProjectRepository Repository;
        public ProjectController() : this (new ProjectRepository())
        {
        }
        public ProjectController(IProjectRepository _Repository)
        {
            Repository = _Repository;
        }

        // GET: api/Project
        public IEnumerable<ProjectInfo> Get()
        {
            return Repository.Get();
        }

        // GET: api/Project/5
        public ProjectInfo Get(int id)
        {
            return Repository.Get(id);
        }

        // POST: api/Project
        public void Post(ProjectInfo model)
        {
            Repository.Add(model);
        }

        // PUT: api/Project/5
        public void Put(int id, ProjectInfo model)
        {
            Repository.Update(id, model);
        }

        // DELETE: api/Project/5
        public void Delete(int id)
        {
            Repository.Delete(id);
        }
    }
}
