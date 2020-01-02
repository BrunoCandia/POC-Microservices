using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.API.DTO.Common;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Infrastructure.Services;
using Users.API.Model;
using Users.API.ViewModel;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [Produces("application/json")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService ?? throw new ArgumentNullException(nameof(questionsService));
        }

        //GET api/[controller]/
        //[Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Questions>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Questions>>> GetAllQuestionAsync()
        {
            try
            {
                return await _questionsService.GetQuestionListAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }            
        }

        //GET api/[controller]/
        [Route("GetPagedAsync")]
        [HttpPost]
        [ProducesResponseType(typeof(IPagedResult<Questions>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IPagedResult<Questions>>> GetPagedAsync([FromBody] QuestionRequest questionRequest)
        {
            try
            {
                var requestPaged = new PagedRequestDTO(questionRequest.PageIndex, questionRequest.PageSize);                

                var fieldsValues = new Dictionary<string, string>();
                if (questionRequest.Filters.Any())
                {
                    foreach (var filter in questionRequest.Filters)
                    {
                        fieldsValues.Add(filter.PropertyName, filter.PropertyValue);
                    }
                }

                var sortData = new SortDTO();
                if (questionRequest?.SortData != null)
                {
                    sortData.SortDirection = questionRequest.SortData.SortDirection;
                    sortData.SortField = questionRequest.SortData.SortField;
                }
                
                var result = await _questionsService.GetPagedAsync(requestPaged, fieldsValues, sortData);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    new Questions { QuestionText = "Question1 V1" },
                    new Questions { QuestionText = "Question2 V1" },
                    new Questions { QuestionText = "Question3 V1" }
                };

                return questionsList;
            });
        }
    }
}