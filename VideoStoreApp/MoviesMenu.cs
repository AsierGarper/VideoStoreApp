using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoStoreApp
{
    class MoviesMenu
    {
        //● Ver películas disponibles: Aquí tendremos que desplegar una lista de películas teniendo en cuenta la edad del usuario, si tiene menos de 18 años le aparecerán las películas recomendadas para esa edad, si tiene menos de 16, las recomendadas para menores de 16, etc. Aparecerán todas las películas de la BBDD incluidas las que están alquiladas.
        //Si el usuario selecciona una de ellas, se le mostrará todos los datos referentes a esa película.
        //● Alquilar película: aquí podrán alquilar la película que deseen siempre que esté disponible y la edad recomendada sea la adecuada. Una vez alquilada, la película deberá pasar a modo no disponible.
        //● Mis alquileres: aquí podrán ver las películas que tienen alquiladas y cuando vence el plazo de alquiler.Cuando el plazo haya expirado, les tendrá que aparecer la película en color rojo.Podrán entregar la película que hayan alquilado, la cual se volverá a poner en modo disponible.
        //● Logout.
        public static void ShowMenu()
        {
            int opcion = 1;
            while (opcion != 0)
            {
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
        public static int CustomerAge()
        {
            //Here, I have to calculate the age of the user who is using the application. 
            //And based on that age, show him the movies whose recommended age does not exceed the age of the client.
            

            return CustomerAge;
        }
        public static void AvaliableFilms() //COMO PUEDO GUARDAR LAS PELICULAS RECIBIDAS EN UNA LISTA? y PARA QUE ES?
        {
            
            string query = "SELECT * FROM Film "; //Este es el mensaje que se va a enviar
            DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

            while (sqlData.Reader.Read())
            {
                Console.WriteLine("\nTitle:" + sqlData.Reader[1].ToString());//This returns the first column
                Console.WriteLine("Synopsis:" + sqlData.Reader[2].ToString());//This returns the second column, and so on.
                Console.WriteLine("Recommended age: Over " + sqlData.Reader[3].ToString());
                Console.WriteLine("Avaliable:" + sqlData.Reader[4].ToString());
            }

            sqlData.Connection.Close(); //Segun se ejecuta el codigo, cerrar la conexion para mayor seguridad.
        }
    }
}
