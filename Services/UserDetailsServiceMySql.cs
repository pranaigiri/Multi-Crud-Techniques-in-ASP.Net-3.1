
using NativeMySql.API.Helper;
using NativeMySql.API.Interface;
using NativeMySql.API.Models;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace NativeMySql.API.Services
{
    internal class UserDetailsServiceMySql : IUserDetailsServiceMySql
    {

        private readonly MySqlHelper _mySqlHelper;

        public UserDetailsServiceMySql(MySqlHelper mySqlHelper)
        {
            _mySqlHelper = mySqlHelper;
        }

        public List<UserDetailsModel> GetAllUsers()
        {
            List<UserDetailsModel> users = _mySqlHelper.GetAll("sp_GetAllUsers", reader =>
            {
                return new UserDetailsModel
                {
                        Id = Convert.ToInt32(reader["id"]),
                        Username = reader["username"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Phone = reader["phone"].ToString(),
                };
            });

            return users;
        }

        public UserDetailsModel GetUserById(int id)
        {
            UserDetailsModel user = new UserDetailsModel();

            user = _mySqlHelper.GetById("sp_GetUserDetailsById", "p_id", id, reader =>
            {
                return new UserDetailsModel
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Username = reader["username"].ToString(),
                    Email = reader["email"].ToString(),
                    Password = reader["password"].ToString(),
                    Phone = reader["phone"].ToString(),
                };
            });

            return user;
        }

        public bool InsertNewUser(UserDetailsModel userDetails)
        {
            int isInserted = _mySqlHelper.InsertOrUpdate("sp_CreateUserDetails", userDetails, command =>
            {
                command.Parameters.AddWithValue("@p_username", userDetails.Username);
                command.Parameters.AddWithValue("@p_email", userDetails.Email);
                command.Parameters.AddWithValue("@p_password", userDetails.Password);
                command.Parameters.AddWithValue("@p_phone", userDetails.Phone);
            });

            if(isInserted <= 0)
            {
                return false;
            }

            return true;

        }

        public bool UpdateUser(UserDetailsModel userDetails)
        {
            int isInserted = _mySqlHelper.InsertOrUpdate("sp_UpdateUserDetails", userDetails, command =>
            {
                command.Parameters.AddWithValue("@p_id", userDetails.Id);
                command.Parameters.AddWithValue("@p_username", userDetails.Username);
                command.Parameters.AddWithValue("@p_email", userDetails.Email);
                command.Parameters.AddWithValue("@p_password", userDetails.Password);
                command.Parameters.AddWithValue("@p_phone", userDetails.Phone);
            });

            if (isInserted <= 0)
            {
                return false;
            }

            return true;
        }
        public bool DeleteUser(int id)
        {
            int isDeleted = _mySqlHelper.Delete("sp_DeleteUserDetailsById", "p_id", id);

            if (isDeleted <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
