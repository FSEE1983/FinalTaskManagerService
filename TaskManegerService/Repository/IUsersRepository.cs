using System.Collections.Generic;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public interface IUsersRepository
    {
        void Add(Users TasksModel);
        void Delete(int id);
        IEnumerable<Users> Get();
        Users Get(int id);
        void Update(int id, Users TasksModel);
    }
}
