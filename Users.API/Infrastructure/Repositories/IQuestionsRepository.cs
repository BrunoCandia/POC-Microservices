using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public interface IQuestionsRepository : IRepository<Questions, string>
    {
        Task<List<Questions>> GetQuestionListAsync();
    }
}
