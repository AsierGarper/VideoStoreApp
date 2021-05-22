using System;
using System.Collections.Generic;
using System.Text;

namespace VideoStoreApp
{
    public class Customer
    {
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int Customer_id { get; set; }

        //These variables, I have initialized them in LoginMenu.Login, this way, when being static, they store the values of Customer registered in the application, and this way to know which movies to show based on their age, for example.
        //For example, by typing Customer.Dni in any class of the program, I can access its stored value.

        public Customer (int customer_id, string dni, string name, string lastName, DateTime birthday, string mail, string password)
        {
            Customer_id = customer_id;
            Dni = dni;
            Name = name;
            LastName = lastName;
            Birthday = birthday;
            Mail = mail;
            Password = password;
        }
        public int CustomerAge()
        {
            ////Here, I have to calculate the age of the user who is using the application. 
            ////And based on that age, show him the movies whose recommended age does not exceed the age of the client.
            int customerAge = Convert.ToInt32(Math.Floor((DateTime.Now - Birthday).Days/365.25));
            //We collect the accumulated Birthday value in Customer.Birthday, and calculate the age with respect to today's date. We use Math.Floor to round down, as the user may not have reached the age of the current year.

            return customerAge;
        }


    }
}
