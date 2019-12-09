using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.DTO.Common;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Infrastructure.Services;
using Users.API.Model;
using Users.API.ViewModel;

namespace Users.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService ?? throw new ArgumentNullException(nameof(questionsService));
        }

        //GET api/v1/[controller]/
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

        //GET api/v1/[controller]/
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
    }
}