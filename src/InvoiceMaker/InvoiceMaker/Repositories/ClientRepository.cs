using System.Collections.Generic;
using InvoiceMaker.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace InvoiceMaker.Repositories
{
    public class ClientRepository
    {
        private string _connectionString;

        public ClientRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public IList<Client> GetClients()
        {
            var clients = new List<Client>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                    select Id, ClientName, IsActivated
                    from Client
                    order by ClientName
                ";

                using (var command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        bool isActivated = reader.GetBoolean(2);
                        var client = new Client(id, name, isActivated);
                        clients.Add(client);
                    }
                }
            }

            return clients;
        }

        public Client GetClient(int id)
        {
            Client client = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                    select ClientName, IsActivated
                    from Client
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
                            bool isActivated = reader.GetBoolean(1);
                            client = new Client(id, name, isActivated);
                        }
                    }
                }
            }

            return client;
        }

        public void Insert(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                  insert Client(ClientName, IsActivated)
                  values (@ClientName, @IsActivated)
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClientName", client.Name);
                    command.Parameters.AddWithValue("@IsActivated", client.IsActive);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                  update Client set 
                    ClientName = @ClientName, 
                    IsActivated = @IsActivated
                  where Id = @Id
                ";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClientName", client.Name);
                    command.Parameters.AddWithValue("@IsActivated", client.IsActive);
                    command.Parameters.AddWithValue("@Id", client.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}