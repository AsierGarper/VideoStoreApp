using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace VideoStoreApp
{
    class MoviesMenu
    {
        //View available movies: Here we will have to display a list of movies taking into account the user's age, if he/she is under 18 years old, the movies recommended for that age will appear, if he/she is under 16, the movies recommended for children under 16, etc. All the movies in the database will appear, including those that are rented.
        //If the user selects one of them, all the data related to that movie will be shown.
        //Rent movie: here they can rent the movie they want as long as it is available and the recommended age is appropriate. Once rented, the movie should be switched to unavailable mode.
        //● My rentals: here you can see the movies you have rented and when the rental period expires.When the period has expired, the movie should appear in red.You can return the movie you have rented, which will be put back into available mode.
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
            string query = $"SELECT * FROM Film where RecommendedAge<={customer.CustomerAge()}"; //This is the message that will be sent, asking for movies whose recommended age is lower than the user's age.
            DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

            List<Film> films = new List<Film>(); //We create the list of movies, on which to save the movies available for that user.

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
                    Console.WriteLine("Not Available");//CONVENIENT TO PUT THE DATE OF RETURN, WHEN YOU RETURN IT
                    Console.ForegroundColor = ConsoleColor.White;
                }

                //First way: create the object directly when we do the add in the list
                //films.Add(new Film
                //{
                //    Film_id = Convert.ToInt32(sqlData.Reader["Film_id"]),

                //}) ;

                //To store the data, for EACH movie, we will store the information in an f object. 
                //Second way: first we create the object, and then
                Film fList = new Film();
                fList.Film_id = Convert.ToInt32(sqlData.Reader["Film_id"]);
                fList.Title = Convert.ToString(sqlData.Reader["Title"]);
                fList.Synopsis = Convert.ToString(sqlData.Reader["Synopsis"]);
                fList.RecommendedAge = Convert.ToInt32(sqlData.Reader["RecommendedAge"]);
                fList.Available = Convert.ToBoolean(sqlData.Reader["Available"]);

                //we insert it in the list with the Add
                films.Add(fList); //And now, in the films list we have stored all the fList objects of each movie.
                                  //Now, in the Film class, we have stored the values of each flist object, and we can access them, using the while reader.Read and write Flist.Film_id, or fList.Title for example.

            }

            sqlData.Connection.Close(); //Segun se ejecuta el codigo, cerrar la conexion para mayor seguridad.

            //------------------------EXTRA INFO ABOUT THE MOVIES------------------

            Console.WriteLine("\nSelect the id of your movie to advance:");
            string selectedId = Console.ReadLine();
            // Podemos utilizar LinkQ para hacer algo similar a SELECT * FROM FILMS WHERE Fiml_id = 1
            Film fselected = films.Where(x => x.Film_id == Convert.ToInt32(selectedId)).FirstOrDefault();
            Console.WriteLine("Title: " + fselected.Title);
            Console.WriteLine("Synopsis: " + fselected.Synopsis);
            Console.WriteLine("RecommendedAge: " + fselected.RecommendedAge);

            Console.WriteLine("Type 'back' to view available films.");


            //------------------------FUTURE UPDATE: RENT A MOVIE FROM EXTRA MOVIE INFO------------------------
            //Console.WriteLine("\nDo you want this rent a movie? Yes/No");
            //string rentingAnswer = Console.ReadLine();
            //if (rentingAnswer.ToLower() == "yes")
            //{
            //    RentMovie(customer);
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
            sqlData1.Connection.Close();

            //And now, we update the Table Film (DB) with the film selected to 'not available'
            string updateQuery = $"UPDATE Film set Available = 1 where Film_id = {answer}";
            DTOReaderAndConnection sqlData2 = DatabaseConnections.QueryExecute(updateQuery);
            sqlData2.Connection.Close();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"You have made a reservation on the day {RentDay.ToShortDateString()}. The film should be returned on {MaxReturnDate.ToShortDateString()}.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void CustomerReservations(Customer customer)
        {
            Console.WriteLine("Your reservations:\n");
            string query = $"select Reservation_id, RentDay, MaxReturnDate, title from Reservation r, Film f where f.Film_id = r.Film_id and Customer_id = {customer.Customer_id}";
            DTOReaderAndConnection sqlData = DatabaseConnections.QueryExecute(query);

            //To see the movies that a user has reserved, we are going to create this virtual object, that is to say, an object that takes values from two different tables of the database.
            //It is like a non-real table, where all the fields of the Reservations Table are contained, together with the 'Title' field of the Fimls Table.
            //This way, in MoviesMenu.CustomerReservations we are going to create a list of DTOFilmsReservations called filmsReservations (in plural), with objects 'filmReservation' where to store the values of each movie.
            List<DTOFilmsReservation> filmsReservations = new List<DTOFilmsReservation>();


            if (sqlData.Reader.HasRows)
            {
                while (sqlData.Reader.Read())
                {
                    //Creating in this way a list 'FilmsReservations' with objects 'fimlReservation' with the values of EACH movie.
                    DTOFilmsReservation filmsReservation = new DTOFilmsReservation();
                    filmsReservation.Title = sqlData.Reader["title"].ToString();
                    filmsReservation.Reservation_id = Convert.ToInt32(sqlData.Reader["Reservation_id"]);
                    filmsReservation.RentDay = Convert.ToDateTime(sqlData.Reader["RentDay"]);
                    filmsReservation.MaxReturnDay = Convert.ToDateTime(sqlData.Reader["MaxReturnDate"]);

                    //And we add, in each while, a movie (filmsReservation) with all the data in one position of the list (filmsReservations)
                    filmsReservations.Add(filmsReservation);
                }

                foreach (DTOFilmsReservation filmsReservation in filmsReservations)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Reservation Id: " + filmsReservation.Reservation_id);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Film title: " + filmsReservation.Title);
                    Console.WriteLine("Rental date: " + filmsReservation.RentDay);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Maximum return date: " + filmsReservation.MaxReturnDay);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                    
                Console.WriteLine("\nDo you want to return a movie? Yes/Back");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "yes")
                {
                    //AQUI HABRIA QUE METER UN TRY CATCH
                    Console.Write("Select the ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("reservation id");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" you want to return. Type 'back' to go back to main menu.");
                    //If the customer wants to return a movie, it will be necessary to do an update in the Film table and a delete in the Reservation table.
                    string selectedId = Console.ReadLine();
                    //PENDING I don't know how to check that the selected id entered is the one returned by the select. I have tried if (selectedId.Contains($"{sqlData.Reader["Reservation_id"]}")), but it fails to return anything.

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
                        DTOReaderAndConnection sqlData2 = DatabaseConnections.QueryExecute(queryUpdate);
                        sqlData2.Connection.Close();
                        string queryDelete = $"DELETE FROM Reservation WHERE Reservation_id = {selectedId}";
                        DTOReaderAndConnection sqlData3 = DatabaseConnections.QueryExecute(queryDelete);
                        sqlData2.Connection.Close();
                        Console.WriteLine($"Your return with reservation id {selectedId} has been successfully accepted. You will be redirected to Main menu.");
                        ShowMenu(customer);
                    }

                }
                else if (answer.ToLower() == "no" || answer.ToLower() == "back")
                {
                    sqlData.Connection.Close();
                    Console.WriteLine("Okay, you have been redirected to Main Menu.");
                    ShowMenu(customer);
                }
                else
                {
                    sqlData.Connection.Close();
                    Console.WriteLine("I don't understand what you mean. You have been redirected to Main Menu.");
                    ShowMenu(customer);
                }
            }
            else
            {
                sqlData.Connection.Close();
                Console.WriteLine("There are no reserves. Try to do a reservation in '2- Rent a movie' section.");
                ShowMenu(customer);
            }
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
