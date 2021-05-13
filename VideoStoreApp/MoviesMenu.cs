using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoStoreApp
{
    class MoviesMenu
    {

        public static void ShowMenu()
        {
            int opcion = 1;
            while (opcion != 0)
            {
                Console.WriteLine("\nWelcome to the most famous Videostore in your town! Log in to access the largest movie catalog on the internet!");
                Console.WriteLine("Select the option you want to perform:");
                Console.WriteLine("\n 1-Avaliable Films Catalog \n 2-Rent a movie \n 3-My reservations \n 4-Logout\n");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AvaliableFilms();
                        break;
                    case 2:
                        //RentMovie();
                        break;
                    case 3:
                        //CostumerReservations();
                        break;
                    case 4:
                        //Logout();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void AvaliableFilms()
        {
            string query = "SELECT * FROM Film "; //Este es el mensaje que se va a enviar
            DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

            while (sqlData.Reader.Read())
            {
                //Console.WriteLine(reader["CustomerID"].ToString()); //
                //Console.WriteLine(reader[0].ToString());//Esto nos devuelve la primera columna
                Console.WriteLine("Title:" + sqlData.Reader[1].ToString());//Esto nos devuelve la segunda columna
                Console.WriteLine("Synopsis:" + sqlData.Reader[2].ToString());
                Console.WriteLine("Recommended age: Over " + sqlData.Reader[3].ToString());
                Console.WriteLine("Avaliable:" + sqlData.Reader[4].ToString());
            }

            sqlData.Connection.Close(); //Segun se ejecuta el codigo, cerrar la conexion para mayor seguridad.
        }
    }
}
