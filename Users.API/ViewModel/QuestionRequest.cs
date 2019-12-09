using Users.API.DTO.Common.Paging.Request;

namespace Users.API.ViewModel
{
    public class QuestionRequest : PagedRequestDTO
    {
        public string QuestionText { get; set; }
        
        public string Tags { get; set; }
    }
}
