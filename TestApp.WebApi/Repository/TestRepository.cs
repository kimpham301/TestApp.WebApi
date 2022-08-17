using System.Collections;
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

        public async Task<IEnumerable> GetQuestions()
        {
            using (var connection = _context.CreateConnection())
            {
                var allQuestionQuery =
                    await connection.QueryAsync<Test>($@"SELECT * FROM Test");
                var questionList = allQuestionQuery.Select(x => new
                {
                    question_id = x.question_id,
                    question = x.question,
                    Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                }).OrderBy(y => Guid.NewGuid())
                    .Take(5)
                    .ToList();
                return questionList;
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

        public async Task<IEnumerable> RetrieveAnswers(int[] qnIds)
        {
            using (var connection = _context.CreateConnection())
            {
                var allAnswerQuery =
                    await connection.QueryAsync<Test>($@"SELECT * FROM Test");
                var questionList = allAnswerQuery
                    .Where(x=>qnIds.Contains(x.question_id))
                    .Select(y => new
                    {
                        question_id = y.question_id,
                        question = y.question,
                        Options = new string[] { y.Option1, y.Option2, y.Option3, y.Option4 },
                        answer = y.answer
                    })
                    .ToList();
                return questionList;
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
            var query = "INSERT INTO Test (question, answer, option1, option2, option3, option4) VALUES (@question, @answer , @option1, @option2, @option3, @option4)";

            var parameters = new DynamicParameters();
            parameters.Add("question", test.question, DbType.String);
            parameters.Add("answer", test.answer - 1, DbType.Int32);
            parameters.Add("option1", test.Option1, DbType.String);
            parameters.Add("option2", test.Option2, DbType.String);
            parameters.Add("option3", test.Option3, DbType.String);
            parameters.Add("option4", test.Option4, DbType.String);
            
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }

        }
    }
}