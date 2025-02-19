using Microsoft.AspNetCore.Identity;

namespace WatchLk.AuthProcessing.Domains.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public required string FirstName { get; set; }

        [PersonalData]
        public required string LastName { get; set; }

        public IList<Address> Addresses { get; set; } = [];
    }

}
