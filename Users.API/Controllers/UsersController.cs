using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Infrastructure.Services;
using Users.API.Model;
using Users.API.ViewModel;

namespace Users.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]    
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        //GET api/v1/[controller]/
        //[Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(List<UsersModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UsersModel>>> GetAllUserAsync()
        {
            return await _usersService.GetAllUserAsync();
        }

        //GET api/v1/[controller]/1
        [Route("{userId}")]
        [HttpGet]
        [ProducesResponseType(typeof(UsersModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UsersModel>> GetUserAsync(int userId)
        {
            var user = await _usersService.GetUserAsync(userId);

            if (user != null)
            {
                return user;
            }

            return NotFound();
        }        

        // GET: api/Users
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Users/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Users
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/Users/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
