using Database;
using Models.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Services.Helper;

namespace Services.Helper
{
    public class DataBaseOperations
    {

        public DataSet ExecuteStoredProcedure(SqlConnection connection, SqlCommand sqlcom)
        {
            DataSet ds;
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlcom))
            {
                try
                {
                    ds = new DataSet();
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            return ds;
        }

        public SqlParameter SetParameter(string parameterName, object parameterValue, SqlDbType dbType, ParameterDirection Direction = ParameterDirection.Input, string typeName = "")
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = parameterName;

            if (parameterValue == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = parameterValue;
            }
            parameter.SqlDbType = dbType;
            parameter.Direction = Direction;
            if (!string.IsNullOrEmpty(typeName))
            {
                parameter.TypeName = typeName;
            }
            return parameter;
        }




    }
}
