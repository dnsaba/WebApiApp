using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WebApiApp.Interfaces;
using WebApiApp.Models.Domain;
using WebApiApp.Services.Cryptography;

namespace WebApiApp.Services
{
    public class UserService
    {
        CryptographyService cryptsvc = new CryptographyService();
        private IAuthenticationService _authenticationService;
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        int RAND_LENGTH = 15;
        int HASH_ITERATION_COUNT = 1;

        public UserService(IAuthenticationService authService)
        {
            _authenticationService = authService;
        }

        public UserService()
        { }

        public int Insert(RegisterUser model)
        {
            // return to later if I have time to check for already registered emails
            //LoginUser loginModel = GetSalt(userModel.Email);
            //if (loginModel == null)
            
                int userId = 0;
                string salt;
                string passwordHash;

                string password = model.BasicPass;

                salt = cryptsvc.GenerateRandomString(RAND_LENGTH);
                passwordHash = cryptsvc.Hash(password, salt, HASH_ITERATION_COUNT);
                model.Salt = salt;
                model.EncryptedPass = passwordHash;
            //DB provider call to create user and get us a user id

            //be sure to store both salt and passwordHash
            //DO NOT STORE the original password value that the user passed us
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Salt", model.Salt);
                    cmd.Parameters.AddWithValue("@HashPass", model.EncryptedPass);

                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);

                    cmd.ExecuteNonQuery();

                    userId = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return userId;
        }

        public bool LogIn(LoginUser model)
        {
            bool isSuccessful = false;

            LoginUser userData = GetSalt(model.Email);

            if (userData != null && !String.IsNullOrEmpty(userData.Salt))
            {
                int multOf4 = userData.Salt.Length % 4;
                if (multOf4 > 0)
                {
                    userData.Salt += new string('=', 4 - multOf4);
                }

                string passwordHash = cryptsvc.Hash(model.Password, userData.Salt, HASH_ITERATION_COUNT);

                IUserAuthData response = new UserBase
                {
                    Id = userData.Id,
                    Email = userData.Email
                };

                Claim emailClaim = new Claim(userData.Email.ToString(), "LPGallery");
                _authenticationService.LogIn(response, new Claim[] { emailClaim });

                if (model.Email == userData.Email && passwordHash == userData.HashPassword)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        /// The Dataprovider call to get the Salt & other data for User with the given Email
        private LoginUser GetSalt(string email)
        {
            LoginUser model = null;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_SelectByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        model = new LoginUser();
                        int index = 0;
                        model.Id = reader.GetInt32(index++);
                        model.Email = reader.GetString(index++);
                        model.Salt = reader.GetString(index++);
                        model.HashPassword = reader.GetString(index++);
                        model.CreatedDate = reader.GetDateTime(index++);
                        model.ModifiedDate = reader.GetDateTime(index++);
                    }
                        
                }
                conn.Close();
            }
            return model;
        }
    }
}