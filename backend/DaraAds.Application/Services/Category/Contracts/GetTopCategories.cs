using System.Collections.Generic;

namespace DaraAds.Application.Services.Category.Contracts
{
    public class GetTopCategories
    {
        public class Response
        {
            public IEnumerable<TopCategory> Categories { get; set; }

            public class TopCategory
            {
                public int Id { get; set; }

                public string Name { get; set; }

                public IEnumerable<ChildCategories> ChildCategories { get; set; }
            }

            public class ChildCategories
            {
                public int Id { get; set; }

                public string Name { get; set; }
            }
        }
    }
}
