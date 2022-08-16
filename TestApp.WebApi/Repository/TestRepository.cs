using TestApp.WebApi.Models;
using TestApp.WebApi.Data;
using TestApp.WebApi.Authorization;
using System.Data;
using Dapper;

namespace TestApp.WebApi.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly TestApiDbContext _context;

        public TestRepository(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetQuestions()
        {
            var query = "SELECT question_id, question, answer FROM Test";
            using (var connection = _context.CreateConnection())
            {
                var randomQues = await connection.QueryAsync<Test>(query);
                return randomQues.ToList();
            }
        }

        public async Task<Test> GetQuestion(int id)
        {
            var query = "SELECT * FROM Test WHERE question_id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var question = await connection.QuerySingleOrDefaultAsync<Test>(query, new { id });
                return question;
            }
        }

        public async Task<IEnumerable<Test>> RetrieveAnswers(int[] qnIds)
        {
            var query = "SELECT answer, question FROM Test WHERE question_id = ANY(@qnIds)";
            using (var connection = _context.CreateConnection())
            {
                var answer = await connection.QueryAsync<Test>(query, new{qnIds});
                return answer.ToList();
            }
        }

        public async Task<int> DeleteQuestion(int id)
        {
            var query = $@"DELETE FROM Test WHERE question_id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query);
            }
        }

        public async Task<int> AddQuestion(Test test)
        {
            var query = $@"INSERT INTO Test (question_id, question, Options, answer) 
                            VALUES ('{test.question_id}','{test.question}',ARRAY{test.Options}, {test})";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query);
            }
        }
}
}