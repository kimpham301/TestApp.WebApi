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
    }
}