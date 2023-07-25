
using NativeMySql.API.Helper;
using NativeMySql.API.Interface;
using NativeMySql.API.Models;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace NativeMySql.API.Services
{
    internal class UserDetailsServiceDapper : IUserDetailsServiceDapper
    {

        private readonly DapperHelper _dapperHelper;

        public UserDetailsServiceDapper(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public List<UserDetailsModel> GetAllUsers()
        {
            List<UserDetailsModel> users = _dapperHelper.GetAll<UserDetailsModel>("sp_GetAllUsers");
            return users;
        }

        public UserDetailsModel GetById(int id)
        {
            UserDetailsModel user = _dapperHelper.GetById<UserDetailsModel>("sp_GetUserDetailsById", "p_id", id);

            return user;
        }

        public bool InsertNewUser(UserDetailsModel userDetails)
        {
            int isInserted = _dapperHelper.InsertOrUpdate("sp_CreateUserDetails", userDetails, parameters =>
            {
                parameters.Add("@p_username", userDetails.Username);
                parameters.Add("@p_email", userDetails.Email);
                parameters.Add("@p_password", userDetails.Password);
                parameters.Add("@p_phone", userDetails.Phone);
            });


            if (isInserted <= 0)
            {
                return false;
            }

            return true;

        }

        public bool UpdateUser(UserDetailsModel userDetails)
        {
            int isInserted = _dapperHelper.InsertOrUpdate("sp_UpdateUserDetails", userDetails, parameters =>
            {
                parameters.Add("@p_id", userDetails.Id);
                parameters.Add("@p_username", userDetails.Username);
                parameters.Add("@p_email", userDetails.Email);
                parameters.Add("@p_password", userDetails.Password);
                parameters.Add("@p_phone", userDetails.Phone);
            });

            if (isInserted <= 0)
            {
                return false;
            }

            return true;
        }
        public bool DeleteUser(int id)
        {
            int isDeleted = _dapperHelper.Delete("sp_DeleteUserDetailsById", "p_id", id);

            if (isDeleted <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
