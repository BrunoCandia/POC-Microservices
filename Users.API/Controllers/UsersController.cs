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
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        //GET api/[controller]/
        [MapToApiVersion("1")]
        //[Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(List<UsersModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UsersModel>>> GetAllUserAsync()
        {
            return await _usersService.GetAllUserAsync();
        }

        //GET api/[controller]/1
        [MapToApiVersion("1")]
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

        //GET api/v1/[controller]/
        [Route("GetAllAsync")]
        [HttpGet]
        [ProducesResponseType(typeof(List<UsersModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UsersModel>>> GetAllAsync()
        {
            var users = await _usersService.GetAllAsync();

            return users.ToList();
        }

        //GET api/[controller]/
        [MapToApiVersion("1")]
        [Route("GetPagedAsync")]
        [HttpPost]
        [ProducesResponseType(typeof(IPagedResult<UsersModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IPagedResult<UsersModel>>> GetPagedAsync([FromBody] UserRequest userRequest)
        {
            try
            {
                var requestPaged = new PagedRequestDTO(userRequest.PageIndex, userRequest.PageSize);
                var requestFilter = userRequest.LastName;

                var fieldsValues = new Dictionary<string, string>();
                if (userRequest.Filters.Any())
                {                    
                    foreach (var filter in userRequest.Filters)
                    {
                        fieldsValues.Add(filter.PropertyName, filter.PropertyValue);
                    }
                }

                var sortData = new SortDTO();
                if (userRequest?.SortData != null)
                {
                    sortData.SortDirection = userRequest.SortData.SortDirection;
                    sortData.SortField = userRequest.SortData.SortField;
                }
                                
                var result = await _usersService.GetPagedAsync(requestPaged, fieldsValues, sortData);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        //GET api/[controller]/
        [MapToApiVersion("1")]
        [Route("GetDummyUserAsync")]
        [HttpGet]
        [ProducesResponseType(typeof(List<UsersModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UsersModel>>> GetDummyUserAsync()
        {
            var list = await GetMockedData();

            return Ok(list);
        }

        //GET api/[controller]/
        [MapToApiVersion("2")]
        [Route("GetDummyUserAsync_V2")]
        [HttpGet]
        [ProducesResponseType(typeof(List<UsersModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UsersModel>>> GetDummyUserAsync_V2()
        {
            var list = await GetMockedData_V2();

            return Ok(list);
        }

        private Task<List<UsersModel>> GetMockedData()
        {
            return Task.Run(() => {
                var usersList = new List<UsersModel> {
                    new UsersModel { FirstName = "Juan", LastName = "Perez V1" },
                    new UsersModel { FirstName = "Pepe", LastName = "Lopez V1" },
                    new UsersModel { FirstName = "Ramon", LastName = "Diaz V1" }
                };

                return usersList;
            });
        }

        private Task<List<UsersModel>> GetMockedData_V2()
        {
            return Task.Run(() => {
                var usersList = new List<UsersModel> {
                    new UsersModel { FirstName = "Juan", LastName = "Perez V2" },
                    new UsersModel { FirstName = "Pepe", LastName = "Lopez V2" },
                    new UsersModel { FirstName = "Ramon", LastName = "Diaz V2" },
                    new UsersModel { FirstName = "Lucia", LastName = "Filot V2" },
                    new UsersModel { FirstName = "Ana", LastName = "Sat V2" }
                };

                return usersList;
            });
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
