using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Providers
{
    public sealed class SqlDataProvider
    {
        private const string LOG_CAT = "DAO";
        readonly string connectionString;

        public SqlDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public void ExecuteCmd(
        //    string storedProc,
        //    Action<SqlParameterCollection> inputParamMapper,
        //)
    }
}