using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Users.API.Controllers;
using Users.API.Infrastructure.Services;
using Users.API.Model;

namespace Users.API.UnitTests.Controllers
{
    [TestFixture]
    public class UsersControllerTest
    {
        private Mock<IUsersService> usersServiceMock = null;
        private UsersController usersController = null;

        [SetUp]
        public void BaseSetUp()
        {
            //usersServiceMock = new Mock<IUsersService>();

            //usersServiceMock.Setup(x => x.GetUserAsync(It.IsAny<int>())).Returns(Task.FromResult(new UsersModel { UserId = 1, FirstName = "irrelevant", LastName = "irrelevant" }));

            //usersController = new UsersController(usersServiceMock.Object);
        }

        [TearDown]
        public void BaseTearDown() 
        {
            usersServiceMock = null;
            usersController = null;
        }

        [Test]
        public async Task GivenUserId_WhenGet_ThenReturnUser()
        {
            //var result1 = await usersController.GetUserAsync(1).ConfigureAwait(false);
            //var result2 = usersController.GetUserAsync(1).Result;

            //Assert.IsNotNull(result1);
            //Assert.IsTrue(result1.Value.UserId == 1);

            //Assert.IsNotNull(result2);
            //Assert.IsTrue(result2.Value.UserId == 1);
        }
    }
}
