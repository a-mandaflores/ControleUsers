using ControleUsers.DTOs;
using ControleUsers.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleUsers.Controllers.User
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserControllers : ControllerBase
    {
        private readonly IUserService _userService;

        public UserControllers(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserPost userPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createUser = await _userService.Create(userPost);
            return Ok(userPost);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRead>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest("Exception");
            }
        }

        public async Task<ActionResult<UserRead>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);
                return user != null
                    ? Ok(user)
                    : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Exception");

            }
        }
    }
}
