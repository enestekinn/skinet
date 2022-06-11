using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    public class Address
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        
        // One to One relationship
        // Required prevent nullable in database
        [Required]
        public string AppUserId { get; set; }
        public AppUser App { get; set; }
        
    }
}