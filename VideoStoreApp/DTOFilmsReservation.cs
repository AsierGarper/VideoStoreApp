using System;
using System.Collections.Generic;
using System.Text;

namespace VideoStoreApp
{
    //To see the movies that a user has reserved, we are going to create this virtual object, that is to say, an object that takes values from two different tables of the database.
    //It is like a non-real table, where all the fields of the Reservations Table are contained, together with the 'Title' field of the Fimls Table.
    //This way, in MoviesMenu.CustomerReservations we are going to create a list of DTOFilmsReservations called filmsReservations (in plural), with objects 'filmReservation' where to store the values of each movie.
    //Creating in this way a list 'FilmsReservations' with objects 'FimlReservation' with the values of EACH movie.
    class DTOFilmsReservation
    {
        public string Title { get; set; }

        public int Reservation_id { get; set; }
        public DateTime RentDay { get; set; }
        public DateTime ReturnDay { get; set; }

        public DateTime MaxReturnDay { get; set; }

        public int Customer_id { get; set; }
        public int Film_id { get; set; }
    }
}
