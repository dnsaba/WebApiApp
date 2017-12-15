using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApiApp.Models.Domain;

namespace WebApiApp.Services
{
    public class PersonService 
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int Insert(PersonInsertRequest model)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Person_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleInitial", model.MiddleInitial);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    cmd.Parameters.AddWithValue("@DisplayName", model.DisplayName);

                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return id;
        }

        public List<Person> SelectAll()
        {
            List<Person> profileList = new List<Person>();

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Person_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Person model = Mapper(reader);
                        profileList.Add(model);
                    }
                }
                conn.Close();
            }
            return profileList;
        }

        public Person SelectById(int id)
        {
            Person model = new Person();
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Person_SelectByid", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        model = Mapper(reader);

                }
                conn.Close();
            }
            return model;
        }

        public void Update(Person model)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Person_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleInitial", model.MiddleInitial);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    cmd.Parameters.AddWithValue("@DisplayName", model.DisplayName);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Person_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private Person Mapper(SqlDataReader reader)
        {
            Person model = new Person();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.UserId = reader.GetInt32(index++);
            model.FirstName = reader.GetString(index++);
            if (!reader.IsDBNull(index))
            {
                model.MiddleInitial = reader.GetString(index++);
            }
            else
            {
                index++;
            }
            model.LastName = reader.GetString(index++);
            model.CreatedDate = reader.GetDateTime(index++);
            model.ModifiedDate = reader.GetDateTime(index++);
            model.ModifiedBy = reader.GetInt32(index++);
            model.Won = reader.GetInt32(index++);
            model.Lost = reader.GetInt32(index++);
            model.TotalGames = reader.GetInt32(index++);
            model.DisplayName = reader.GetString(index);

            return model;
        }
    }
}