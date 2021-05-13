using System;
using System.Collections.Generic;
using DaraAds.Application.Common;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class GetPages
    {
        public sealed class Request : Paged.Request
        {
            public string SortBy { get; set; }
            public string SortDirection { get; set; }
            public string SearchString { get; set; }
            public int CategoryId { get; set; }
            public decimal MinPrice { get; set; }
            public decimal MaxPrice { get; set; }
            public DateTime MinDate { get; set; }
            public DateTime MaxDate { get; set; }
        }

        public sealed class Response : Paged.Response<Response.Item>
        {
            public sealed class OwnerResponse
            {
                public string Id { get; set; }
                public string Email { get; set; }
                public string Name { get; set; }
                public string Lastname { get; set; }
                public string Username { get; set; }
                public string Avatar { get; set; }
            }

            public sealed class CategoryResponse
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            public sealed class ImageResponse
            {
                public string Id { get; set; }
                public string ImageUrl { get; set; }
            }

            public sealed class Item
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public string Cover { get; set; }
                public decimal Price { get; set; }
                public string Status { get; set; }
                public string Location { get; set; }
                public DateTime CreatedDate { get; set; }
                public OwnerResponse Owner { get; set; }
                public IEnumerable<ImageResponse> Images { get; set; }
                public CategoryResponse Category { get; set; }
            }
        }
    }
}