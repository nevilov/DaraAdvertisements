using System.Collections.Generic;
using DaraAds.Application.Common;

namespace DaraAds.Application 
{
    public static class Paged
    {
        public abstract class Request
        {
            public int Offset { get; set; } = PagedConstants.PaginationOffset;
            public int Limit { get; set; } = PagedConstants.PaginationLimit;
        }

        public abstract class Response<T>
        {
            public int Total { get; set; }
            public int Limit { get; set; }
            public int Offset { get; set; }
            
            public IEnumerable<T> Items { get; set; }
        } 
    }
}