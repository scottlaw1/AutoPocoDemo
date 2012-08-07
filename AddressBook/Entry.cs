using System.Collections.Generic;

namespace AddressBook
{
    public class Entry
    {
        public Entry()
        {
            Addresses = new List<Address>();
        }

        public Person Person { get; set; }
        public List<Address> Addresses { get; set; } 
    }
}
