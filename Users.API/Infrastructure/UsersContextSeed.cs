using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Users.API.Infrastructure.Mongo;
using Users.API.Model;
using Users.API.Resources;

namespace Users.API.Infrastructure
{
    public class UsersContextSeed
    {        
        private readonly IMongoContext _context;

        public UsersContextSeed(IMongoContext context)
        {
            _context = context;
        }

        public async Task SeedAsync(IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory)
        {            
            if (!_context.GetCollection<UsersModel>("UsersModel").AsQueryable().Any())
            {
                if (!_context.GetCollection<UsersModel>("UsersModel").Find(new BsonDocument()).ToList().Any())
                {
                    //await SetIndexesAsync();
                    await SetUsersAsync();
                }                
            }

            if (!_context.GetCollection<Questions>("Questions").AsQueryable().Any())
            {
                if (!_context.GetCollection<Questions>("Questions").Find(new BsonDocument()).ToList().Any())
                {
                    //await SetIndexesAsync();
                    await SetQuestionsAsync();
                }
            }
        }        

        private async Task SetIndexesAsync()
        {
            // Set location indexes
            var builder = Builders<UsersModel>.IndexKeys;
            var indexModel = new CreateIndexModel<UsersModel>(builder.Ascending(x => x.LastName));            
            await _context.GetCollection<UsersModel>("UsersModel").Indexes.CreateOneAsync(indexModel);
        }

        private async Task SetUsersAsync()
        {
            var userList = new List<UsersModel>{
                new UsersModel { UserId = 1, FirstName = "Juan", LastName = "Perez" },
                new UsersModel { UserId = 2, FirstName = "Pepe", LastName = "Lopez" },
                new UsersModel { UserId = 3, FirstName = "Ramon", LastName = "Diaz" },
                new UsersModel { UserId = 4, FirstName = "Ramon2", LastName = "Diaz" },
                new UsersModel { UserId = 5, FirstName = "Ramon3", LastName = "Diaz" },
                new UsersModel { UserId = 6, FirstName = "Ramon4", LastName = "Diaz" },
                new UsersModel { UserId = 7, FirstName = "Ramon5", LastName = "Diaz" },
                new UsersModel { UserId = 8, FirstName = "Ramon6", LastName = "Diaz" },
                new UsersModel { UserId = 9, FirstName = "Ramon7", LastName = "Diaz" },
                new UsersModel { UserId = 10, FirstName = "Ramon8", LastName = "Diaz" },
                new UsersModel { UserId = 11, FirstName = "Ramon9", LastName = "Diaz" },
                new UsersModel { UserId = 12, FirstName = "Ramon10", LastName = "Diaz" },
                new UsersModel { UserId = 13, FirstName = "Ramon11", LastName = "Diaz" },
                new UsersModel { UserId = 14, FirstName = "Ramon12", LastName = "Diaz" },
                new UsersModel { UserId = 15, FirstName = "Ramon13", LastName = "Diaz" }
            };

            await _context.GetCollection<UsersModel>("UsersModel").InsertManyAsync(userList);
        }

        private async Task SetQuestionsAsync()
        {
            var questionList = new List<Questions>{
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question1,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question2,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question3,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question4,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question5,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question6,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question7,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question8,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question9,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                },
                new Questions {
                    Id = Guid.NewGuid().ToString(),
                    QuestionText = QuestionsResource.Question10,
                    IsMultipleChoice = false,
                    Tags = "csharp"
                }                
            };

            await _context.GetCollection<Questions>("Questions").InsertManyAsync(questionList);
        }
    }
}
