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
    public class PeopleService 
    {
        //public int Insert(Profile model)
        //{
        //    int id = 0;
        //    this.DataProvider.ExecuteNonQuery(
        //        "Coupons_Insert",
        //        inputParamMapper: delegate (SqlParameterCollection paramCol)
        //        {
        //            SqlParameter param = new SqlParameter();
        //            param.ParameterName = "@Id";
        //            param.SqlDbType = SqlDbType.Int;
        //            param.Direction = ParameterDirection.Output;
        //            paramCol.Add(param);

        //            paramCol.AddWithValue("@CoupnCode", model.CouponCode);
        //            paramCol.AddWithValue("@MultipleUse", model.MultipleUse);
        //            paramCol.AddWithValue("@SpecifiedCustomer", model.SpecifiedCustomer);
        //            paramCol.AddWithValue("@Description", model.Description);
        //            paramCol.AddWithValue("@MaxUseCount", model.MaxUseCount);
        //            paramCol.AddWithValue("@DiscountType", model.DiscountType);
        //            paramCol.AddWithValue("@DiscountValue", model.DiscountValue);
        //            paramCol.AddWithValue("@StartDate", model.StartDate);
        //            paramCol.AddWithValue("@EndDate", model.EndDate);
        //            paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
        //        },
        //        returnParameters: delegate (SqlParameterCollection paramCol)
        //        {
        //            id = (int)paramCol["@Id"].Value;
        //        }
        //    );
        //    return id;
        //}

        public List<People> SelectAll()
        {
            List<People> profileList = new List<People>();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Profiles_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        People model = Mapper(reader);
                        profileList.Add(model);
                    }
                }
                conn.Close();
            }
            return profileList;
        }

        private People Mapper(SqlDataReader reader)
        {
            People model = new People();
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