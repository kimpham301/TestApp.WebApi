﻿using System.Collections;

using TestApp.WebApi.Models;

namespace TestApp.WebApi.Repository
{
    public interface ITestRepository
    {
        public Task<IEnumerable> GetQuestions();

        public Task<Test> GetQuestion(int id);

        public Task<IEnumerable> RetrieveAnswers(int[] qnIds);
        public Task<int> AddQuestion(Test test);

        public Task<int> DeleteQuestion(int id);
    }
    
}
