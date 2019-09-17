using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Repositories
{
    public class WorkTypeRepository
    {
        private string _connectionString;

        public WorkTypeRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public IList<WorkType> GetWorkTypes()
        {
            var workTypes = new List<WorkType>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                    select Id, WorkTypeName, Rate
                    from WorkType
                    order by WorkTypeName
                ";

                using (var command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal rate = reader.GetDecimal(2);
                        var workType = new WorkType(id, name, rate);
                        workTypes.Add(workType);
                    }
                }
            }

            return workTypes;
        }

        public WorkType GetWorkType(int id)
        {
            WorkType workType = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                    select WorkTypeName, Rate
                    from WorkType
                    where Id = @Id
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(0);
                            decimal rate = reader.GetDecimal(1);
                            workType = new WorkType(id, name, rate);
                        }
                    }
                }
            }

            return workType;
        }

        public void Insert(WorkType workType)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                  insert WorkType(WorkTypeName, Rate)
                  values (@WorkTypeName, @Rate)
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@WorkTypeName", workType.Name);
                    command.Parameters.AddWithValue("@Rate", workType.Rate);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(WorkType workType)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                  update WorkType set 
                    WorkTypeName = @WorkTypeName, 
                    Rate = @Rate
                  where Id = @Id
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@WorkTypeName", workType.Name);
                    command.Parameters.AddWithValue("@Rate", workType.Rate);
                    command.Parameters.AddWithValue("@Id", workType.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}