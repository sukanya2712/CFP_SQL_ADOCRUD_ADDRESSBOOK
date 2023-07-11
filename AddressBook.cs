using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookADONET
{
    public class AddressBook
    {
        private string connectionString = "Data Source=DESKTOP-41GBJMF; Database=AddressBookk; Integrated Security=true";

        public void AddContact(Contact contact)
        {
            string connection = $"Data source= DESKTOP-41GBJMF; Database = AddressBookk; Integrated Security = true ";

            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();


            string query = $"INSERT INTO contacts VALUES ('{contact.FirstName}','{contact.LastName}','{contact.Email}','{contact.PhoneNumber}','{contact.City}','{contact.SState}','{contact.Zip}')";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine($"{result} is affected");
            }

            sqlConnection.Close();


        }

        public List<Contact> DisplayContacts()
        {
            List<Contact> contactslist = new List<Contact>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "SELECT * FROM contacts";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Contact contact = new Contact()
                    {
                        ID = (int)reader["ID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        City = (string)reader["City"],
                        SState = (string)reader["SState"],
                        Zip = (string)reader["Zip"],
                    };

                    contactslist.Add(contact);
                }
                foreach (Contact contact in contactslist)
                {
                    Console.WriteLine($"ID: {contact.ID}");
                    Console.WriteLine($"First Name: {contact.FirstName}");
                    Console.WriteLine($"Last Name: {contact.LastName}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                    Console.WriteLine($"City: {contact.City}");
                    Console.WriteLine($"State: {contact.SState}");
                    Console.WriteLine($"Zip: {contact.Zip}");
                    Console.WriteLine("----------------------");
                }
            }

            return contactslist;
        }


    }
}
