﻿
namespace Users.API.DTO.Common.Paging.Request
{
    public class PagedRequestDTO : FilterParamDTO
    {
        public PagedRequestDTO()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public PagedRequestDTO(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
		///     Gets or sets the index of the page.
		/// </summary>
		/// <value>
		///     The index of the page.
		/// </value>
		public int PageIndex { get; set; }

        /// <summary>
        ///     Gets or sets the size of the page.
        /// </summary>
        /// <value>
        ///     The size of the page.
        /// </value>
        public int PageSize { get; set; }
    }
}
