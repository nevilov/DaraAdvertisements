using System.Collections.Generic;

namespace DaraAds.Application.Services.Category.Contracts
{
    public static class GetCategoryById
    {
        public class Request
        {
            public int ParentCategoryId { get; set; }
        }

        public class Response
        {
            public ParentCategoryItem Parent { get; set; }

            public class ParentCategoryItem
            {
                public int Id { get; set; }

                public string Name { get; set; }

                public IEnumerable<ChildCategoryItem> ChildCategories { get; set; }

            }

            public class ChildCategoryItem
            {
                public int Id { get; set; }

                public string Name { get; set; }  
            }

        }
    }
}
