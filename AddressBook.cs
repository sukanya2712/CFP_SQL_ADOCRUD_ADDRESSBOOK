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
        //private string connectionString = "Data Source=DESKTOP-41GBJMF; Database=AddressBookk; Integrated Security=true";

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

    }
}
