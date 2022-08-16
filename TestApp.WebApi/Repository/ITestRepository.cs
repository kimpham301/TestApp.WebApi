using System.Data;
using Dapper;
using TestApp.WebApi.Models;

namespace TestApp.WebApi.Repository
{
    public interface ITestRepository
    {
        public Task<IEnumerable<Test>> GetQuestions();

        public Task<Test> GetQuestion(int id);

        public Task<IEnumerable<Test>> RetrieveAnswers(int[] qnIds);
        public Task<int> AddQuestion(Test test);

        public Task<int> DeleteQuestion(int id);
        public class GenericArrayHandler<T> : SqlMapper.TypeHandler<T[]>
        {
            public override void SetValue(IDbDataParameter parameter, T[] value)
            {
                parameter.Value = value;
            }

            public override T[] Parse(object value) => (T[]) value;
        }
    }
    
}
