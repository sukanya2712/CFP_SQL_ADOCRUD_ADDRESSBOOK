using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookADONET
{
        public class Contact
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string SState { get; set; }
            public string Zip { get; set; }




            public Contact(string First, string Last, string EmailL, string PhoneNumberr, string Cityy, string Statee, string Zipp)
            {

                FirstName = First;
                LastName = Last;
                Email = EmailL;
                PhoneNumber = PhoneNumberr;
                City = Cityy;
                SState = Statee;
                Zip = Zipp;
            }

            public Contact()
            {

            }

        }
}
