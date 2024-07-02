﻿namespace Talabat.Core.Specefication
{
    public class SpecParameter
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private int pageSize=5;

        public int PageSize 
        {
            get { return pageSize; }
            set { pageSize= value>10?10:value; }
        }

        public int PageIndex { get; set; } = 1;
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower().Trim(); }
        }


    }
}