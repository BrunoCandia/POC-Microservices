using System.Collections.Generic;

namespace Users.API.DTO.Common
{
    public class FilterParamDTO
    {
        public bool SortAsc { get; set; }

        public string SortField { get; set; }

        public IList<FilterDTO> Filters { get; set; }
    }
}
