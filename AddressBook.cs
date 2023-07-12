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

        public void EditContact(int id)
        {
            Console.WriteLine("Enter updated Firstname: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter updated LastName: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter updated PhoneNumber: ");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter updated Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter updated City: ");
            string city = Console.ReadLine();
            Console.WriteLine("Enter updated Pincode: ");
            string zip = Console.ReadLine();
            Console.WriteLine("Enter updated State: ");
            string state = Console.ReadLine();

            Contact updatedContact = new Contact(firstName, lastName, phoneNumber, email, city, state, zip);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string query = $"UPDATE contacts SET FirstName = '{updatedContact.FirstName}', " +
                               $"LastName = '{updatedContact.LastName}', " +
                               $"Email = '{updatedContact.Email}', " +
                               $"PhoneNumber = '{updatedContact.PhoneNumber}', " +
                               $"City = '{updatedContact.City}', " +
                               $"SState = '{updatedContact.SState}', " +
                               $"Zip = '{updatedContact.Zip}' " +
                               $"WHERE ID = {id}";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Contact updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update contact.");
                }
            }
        }

        public void DeleteContact(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string query = $"DELETE FROM contacts WHERE ID = {id}";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Contact deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete contact.");
                }
            }
        }

//----------------------------------------------------TRANSACTIONAL STOREDPROCEDURE-----------------------------------------------------
        public bool AddDataUsingNONTransactionStoreProcedure(string firstname, string lastname, string Email, string phoneNumber, string city, string state, string zip)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string Query = "AddOrderNewCustomerr";

                    using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(Query, connection, sqlTransaction))
                        {
                            try
                            {
                                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddWithValue("@FirstName", firstname);
                                sqlCommand.Parameters.AddWithValue("@LastName", lastname);
                                sqlCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                                sqlCommand.Parameters.AddWithValue("@Email", Email);
                                sqlCommand.Parameters.AddWithValue("@City", city);
                                sqlCommand.Parameters.AddWithValue("@SState", state);
                                sqlCommand.Parameters.AddWithValue("@Zip", zip);

                                int result = sqlCommand.ExecuteNonQuery();
                                sqlTransaction.Commit();
                                Console.WriteLine($"{result} rows affected");
                                Console.WriteLine("Data added .....");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Rolling Back the Changes");
                                sqlTransaction.Rollback();
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Something Went wrong....");
                return false;
            }
        }

        public List<Contact> DisplayContactsTransactionalStorePro()
        {
            List<Contact> contactslist = new List<Contact>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DisplayContact", sqlConnection, sqlTransaction))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
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
                            }
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

                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Rolling Back the Transaction");
                        sqlTransaction.Rollback();
                        Console.WriteLine(ex);
                    }
                }
            }

            return contactslist;
        }

        public bool EditContactTransactionalStorePro(int id)
        {
            Console.WriteLine("Enter updated Firstname: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter updated LastName: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter updated PhoneNumber: ");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter updated Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter updated City: ");
            string city = Console.ReadLine();
            Console.WriteLine("Enter updated Pincode: ");
            string zip = Console.ReadLine();
            Console.WriteLine("Enter updated State: ");
            string state = Console.ReadLine();

            Contact updatedContact = new Contact(firstName, lastName, phoneNumber, email, city, state, zip);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand("EditContact", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@FirstName", updatedContact.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", updatedContact.LastName);
                        cmd.Parameters.AddWithValue("@Email", updatedContact.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", updatedContact.PhoneNumber);
                        cmd.Parameters.AddWithValue("@City", updatedContact.City);
                        cmd.Parameters.AddWithValue("@SState", updatedContact.SState);
                        cmd.Parameters.AddWithValue("@Zip", updatedContact.Zip);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            Console.WriteLine("Contact updated successfully.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Failed to update contact.");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while updating the contact: " + ex.Message);
                    return false;
                }
            }
        }



    }
}
