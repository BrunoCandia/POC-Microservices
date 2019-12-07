using System.Collections.Generic;

namespace Users.API.DTO.Common
{
    public class FilterParamDTO
    {        
        //public Sort SortDirection { get; set; }

        //public string SortField { get; set; }

        public SortDTO SortData { get; set; }

        public IList<FilterDTO> Filters { get; set; }
    }

    public enum Sort
    {
        Asc = 1,
        Desc = -1
    }
    
    public class SortDTO
    {
        public Sort SortDirection { get; set; }

        public string SortField { get; set; }
    }
}
