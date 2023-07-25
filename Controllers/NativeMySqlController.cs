using Microsoft.AspNetCore.Mvc;
using NativeMySql.API.Interface;
using NativeMySql.API.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NativeMySql.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NativeMySqlController : ControllerBase
    {

        IUserDetailsServiceMySql _userDetailServiceMySql;

        public NativeMySqlController(IUserDetailsServiceMySql userDetailsServiceMySql) 
        {
            _userDetailServiceMySql = userDetailsServiceMySql;
        }

        [HttpGet]
        public string Get()
        {
            return "Test Success!";
        }

        [HttpGet("GetAllUsers")]
        public List<UserDetailsModel> GetAllUsers()
        {
            return _userDetailServiceMySql.GetAllUsers();
        }


        [HttpGet("GetUserById/{id}")]
        public UserDetailsModel GetUserById(int id)
        {
            return _userDetailServiceMySql.GetUserById(id);
        }

        [HttpPost("InsertNewUser")]
        public IActionResult InsertNewUser(UserDetailsModel user)
        {
            if (_userDetailServiceMySql.InsertNewUser(user))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UserDetailsModel user)
        {
           if(_userDetailServiceMySql.UpdateUser(user))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            if(_userDetailServiceMySql.DeleteUser(id))
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
