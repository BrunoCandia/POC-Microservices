
using Users.API.DTO.Common;
using Users.API.DTO.Common.Paging.Request;

namespace Users.API.ViewModel
{
    public class UserRequest : PagedRequestDTO /*PageParamDTO*/
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}
