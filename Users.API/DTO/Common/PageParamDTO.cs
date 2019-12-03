
namespace Users.API.DTO.Common
{
    public class PageParamDTO : FilterParamDTO
    {
        public int FirstRecord { get; set; }

        public int RecordsAmount { get; set; }
    }
}
