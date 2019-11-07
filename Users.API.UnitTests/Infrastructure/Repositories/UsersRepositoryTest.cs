using Moq;
using NUnit.Framework;
using Users.API.Infrastructure;
using Users.API.Infrastructure.Repositories;

namespace Users.API.UnitTests.Infrastructure.Repositories
{
    [TestFixture]
    public class UsersRepositoryTest
    {
        private Mock<IUsersContext> usersContext = null;
        private readonly IUsersRepository usersRepository = null;

        [SetUp]
        public void BaseSetUp()
        {
            usersContext = new Mock<IUsersContext>();


            //usersRepository = new UsersRepository()

            //questionsServiceMock = new Mock<IQuestionsService>();
            //questionsController = new QuestionsController(questionsServiceMock.Object);
        }

        [TearDown]
        public void BaseTearDown()
        {
            //questionsServiceMock = null;
            //questionsController = null;
        }
    }
}
