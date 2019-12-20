using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManegerService.Models;
using TaskManegerService.Repository;

namespace TaskManegerService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class UsersController : ApiController
    {

        IUsersRepository userRepository;

        public UsersController():this(new UsersRepository())
        {

        }
        public UsersController(IUsersRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        // GET: api/Users
        public IEnumerable<Users> Get()
        {
            return userRepository.Get();
        }

        // GET: api/Users/5
        public Users Get(int id)
        {
            return userRepository.Get(id);
        }

        // POST: api/Users
        public void Post(Users model)
        {
            userRepository.Add(model);
        }

        // PUT: api/Users/5
        public void Put(int id, Users model)
        {
            userRepository.Update(id, model);
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
            userRepository.Delete(id);
        }
    }
}
