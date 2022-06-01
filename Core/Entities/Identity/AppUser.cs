using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{

    // identity yi kullanmak icin identityuser sinifindan derive almaliyiz.
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address  Address { get; set; }
    }
}