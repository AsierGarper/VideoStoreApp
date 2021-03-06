using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoStoreApp
{
    class LoginMenu
    {
        public static void MainMenu()
        {
            //here create, with methods, the login, the register and the logout. If the login is successful, go to MoviesMenu.
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Welcome to the most famous Videostore in your town! Log in to access the largest movie catalog on the internet!");
                Console.WriteLine("Select the option you want to perform:");
                Console.WriteLine("\n 1-Login \n 2-Register\n");
                int selection = Convert.ToInt32(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        LogIn();
                        break;
                    case 2:
                        Register();
                        break;
                    default:
                        break;
                }
            }
        }
        public static void LogIn()
        {
            bool loginMenu = true;
            while (loginMenu)
            {
                Console.WriteLine("Introduce your identification number (DNI): \t\t\t\t\t (To go back to the menu, type 'back')");
                string customerDni = Console.ReadLine();
                if (customerDni.ToLower() == "back") //This 'if-else' is just to let the user go back to the main menu. We convert the string ToLower just if the user writes 'Back' or 'BACK'
                {
                    MainMenu();
                }
                else
                {
                    string query = $"SELECT * FROM Customer where Dni = '{customerDni}'";

                    DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

                    if (sqlData.Reader.Read())
                    {
                        //If the DNI is correct, we will save the data in the Customer customerData object, and this follow the flow of the program with the credentials of THAT user, or until he/she logs out.
                        Customer customerData = new Customer(
                            Convert.ToInt32(sqlData.Reader["Customer_id"]),
                            Convert.ToString(sqlData.Reader["Dni"]),
                            Convert.ToString(sqlData.Reader["Name"]),
                            Convert.ToString(sqlData.Reader["LastName"]),
                            Convert.ToDateTime(sqlData.Reader["Birthday"]),
                            Convert.ToString(sqlData.Reader["Mail"]),
                            Convert.ToString(sqlData.Reader["Password"])
                            );

                        //And this way, we have stored in the Customer class the values of the user navigating through the application. These values are going to be able to be called from anywhere, using the Customer class as a bridge.

                        sqlData.Connection.Close();//If the DNI exists, we close the connection.
                        loginMenu = false; //In this way, when the DNI exists in the DB, we prevent it from re-entering the while.
                        Console.WriteLine("Successful login.");
                        MoviesMenu.ShowMenu(customerData);//If the login is correct, show the movie menu.
                    }
                    else
                    {
                        Console.WriteLine("Incorrect login. Please try again.");
                    }
                    sqlData.Connection.Close(); //If the DNI does not exist, close the connection, and return to while 
                }
            }
        }

        public static void Register()
        {
            bool loginMenu = true;
            while (loginMenu)
            {
                Console.WriteLine("To create a new user, introduce your identification number (DNI): \t\t (To go back to the menu, press 'back')");
                string newDni = Console.ReadLine();//IMPROVEMENT, ASK FOR A 9-CHARACTER ID MAX.
                if (newDni.ToLower() == "back") //This 'if-else' is just to let the user go back to the main menu. We convert the string ToLower just if the user writes 'Back' or 'BACK'
                {
                    MainMenu();
                }
                else
                {
                    string query = $"SELECT * FROM Customer where Dni = '{newDni}'";

                    DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

                    if (sqlData.Reader.Read())
                    {
                        Console.WriteLine("DNI already in use.");//POSSIBLE IMPROVEMENT: GIVE THE USER THE OPTION TO SKIP TO LOG IN
                    }
                    else
                    {
                        sqlData.Connection.Close();//If the DNI exist, we ask for the rest of data
                        Console.WriteLine("DNI registered. Please introduce your name.");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Introduce your last name.");
                        string newLastName = Console.ReadLine();
                        Console.WriteLine("Introduce your last Birthday.");
                        DateTime newBirthday = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Introduce your mail.");
                        string newMail = Console.ReadLine();
                        Console.WriteLine("Introduce your Password.");
                        //Here we are going to enter a code taken from the internet, so that * appears instead of the password:
                        //-----------------------------------------------
                        char[] newPassword = new char[35]; //Assign the max length of password you want
                        ConsoleKeyInfo keyinfo = new ConsoleKeyInfo();
                        for (int i = 0; i < newPassword.Length; i++)
                        {
                            keyinfo = Console.ReadKey(true); //Small mistake, I cant delete characters mean Im introducing the password

                            if (!keyinfo.Key.Equals(ConsoleKey.Enter))

                            {
                                newPassword[i] = keyinfo.KeyChar;

                                Console.Write("*");
                            }
                            else
                            {
                                break;
                            }
                        }
                        string convertedPassword = new string(newPassword);
                        //-----------------------------------The variable newPassword is in format *****, the variable 'convertedPassword' is the one converted to string (the readable one), the one that we pass to the query--------------------------------
                        //Here, we collect the variables of the client to register it in the database.
                        string queryInsert = $"INSERT INTO Customer (Dni, Name, LastName, Birthday, Mail, Password) VALUES ('{newDni}','{newName}', '{newLastName}', '{newBirthday}', '{newMail}','{convertedPassword}')";
                        DTOReaderAndConnection sqlData2 = DatabaseConnections.QueryExecute(queryInsert);
                        sqlData2.Connection.Close();
                        Console.WriteLine("\nRegistration completed! You will be redirected to the main menu. Log in to book your first movie.\n");
                        MainMenu();
                    }
                    sqlData.Connection.Close(); //If the DNI does not exist, close the connection, and return to while 
                }

            }

        }

    }

}
