using System.Collections.Generic;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public interface IProjectRepository
    {
        void Add(ProjectInfo model);
        void Delete(int id);
        IEnumerable<ProjectInfo> Get();
        ProjectInfo Get(int id);
        void Update(int id, ProjectInfo model);
    }
}
