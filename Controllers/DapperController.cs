using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NativeMySql.API.Interface;
using NativeMySql.API.Models;
using System.Collections.Generic;

namespace NativeMySql.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        IUserDetailsServiceDapper _userDetailServiceDapper;

        public DapperController(IUserDetailsServiceDapper userDetailsServiceDapper)
        {
            _userDetailServiceDapper = userDetailsServiceDapper;
        }

        [HttpGet]
        public string Get()
        {
            return "Test Success!";
        }

        [HttpGet("GetAllUsers")]
        public List<UserDetailsModel> GetAllUsers()
        {
            return _userDetailServiceDapper.GetAllUsers();
        }


        [HttpGet("GetUserById/{id}")]
        public UserDetailsModel GetUserById(int id)
        {
            return _userDetailServiceDapper.GetById(id);
        }

        [HttpPost("InsertNewUser")]
        public IActionResult InsertNewUser(UserDetailsModel user)
        {
            if (_userDetailServiceDapper.InsertNewUser(user))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UserDetailsModel user)
        {
            if (_userDetailServiceDapper.UpdateUser(user))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (_userDetailServiceDapper.DeleteUser(id))
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
