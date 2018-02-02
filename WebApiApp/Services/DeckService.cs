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
    public class DeckService
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //public bool Insert(Deck model)
        //{
        //    int id = 0;
        //    using (SqlConnection conn = new SqlConnection(sqlConnectionString))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand("Decks_Insert", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@DeckName", model.DeckName);

        //            SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
        //            parm.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(parm);

        //            cmd.ExecuteNonQuery();

        //            id = (int)cmd.Parameters["@Id"].Value;
        //        }

        //        using (SqlCommand cmd = new SqlCommand()
        //        conn.Close();
        //    }
            
        //}
    }
}