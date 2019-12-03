using System.Collections.Generic;

namespace Users.API.DTO.Common
{
    public class PageResultDTO<T>
    {
        public List<T> Elements { get; set; }
        public int TotalElements { get; set; }

        public PageResultDTO()
        {
            TotalElements = 0;
            Elements = new List<T>();
        }
    }
}
