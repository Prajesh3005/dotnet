
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get() => Ok(users);

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound("User Not Found");
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.Id = users.Count + 1;
            users.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound("User Not Found");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound("User Not Found");

            users.Remove(user);
            return Ok("User Deleted");
        }
    }
}
