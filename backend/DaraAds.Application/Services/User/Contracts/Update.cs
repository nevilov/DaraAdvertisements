namespace DaraAds.Application.Services.User.Contracts
{
    public static class Update
    { 
        public class Request
        {        
            public string Name { get; set; }
            
            public string LastName { get; set; }

            public string Phone { get; set; }
        }
    }
}
