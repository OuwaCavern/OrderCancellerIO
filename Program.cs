using System;
using System.Data.SqlClient;

namespace OrderCancellerIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection;
            string connectionString = "Data Source=DESKTOP-NEF9RON\\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True";
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                Console.WriteLine("Connection established.");
                string insertQuery = "INSERT INTO production.information(name,phone,email) VALUES( 'hasan', '+9054687265', 'hasan@gmail.com')";
                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Process is completed.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}