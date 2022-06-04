namespace Core.OrderAggregate
{
    
    /*
     * Daha onceden adres classimiz vardi fakat o identity ait.
     */
    public class Address
    {
        public Address(string firstName, string lastName, string street, string city, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        // ef need parameterless  constructor  for migration 
        public Address()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}