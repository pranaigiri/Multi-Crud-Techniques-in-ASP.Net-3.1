using NativeMySql.API.Models;
using System.Collections.Generic;

namespace NativeMySql.API.Interface
{
    public interface IUserDetailsServiceMySql
    {
        /*SERVICES THAT USES NATIVE MYSQL FOR CRUD*/
        public List<UserDetailsModel> GetAllUsers();
        public UserDetailsModel GetUserById(int id);
        public bool InsertNewUser(UserDetailsModel userDetails);
        public bool UpdateUser(UserDetailsModel userDetails);
        public bool DeleteUser(int id);
    }
}
