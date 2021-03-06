using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoStoreApp
{
    class DatabaseConnections
    {
        static SqlConnection connection = new SqlConnection("Data Source=MSI-Asier\\SQLEXPRESS; Initial Catalog=VIDEOSTORE; Integrated Security=True");

        public static DTOReaderAndConnection QueryExecute(string query)
        {
            DTOReaderAndConnection sqlData = new DTOReaderAndConnection();
            sqlData.Reader = null; //Initialize the reader to 'null'. 
            sqlData.Connection = connection;
            
            SqlCommand command = new SqlCommand(query, connection); //The object is created with the message to be sent and the connection to send it to.
            connection.Open(); //I open the connection
            if (query.ToUpper().Contains("SELECT")) //As in the SELECT the reader has to return something, we do that.
            {
                sqlData.Reader = command.ExecuteReader(); //Execute the command where I have the query and connection
            }
            else
            {
                command.ExecuteNonQuery(); //for UPDATE, INSERT and DELETE, the reader should not return anything.
            }
           
            return sqlData;

            //Remember to always close the connection after each reader, in its corresponding class.
        }

    }
}




//----------------------------------------------------------------------DELETE------------------------------------------
            //DateTime fecha = Convert.ToDateTime(Console.ReadLine());
            //Console.WriteLine(fecha);
            //SqlConnection connection = new SqlConnection("Data Source=MSI-Asier\\SQLEXPRESS; Initial Catalog=Northwind; Integrated Security=True");//se guarda en la variable connection la direccion a la que conectar

            //string queryDelete = "Delete from Customers where customerid = 'PEIOM'";

            //SqlCommand command = new SqlCommand(queryDelete, connection);

            //connection.Open();
            //command.ExecuteNonQuery();
            //connection.Close();

            //string querySelect = "Select * from Customers where customerid = 'PEIOM'";
            //SqlCommand commandSelect = new SqlCommand(querySelect, connection);

            //connection.Open();
            //SqlDataReader reader = commandSelect.ExecuteReader();

            //while (reader.Read())
            //{
            //    Console.WriteLine(reader[0].ToString());
            //    Console.WriteLine(reader[1].ToString());
            //}
            //connection.Close();

    //    }
    //}

    //----------------------------------------------------------------------UPDATE------------------------------------------

    //string queryUpdate = "UPDATE CUSTOMERS SET COMPANYNAME = 'ERLANTZ ROJO' WHERE CUSTOMERID = 'PEIOM'";

    //SqlCommand command = new SqlCommand(queryUpdate, connection);

    //connection.Open();
    //command.ExecuteNonQuery();
    //connection.Close();

    //string querySelect = "Select * from Customers where customerid = 'PEIOM'";
    //SqlCommand commandSelect = new SqlCommand(querySelect, connection);

    //connection.Open();
    //SqlDataReader reader = commandSelect.ExecuteReader();

    //while (reader.Read())
    //{
    //    Console.WriteLine(reader[0].ToString());
    //    Console.WriteLine(reader[1].ToString());
    //}
    //connection.Close();



    //----------------------------------------------------------------------SELECT------------------------------------------


    //string query = "SELECT * FROM CUSTOMERS "; //Este es el mensaje que se va a enviar
    //SqlCommand command = new SqlCommand(query, connection); //Se crea el objeto con el mensaje a enviar y la cone xion a la que enviarselo
    //connection.Open(); //Abro la conexion
    //SqlDataReader reader = command.ExecuteReader(); //Ejecuta el comando donde tengo la query y la conexion

    //while (reader.Read())
    //{
    //    //Console.WriteLine(reader["CustomerID"].ToString()); //
    //    Console.WriteLine(reader[0].ToString());//Esto nos devuelve la primera columna
    //    Console.WriteLine(reader[1].ToString());//Esto nos devuelve la segunda columna

    //}

    //connection.Close(); //Segun se ejecuta el codigo, cerrar la conexion para mayor seguridad.



    //----------------------------------------------------------------INSERT-----------------------------------------------------------

    //string queryInsert = "INSERT INTO CUSTOMERS (CUSTOMERID, COMPANYNAME) VALUES ('PEIOM', 'PEIO MURGUIA')";

    //SqlCommand commandInsert = new SqlCommand(queryInsert, connection);

    //connection.Open();
    //commandInsert.ExecuteNonQuery();
    //connection.Close();

    //Luego habria que escribir en la query de arriba, para comprobar que efectivamente se ha insertado la fila, lo siguiente:
    //string query = "SELECT * from CUSTOMERS WHERE CUSTOMERID = 'PEIOM'";
//}

