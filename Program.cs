namespace AddressbookADONET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AddressBook BookOperation = new AddressBook();

            while (true)
            {
                Console.WriteLine("\n1. Create Contact" +
                    "\n2. Get All Data From DataBase" +
                    "\n3. Edit Data From DataBase" +
                    "\n4. Delete the Data From DataBase" +
                    "\n5.Exit the program");


                Console.Write("\nEnter option: ");
                int option = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");

                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter Firstname: - ");
                            string FirstName = Console.ReadLine();
                            Console.WriteLine("Enter LastName");
                            string LastName = Console.ReadLine();
                            Console.WriteLine("Enter PhoneNumber");
                            string PhoneNumber = Console.ReadLine();
                            Console.WriteLine("Enter Email:- ");
                            string Email = Console.ReadLine();
                            Console.WriteLine("Enter City:- ");
                            string City = Console.ReadLine();
                            Console.WriteLine("Enter Pincode:- ");
                            string Zip = Console.ReadLine();
                            Console.WriteLine("Enter State:- ");
                            string State = Console.ReadLine();

                            Contact contact = new Contact(FirstName, LastName, PhoneNumber, Email, City, State, Zip);
                            BookOperation.AddContact(contact);
                            break;
                        }
                    case 2:
                        {
                            List<Contact> contacts = BookOperation.DisplayContacts();
                            Console.WriteLine("Contact List:");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter the id to be edited: ");
                            int contactId = Convert.ToInt32(Console.ReadLine());
                            BookOperation.EditContact(contactId);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter the id to be deleted :");
                            int choice = Convert.ToInt32(Console.ReadLine());
                            BookOperation.DeleteContact(choice);
                            break;
                        }
                    case 5:
                        {
                            Environment.Exit(0);
                            break;
                        }

                    default:
                        break;
                }
            }
        }
    }
}