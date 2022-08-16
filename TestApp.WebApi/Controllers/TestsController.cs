using Microsoft.AspNetCore.Mvc;
using TestApp.WebApi.Models;
using TestApp.WebApi.Repository;
using TestApp.WebApi.Services;

namespace TestApp.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestRepository _testRepository;

        public TestsController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions(){
            try
            {
                var questions = await _testRepository.GetQuestions();
                if (questions == null)
                {
                    return BadRequest("There's no test");
                }

                return Ok(questions);
            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
        }
        
        [HttpGet("{id}", Name = "QuestionById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var question = await _testRepository.GetQuestion(id);
                if (question == null)
                    return NotFound();
                return Ok(question);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("answerrs")]
        public async Task<IActionResult> RetrieveAnswers(int[] qnIds)
        {
            try
            {
                var answers = await _testRepository.RetrieveAnswers(qnIds);
                if (answers == null)
                    return NotFound();
                return Ok(answers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var questionToDelete = await _testRepository.GetQuestion(id);
                if (questionToDelete == null)
                    return NotFound();

                await _testRepository.DeleteQuestion(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}