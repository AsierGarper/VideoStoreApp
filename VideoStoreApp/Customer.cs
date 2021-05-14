using System;
using System.Collections.Generic;
using System.Text;

namespace VideoStoreApp
{
    public static class Customer
    {
        public static string Dni { get; set; }
        public static string Name { get; set; }
        public static string LastName { get; set; }
        public static DateTime Birthday { get; set; }
        public static string Mail { get; set; }
        public static string Password { get; set; }

        //These variables, I have initialized them in LoginMenu.Login, this way, when being static, they store the values of Customer registered in the application, and this way to know which movies to show based on their age, for example.
        //For example, by typing Customer.Dni in any class of the program, I can access its stored value.
    }
}
