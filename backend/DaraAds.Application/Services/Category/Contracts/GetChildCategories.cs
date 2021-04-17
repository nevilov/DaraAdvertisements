using System.Collections.Generic;

namespace DaraAds.Application.Services.Category.Contracts
{
    public static class GetChildCategories
    {
        public class Request
        {
            public int ParentCategoryId { get; set; }
        }

        public class Response
        {
            public class CategoryItem
            {
                public int Id { get; set; }

                public string Name { get; set; }
                
            }
            public IEnumerable<CategoryItem> CategoryItems { get; set; }
        }
    }
}
