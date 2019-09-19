using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Repositories
{
    public class WorkDoneRepository
    {
        private string _connectionString;

        public WorkDoneRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public IList<WorkDone> GetWorkDone()
        {
            var workDoneList = new List<WorkDone>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                    select wd.Id, wd.ClientId, wd.WorkTypeId,
	                    wd.StartedOn, wd.EndedOn, c.ClientName, c.IsActivated,
	                    wt.WorkTypeName, wt.Rate
                    from WorkDone wd
                    join Client c on c.Id = wd.ClientId
                    join WorkType wt on wt.Id = wd.WorkTypeId
                    order by StartedOn desc, ClientName asc
                ";

                using (var command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int clientId = reader.GetInt32(1);
                        int workTypeId = reader.GetInt32(2);
                        DateTimeOffset startedOn = reader.GetDateTimeOffset(3);
                        DateTimeOffset? endedOn = null;
                        if (!reader.IsDBNull(4))
                        {
                            endedOn = reader.GetDateTimeOffset(4);
                        }
                        string clientName = reader.GetString(5);
                        bool isActivated = reader.GetBoolean(6);
                        string workTypeName = reader.GetString(7);
                        decimal rate = reader.GetDecimal(8);

                        var client = new Client(clientId, clientName, isActivated);
                        var workType = new WorkType(workTypeId, workTypeName, rate);

                        WorkDone workDone = null;

                        if (endedOn != null)
                        {
                            workDone = new WorkDone(id, client, workType, startedOn, endedOn.Value);
                        }
                        else
                        {
                            workDone = new WorkDone(id, client, workType, startedOn);
                        }

                        workDoneList.Add(workDone);
                    }
                }
            }

            return workDoneList;
        }

        public WorkDone GetWorkDone(int id)
        {
            WorkDone workDone = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                    select wd.ClientId, wd.WorkTypeId,
	                    wd.StartedOn, wd.EndedOn, c.ClientName, c.IsActivated,
	                    wt.WorkTypeName, wt.Rate
                    from WorkDone wd
                    join Client c on c.Id = wd.ClientId
                    join WorkType wt on wt.Id = wd.WorkTypeId
                    where wd.Id = @Id
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int clientId = reader.GetInt32(0);
                            int workTypeId = reader.GetInt32(1);
                            DateTimeOffset startedOn = reader.GetDateTimeOffset(2);
                            DateTimeOffset? endedOn = null;
                            if (!reader.IsDBNull(3))
                            {
                                endedOn = reader.GetDateTimeOffset(3);
                            }
                            string clientName = reader.GetString(4);
                            bool isActivated = reader.GetBoolean(5);
                            string workTypeName = reader.GetString(6);
                            decimal rate = reader.GetDecimal(7);

                            var client = new Client(clientId, clientName, isActivated);
                            var workType = new WorkType(workTypeId, workTypeName, rate);

                            if (endedOn != null)
                            {
                                workDone = new WorkDone(id, client, workType, startedOn, endedOn.Value);
                            }
                            else
                            {
                                workDone = new WorkDone(id, client, workType, startedOn);
                            }
                        }
                    }
                }
            }

            return workDone;
        }

        public void Insert(WorkDone workDone)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                  insert WorkDone(ClientId, WorkTypeId, StartedOn, EndedOn)
                  values (@ClientId, @WorkTypeId, @StartedOn, @EndedOn)
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
                    command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
                    command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
                    command.Parameters.AddWithValue("@EndedOn", workDone.EndedOn ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(WorkDone workDone)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                  update WorkDone set
                    ClientId = @ClientId,
                    WorkTypeId = @WorkTypeId,
                    StartedOn = @StartedOn,
                    EndedOn = @EndedOn
                  where Id = @Id
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
                    command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
                    command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
                    command.Parameters.AddWithValue("@EndedOn", workDone.EndedOn ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Id", workDone.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}