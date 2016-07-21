using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmParameterManager.DataManager
{
    internal static class SQLHelper
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.AppSettings["ConnectionString"]; }
        }

        public static ArrayList ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var rowList = new ArrayList();

            using (var conn = new SqlConnection(ConnectionString))
            {
                var sqlCmd = new SqlCommand
                    {
                        Connection = conn,
                        CommandType = commandType,
                        CommandText = commandText
                    };

                sqlCmd.Parameters.AddRange(commandParameters);

                conn.Open();

                var dataReader = sqlCmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var values = new object[dataReader.FieldCount];
                        dataReader.GetValues(values);
                        rowList.Add(values);
                    }
                }

                
            }

            return rowList;
        }

        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sqlCmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = commandType,
                    CommandText = commandText
                };

                sqlCmd.Parameters.AddRange(commandParameters);

                conn.Open();

                var retVal = sqlCmd.ExecuteNonQuery();

                if (conn.State != ConnectionState.Closed) conn.Close();;
                
                return retVal;
            }
        }
    }
}
