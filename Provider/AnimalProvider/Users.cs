﻿
using System.Data.SqlClient;

namespace AnimalProvider
{
    public class Users : Core.Disposable
    {
        public List<Entities.User> getAllUsers()
        {
            using var DAL = new DataAccess.DataAccessLayer();
            return DAL.ExecuteReader<Entities.User>("Animal.spGetAllUsers");
        }

        public Entities.User getUser(int Id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter>
                {
                    new SqlParameter{ ParameterName = "@Id", Value = Id }
            };

            return DAL.ExecuteReader<Entities.User>("Animal.spGetUser").FirstOrDefault();
        }

        public Entities.User getUserByInfo(string name)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter>
                {
                    new SqlParameter{ ParameterName = "@Name", Value = name }
            };

            return DAL.ExecuteReader<Entities.User>("Animal.spGetUserByInfo").FirstOrDefault();
        }

        public bool addUser(ViewModel.User user)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Name", Value =  user.Name },
                new SqlParameter{ ParameterName = "@Email", Value =  user.Email },
                new SqlParameter{ ParameterName = "@DateOfBirth", Value =  user.DateOfBirth },
                new SqlParameter{ ParameterName = "@Password", Value =  user.Password },
                new SqlParameter{ ParameterName = "@RoleID", Value =  user.roleID }
            };

            return DAL.ExecuteNonQuery("Animal.spAddUser");
        }
        
        public bool deleteUser(int Id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  Id }
            };

            return DAL.ExecuteNonQuery("Animal.spDeleteUser");
        }

        public bool updateUser(ViewModel.User user)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  user.Id },
                new SqlParameter{ ParameterName = "@Name", Value =  user.Name },
                new SqlParameter{ ParameterName = "@Email", Value =  user.Email },
                new SqlParameter{ ParameterName = "@DateOfBirth", Value =  user.DateOfBirth },
                new SqlParameter{ ParameterName = "@Password", Value =  user.Password },
                new SqlParameter{ ParameterName = "@RoleID", Value =  user.roleID },

            };

            return DAL.ExecuteNonQuery("Animal.spUpdateUser");
        }

        public bool registerUser(ViewModel.Register model)
        {
			using var DAL = new DataAccess.DataAccessLayer();
			DAL.Parameters = new List<SqlParameter> {
				new SqlParameter{ ParameterName = "@Name", Value =  model.Name },
				new SqlParameter{ ParameterName = "@Email", Value =  model.Email },
				new SqlParameter{ ParameterName = "@DateOfBirth", Value =  model.dateOfBirth },
				new SqlParameter{ ParameterName = "@Password", Value =  model.Password },
				new SqlParameter{ ParameterName = "@RoleID", Value =  3 }
			};

			return DAL.ExecuteNonQuery("Animal.spAddUser");
		}
    }
}
