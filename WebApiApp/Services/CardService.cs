using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApiApp.Models.Domain;

namespace WebApiApp.Services
{
    public class CardService
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int Insert(CardWithFile model)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Cards_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileId", model.FileId);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@AttackLevel", model.AttackLevel);
                    cmd.Parameters.AddWithValue("@DefenseLevel", model.DefenseLevel);
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);

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

        public List<CardWithFile> SelectAll()
        {
            List<CardWithFile> cardList = new List<CardWithFile>();

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Cards_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        CardWithFile model = Mapper(reader);
                        cardList.Add(model);
                    }
                }
                conn.Close();
            }
            return cardList;
        }

        public CardWithFile SelectById(int id)
        {
            CardWithFile model = new CardWithFile();
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Cards_SelectById", conn))
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

        public void Update(CardWithFile model)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Cards_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@AttackLevel", model.AttackLevel);
                    cmd.Parameters.AddWithValue("@DefenseLevel", model.DefenseLevel);

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
                using (SqlCommand cmd = new SqlCommand("Cards_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private CardWithFile Mapper(SqlDataReader reader)
        {
            CardWithFile model = new CardWithFile();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Name = reader.GetString(index++);
            model.Description = reader.GetString(index++);
            model.UserId = reader.GetInt32(index++);
            model.AttackLevel = reader.GetInt32(index++);
            model.DefenseLevel = reader.GetInt32(index++);
            model.FileId = reader.GetInt32(index++);
            model.UserFileName = reader.GetString(index++);
            model.SystemFileName = reader.GetString(index++);
            model.FileLocation = reader.GetString(index++);
            model.FileUserId = reader.GetInt32(index++);

            return model;
        }
    }
}
