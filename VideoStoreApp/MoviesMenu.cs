using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        public static void ShowMenu(Customer customerData)
        {

            int opcion = 1;
            while (opcion != 0)
            {
                Console.WriteLine("\nSelect the option you want to perform:");
                Console.WriteLine("\n 1-Available Films Catalog \n 2-Rent a movie \n 3-My reservations \n 4-Logout\n");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //We create the AvailableFilms method, requesting as data, the age (in int) of the client collected in the constructor created in LoginMenu.LogIn.
                        AvailableFilms(customerData);
                        break;
                    case 2:
                        RentMovie(customerData);
                        break;
                    case 3:
                        CustomerReservations(customerData);
                        //Tiene que tener la opcion de DEVOLVER PELICULA, 
                        break;
                    case 4:
                        Logout(customerData);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void AvailableFilms(Customer customer) //We call the AvailableFilms method passing it the object Customer, with all the data of himself saved when we generate the object in LoginMenu.LogIn.
        {
            Console.WriteLine($"Since you are {customer.CustomerAge()} years old, the movies recommended for you are the following:");
            string query = $"SELECT * FROM Film where RecommendedAge<={customer.CustomerAge()}"; //Este es el mensaje que se va a enviar, pidiendo las peliculas cuya edad recomendada es inferior a la edad del usuario.
            DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

            List<Film> films = new List<Film>(); //Creamos la lista de peliculas, sobre la que guardar las peliculas disponibles para dicho usuario.

            while (sqlData.Reader.Read())
            {
                Console.WriteLine("\nId:" + sqlData.Reader["Film_id"].ToString());
                Console.WriteLine("\nTitle:" + sqlData.Reader["Title"].ToString());

                //In the case of availability, being a boolean type, it returns False/True, so I'm going to change it to Available/Unavailable.
                if (sqlData.Reader["Available"].ToString() == "False")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Available for reservation");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not Available");//CONVENIENTE PONER LA FECHA DE RETORNO, CUANDO LA DEVUELVAN
                    Console.ForegroundColor = ConsoleColor.White;
                }

                //Primera forma: crear el objeto directamente cuando hacemos el add en la lista
                //films.Add(new Film
                //{
                //    Film_id = Convert.ToInt32(sqlData.Reader["Film_id"]),

                //}) ;

                //Para almacenar los datos, de CADA pelicula, almacenaremos la informacion en un objeto f 
                //Segunda forma: primero creamos el objeto, y luego
                Film fList = new Film();
                fList.Film_id = Convert.ToInt32(sqlData.Reader["Film_id"]);
                fList.Title = Convert.ToString(sqlData.Reader["Title"]);
                fList.Synopsis = Convert.ToString(sqlData.Reader["Synopsis"]);
                fList.RecommendedAge = Convert.ToInt32(sqlData.Reader["RecommendedAge"]);
                fList.Available = Convert.ToBoolean(sqlData.Reader["Available"]);

                //lo insertamos en la lista con el Add
                films.Add(fList); //Y ahora, en la lista films tenemos almacenados todos los objetos fList de cada pelicula.
                                  //Ahora, en la clase Film, tenemos almacenados los valores de cada objeto flist, y podemos acceder a ellos, usando el while reader.Read y escribir Flist.Film_id, o fList.Title por ejemplo

            }

            sqlData.Connection.Close(); //Segun se ejecuta el codigo, cerrar la conexion para mayor seguridad.

            //------------------------INFO EXTRA DE LAS PELICULAS------------------

            Console.WriteLine("\nSelect the id of your movie to advance:");
            string selectedId = Console.ReadLine();
            // Podemos utilizar LinkQ para hacer algo similar a SELECT * FROM FILMS WHERE Fiml_id = 1
            Film fselected = films.Where(x => x.Film_id == Convert.ToInt32(selectedId)).FirstOrDefault();
            Console.WriteLine("Title: " + fselected.Title);
            Console.WriteLine("Synopsis: " + fselected.Synopsis);
            Console.WriteLine("RecommendedAge: " + fselected.RecommendedAge);

            Console.WriteLine("Type 'back' to view available films.");


            //------------------------AlQUILAR PELICULA DESDE INFO EXTRA DE LA PELICULA------------------------
            //Console.WriteLine("\nDo you want this rent a movie? Yes/No");
            //string rentingAnswer = Console.ReadLine();
            //if (rentingAnswer.ToLower() == "yes")
            //{
            //    RentMovie(customerData);
            //}
            //else if (rentingAnswer.ToLower() == "no")
            //{
            //    Console.WriteLine("You have been redirected to the main menu.\n");
            //    LoginMenu.MainMenu();
            //}
            //else
            //{
            //    Console.WriteLine("I don't understand what you mean. You have been redirected to the main menu.\n");
            //    LoginMenu.MainMenu();
            //}

        }

        public static void RentMovie(Customer customer)
        {
            Console.WriteLine($"According to your age of {customer.CustomerAge()}, the Available and recommended movies for you are the following:");
            //Here we do the SELECT to show the available movies based on the user's age.
            string query = $"SELECT * FROM Film where Available = 0 and RecommendedAge<= {customer.CustomerAge()}";
            DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);
            while (sqlData.Reader.Read())
            {
                Console.WriteLine("\nId:" + sqlData.Reader["Film_id"].ToString());
                Console.WriteLine("\nTitle:" + sqlData.Reader["Title"].ToString());

            }
            sqlData.Connection.Close();
            Console.WriteLine("\nSelect one id for your reservation (for 2 days)");
            string answer = Console.ReadLine();
            Console.WriteLine("\nSelect date to reserve this movie:");
            DateTime RentDay = Convert.ToDateTime(Console.ReadLine());
            DateTime MaxReturnDate = RentDay.AddDays(2);

            //Here, we make the reservation and insert the reservation data in the Reservation Table (DB).
            string insertQuery = $" INSERT INTO Reservation (RentDay, MaxReturnDate, Customer_id, Film_id) values ('{RentDay}', '{MaxReturnDate}', '{customer.Customer_id}', {answer})";
            DTOReaderAndConnection sqlData1 = DatabaseConnections.QueryExecute(insertQuery);
            sqlData.Connection.Close();

            //And now, we update the Table Film (DB) with the film selected to 'not available'
            string updateQuery = $"UPDATE Film set Available = 1 where Film_id = {answer}";
            DTOReaderAndConnection sqlData2 = DatabaseConnections.QueryExecute(updateQuery);
            sqlData.Connection.Close();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"You have made a reservation on the day {RentDay.ToShortDateString()}. The film should be returned on {MaxReturnDate.ToShortDateString()}.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void CustomerReservations(Customer customer)
        {
            Console.WriteLine("Your reservations:\n");
            string query = $"select Reservation_id, RentDay, MaxReturnDate, title from Reservation r, Film f where f.Film_id = r.Film_id and Customer_id = {customer.Customer_id}";
            DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);
            while (sqlData.Reader.Read())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Reservation Id: " + sqlData.Reader["Reservation_id"]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Film title: " + sqlData.Reader["title"]);
                Console.WriteLine("Rental date: " + sqlData.Reader["RentDay"]);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Maximum return date:  {(sqlData.Reader["MaxReturnDate"])}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nDo you want to return a movie? Yes/Back");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "yes")
                {
                    sqlData.Connection.Close();
                    //AQUI HABRIA QUE METER UN TRY CATCH
                    Console.WriteLine("Select the reservation id you want to return. Type 'back' to go back to main menu.");
                    //If the customer wants to return a movie, it will be necessary to do an update in the Film table and a delete in the Reservation table.
                    string selectedId = Console.ReadLine();
                    if (selectedId.ToLower() == "back")
                    {
                        sqlData.Connection.Close();
                        Console.WriteLine("Okay, you have been redirected to Main Menu.");
                        ShowMenu(customer);
                    }
                    else
                    {
                        sqlData.Connection.Close();
                        string queryUpdate = $"UPDATE Film set Available = 0 where Film_id = {selectedId}";
                        //ME HE QUEDADO AQUI
                        //QUERY DELETE
                    }

                }
                else if (answer.ToLower() == "no" || answer.ToLower() == "back")
                {
                    Console.WriteLine("Okay, you have been redirected to Main Menu.");
                    ShowMenu(customer);
                }
                else
                {
                    Console.WriteLine("I don't understand what you mean. You have been redirected to Main Menu.");
                    ShowMenu(customer);
                }

            }
            sqlData.Connection.Close();
            Console.WriteLine("There are no reserves. Try to do a reservation in '2- Rent a movie' section.");
            ShowMenu(customer);
        }



        public static void Logout(Customer customer)
        {
            Console.WriteLine("Are you sure to logout? Yes/No");
            string logoutAnswer = Console.ReadLine();

            if (logoutAnswer == "yes")
            {
                Console.WriteLine("You have logout succesfully\n.");
                LoginMenu.MainMenu();
            }
            else if (logoutAnswer == "no")
            {
                Console.WriteLine("You have been redirected to the main menu.\n");
                ShowMenu(customer);
            }
            else
            {
                Console.WriteLine("I don't understand what you mean. You have been redirected to the main menu.\n");
                ShowMenu(customer);
            }
        }
    }
}
