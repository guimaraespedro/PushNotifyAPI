using Microsoft.AspNetCore.Mvc;
using PushNotifyAPI.Entities;
using PushNotifyAPI.Repositories;
using PushNotifyAPI.Services;

namespace PushNotifyAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPushService _pushService;
        public UserController(IUserRepository userRepository, IPushService pushService)
        {
            _userRepository = userRepository;
            _pushService = pushService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _userRepository.CreateAsync(user);

            return Ok();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User? user = await _userRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNull(user);
            return Ok(user);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ICollection<User> users = await _userRepository.GetAsync();
            return Ok(users);
        }


    }
}