using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Infrastructure.Repositories;
using Users.API.Model;

namespace Users.API.Infrastructure.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IUsersRepository _usersRepository;

        public QuestionsService(IQuestionsRepository questionsRepository, IUsersRepository usersRepository)
        {
            _questionsRepository = questionsRepository ?? throw new ArgumentNullException(nameof(questionsRepository));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }        

        public async Task<List<Questions>> GetQuestionListAsync()
        {
            return await _questionsRepository.GetQuestionListAsync();
        }

        public async Task<IPagedResult<Questions>> GetPagedAsync(PagedRequestDTO requestPaged, Dictionary<string, string> fieldsValues = null, DTO.Common.SortDTO sortData = null)
        {
            var filters = _usersRepository.GetFilters<Questions>(fieldsValues);
            var sort = _usersRepository.GetSortDirection<Questions>(sortData);

            return await _questionsRepository.GetPagedListAsync(requestPaged, filters, sort);
        }
    }
}
