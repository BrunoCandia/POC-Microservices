using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Users.API.Infrastructure.Mongo;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public class QuestionsRepository : BaseRepository<Questions, string>, IQuestionsRepository
    {
        private readonly IMongoContext _context;

        public QuestionsRepository(IMongoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Questions>> GetQuestionListAsync()
        {
            return await _context.GetCollection<Questions>("Questions").Find(new BsonDocument()).ToListAsync();
        }
    }
}
