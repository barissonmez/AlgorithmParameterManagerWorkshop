using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmParameterManager.Entity;

namespace AlgorithmParameterManager.DataManager
{
    public class DataManager
    {
        public AlgorithmParameter GetAlgorithmParameterByID(int ID)
        {
            var parameter = new AlgorithmParameter();

            var rowList = SQLHelper.ExecuteReader(CommandType.Text, "SELECT * FROM AlgorithmParameters WHERE ID=@ID", new SqlParameter("ID", ID));

            if (rowList.Count > 0)
            {

                foreach (object[] row in rowList)
                {
                    parameter.ID = Convert.ToInt32(row[0]);
                    parameter.Name = row[1].ToString();
                    parameter.Description = row[2].ToString();
                    parameter.Type = Convert.ToInt32(row[3]);
                    parameter.Value = row[4].ToString();
                    parameter.AlgorithmType = Convert.ToInt32(row[5]);
                }
            }

            return parameter;
        }

        public List<AlgorithmParameter> GetAllAlgorithmParameters()
        {
            var parameters = new List<AlgorithmParameter>();

            var rowList = SQLHelper.ExecuteReader(CommandType.Text, "SELECT * FROM AlgorithmParameters ORDER BY ID ASC");

            if (rowList.Count > 0)
            {
                parameters.AddRange(from object[] row in rowList
                                    select new AlgorithmParameter
                                        {
                                            ID = Convert.ToInt32(row[0]), 
                                            Name = row[1].ToString(), 
                                            Description = row[2].ToString(), 
                                            Type = Convert.ToInt32(row[3]), 
                                            Value = row[4].ToString(), 
                                            AlgorithmType = Convert.ToInt32(row[5])
                                        });
            }

            return parameters;
        }

        public bool CreateAlgorithmParameter(AlgorithmParameter entity)
        {
            const string sqlQuery = "Insert into AlgorithmParameters Values(@Name, @Description, @Type, @Value, @AlgorithmType )";

            var sqlParameters = new[]
                {
                    new SqlParameter("Name", entity.Name),
                    new SqlParameter("Description", entity.Description),
                    new SqlParameter("Type", entity.Type),
                    new SqlParameter("Value", entity.Value),
                    new SqlParameter("AlgorithmType", entity.AlgorithmType)
                };

            var result = SQLHelper.ExecuteNonQuery(CommandType.Text, sqlQuery, sqlParameters);

            return result > 0;

        }

        public bool UpdateAlgorithmParameter(AlgorithmParameter entity, int ID)
        {
            const string sqlQuery = "UPDATE AlgorithmParameters SET Name=@Name, Description=@Description, Type=@Type, Value=@Value, AlgorithmType=@AlgorithmType WHERE ID=@ID";

            var sqlParameters = new[]
                {
                    new SqlParameter("ID", ID),
                    new SqlParameter("Name", entity.Name),
                    new SqlParameter("Description", entity.Description),
                    new SqlParameter("Type", entity.Type),
                    new SqlParameter("Value", entity.Value),
                    new SqlParameter("AlgorithmType", entity.AlgorithmType)
                };

            var result = SQLHelper.ExecuteNonQuery(CommandType.Text, sqlQuery, sqlParameters);

            return result > 0;
        }

        public bool DeleteAlgorithmParameter(int ID)
        {
            const string sqlQuery = "DELETE FROM AlgorithmParameters WHERE ID=@ID";

            var sqlParameters = new[]
                {
                    new SqlParameter("ID", ID)
                };

            var result = SQLHelper.ExecuteNonQuery(CommandType.Text, sqlQuery, sqlParameters);

            return result > 0;
        }
    }
}
