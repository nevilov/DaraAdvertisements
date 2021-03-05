﻿namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class Get
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }
        public sealed class Response
        {
            public sealed class OwnerResponse
            {
                public string Id { get; set; }
//                public string Email { get; set; }
                public string Name { get; set; }
                public string LastName { get; set; }
            }
            
            public sealed class Category
            {
                public int Id { get; set; }
                
                public string Name { get; set; }
            }

            public string Title { get; set; }
            public string Description { get; set; }
            public string Cover { get; set; }
            public string Status { get; set; }
            public decimal Price { get; set; }
            public Category Cat { get; set; }
            public OwnerResponse Owner { get; set; }
        }
    }
}