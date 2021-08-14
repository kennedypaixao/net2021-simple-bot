using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace SimpleBotCore.Repositories
{
    public class QuestionSqlRepository : IQuestionSqlRepository
    {
        private string _connectionString;

        public QuestionSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Create(string question, int position)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"INSERT INTO QUESTION (Description, Position) VALUES (@Description, @Position);";

                var parameters = new DynamicParameters();
                parameters.Add("@Description", question);
                parameters.Add("@Position", position);

                await db.QueryAsync(query, parameters);
            }
        }
    }
}
