namespace DaraAds.Application.Services.User.Contracts
{
    public static class Update
    { 
        public class Request
        {
            public string Id { get; set; }
            
            public string Name { get; set; }
            
            public string LastName { get; set; }
            
            public string Avatar { get; set; }
            
            public string Phone { get; set; }
        }
    }
}
