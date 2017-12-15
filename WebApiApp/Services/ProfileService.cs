using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiApp.Models.Domain;

namespace WebApiApp.Services
{
    public class ProfileService 
    {
        public int Insert(Profile model)
        {
            int id = 0;
            this.DataProvider.ExecuteNonQuery(
                "Coupons_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Id";
                    param.SqlDbType = SqlDbType.Int;
                    param.Direction = ParameterDirection.Output;
                    paramCol.Add(param);

                    paramCol.AddWithValue("@CoupnCode", model.CouponCode);
                    paramCol.AddWithValue("@MultipleUse", model.MultipleUse);
                    paramCol.AddWithValue("@SpecifiedCustomer", model.SpecifiedCustomer);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@MaxUseCount", model.MaxUseCount);
                    paramCol.AddWithValue("@DiscountType", model.DiscountType);
                    paramCol.AddWithValue("@DiscountValue", model.DiscountValue);
                    paramCol.AddWithValue("@StartDate", model.StartDate);
                    paramCol.AddWithValue("@EndDate", model.EndDate);
                    paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
                },
                returnParameters: delegate (SqlParameterCollection paramCol)
                {
                    id = (int)paramCol["@Id"].Value;
                }
            );
            return id;
        }
    }
}