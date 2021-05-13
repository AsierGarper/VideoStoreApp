using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoStoreApp
{
    class LoginMenu
    {
        public static void Menu()
        {
            //aqui crear, con metodos, el login, el register y el logout. Si es satisfactorio el login, pasar a MoviesMenu
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Welcome to the most famous Videostore in your town! Log in to access the largest movie catalog on the internet!");
                Console.WriteLine("Select the option you want to perform:");
                Console.WriteLine("\n 1-Login \n 2-Register \n 3-Logout\n");
                int selection = Convert.ToInt32(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        LogIn();
                        break;
                    case 2:
                        Register();
                        break;
                    case 3:
                        //Logout(); Deberia hacer que solo apareciera una vez logeados en el menu
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
                Console.WriteLine("Introduce your identification number (DNI): \t\t\t\t\t (To go back to the menu, press 'back')");
                string customerDni = Console.ReadLine();
                if (customerDni.ToLower() == "back") //This 'if-else' is just to let the user go back to the main menu. We convert the string ToLower just if the user writes 'Back' or 'BACK'
                {
                    Menu();
                }
                else
                {
                    string query = $"SELECT * FROM Customer where Dni = '{customerDni}'";

                    DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

                    if (sqlData.Reader.Read())
                    {
                        sqlData.Connection.Close();//Si el DNI existe, cerramos conexion.
                        loginMenu = false; //De esta forma, cuando exista el DNI en la BD, evitamos que vuelva entrar en el while.
                        Console.WriteLine("Successful login.");
                        MoviesMenu.ShowMenu();//Si el inicio de sesion es correcto, enseniar el menu de peliculas
                    }
                    else
                    {
                        Console.WriteLine("Incorrect login. Please try again.");
                    }
                    sqlData.Connection.Close(); //Si no existe el DNI, cerramos conexion, y vuelve al while 
                }

            }

        }

        public static void Register()
        {
            bool loginMenu = true;
            while (loginMenu)
            {
                Console.WriteLine("To create a new user, introduce your identification number (DNI): \t\t (To go back to the menu, press 'back')");
                string newDni = Console.ReadLine();//MEJORA, PEDIR DNI DE 9 CARACTERES MAXIMO
                if (newDni.ToLower() == "back") //This 'if-else' is just to let the user go back to the main menu. We convert the string ToLower just if the user writes 'Back' or 'BACK'
                {
                    Menu();
                }
                else
                {
                    string query = $"SELECT * FROM Customer where Dni = '{newDni}'";

                    DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

                    if (sqlData.Reader.Read())
                    {
                        Console.WriteLine("DNI already in use.");//MEJORA POSIBLE: DAR AL USUARIO LA OPCION DE SALTAR A LOG IN
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
                        //Aqui vamos a meter un codigo sacado de internet, para que aparezcan * en vez de la contrasenia:
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
                        //-----------------------------------La variable newPassword es en formato *****, la variable 'convertedPassword' es la convertida a string (la legible), la que pasamos al query--------------------------------
                        //Aqui, recogemos las variables del cliente para registrarlo en la Base de datos.
                        string queryInsert = $"INSERT INTO Customer (Dni, Name, LastName, Birthday, Mail, Password) VALUES ('{newDni}','{newName}', '{newLastName}', '{newBirthday}', '{newMail}','{convertedPassword}')";
                        DTOReaderAndConnection sqlData2 = DatabaseConnections.QueryExecute(queryInsert);
                        Console.WriteLine("Registration completed! You will be redirected to the main menu. Log in to book your first movie.\n");
                        Menu();
                    }
                    sqlData.Connection.Close(); //Si no existe el DNI, cerramos conexion, y vuelve al while 
                }

            }

        }
    }
}
