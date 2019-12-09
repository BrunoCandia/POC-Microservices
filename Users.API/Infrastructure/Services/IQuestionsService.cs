using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Model;

namespace Users.API.Infrastructure.Services
{
    public interface IQuestionsService
    {
        Task<List<Questions>> GetQuestionListAsync();

        Task<IPagedResult<Questions>> GetPagedAsync(PagedRequestDTO requestPaged, Dictionary<string, string> fieldsValues = null, DTO.Common.SortDTO sortData = null);
    }
}
