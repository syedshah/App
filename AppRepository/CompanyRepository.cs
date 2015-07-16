using Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppRepository
{
    public class CompanyRepository : ICompanyRepository
    {
        private string _connectionString;
        

        public CompanyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Company GetById(int id)
        {
            Company company = new Company();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("uspGetCompanyById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    SqlParameter parameter = new SqlParameter("@CompanyId", SqlDbType.Int) {Value = id};
                    command.Parameters.Add(parameter);

                    connection.Open();
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        company = new Company
                        {
                            Id = int.Parse(reader["CompanyId"].ToString()),
                            Name = reader["Name"].ToString(),
                            Classification = (Classification) int.Parse(reader["ClassificationId"].ToString())
                        };
                    }
                }
            }

            return company;
        }
    }
}
