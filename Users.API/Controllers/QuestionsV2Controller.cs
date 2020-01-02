using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.API.Infrastructure.Services;
using Users.API.Model;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2")]
    public class QuestionsV2Controller : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsV2Controller(IQuestionsService questionsService)
        {
            _questionsService = questionsService ?? throw new ArgumentNullException(nameof(questionsService));
        }

        //GET api/[controller]/        
        [Route("GetDummyQuestionAsync")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Questions>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Questions>>> GetDummyQuestionAsync()
        {
            var list = await GetMockedData();

            return Ok(list);
        }

        private Task<List<Questions>> GetMockedData()
        {
            return Task.Run(() => {
                var questionsList = new List<Questions> {
                    new Questions { QuestionText = "Question1 V2" },
                    new Questions { QuestionText = "Question2 V2" },
                    new Questions { QuestionText = "Question3 V2" },
                    new Questions { QuestionText = "Question4 V2" },
                    new Questions { QuestionText = "Question5 V2" }
                };

                return questionsList;
            });
        }
    }
}