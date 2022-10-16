using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Configuration;

#region User Manual

/* User Manual
  1. Выход Exit
  
 */
#endregion

namespace WashintonStats
{
    class Program
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["WashingtonStats"].ConnectionString;

        private static SqlConnection sqlConnection = null;
        
        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            Console.WriteLine("WashingtonStats");

            SqlDataReader sqlDataReader = null;

            string command = string.Empty;

            while (true)
            {
                Console.Write("> ");
                command = Console.ReadLine();

                #region Exit
                if (command.ToLower().Equals("exit"))
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }
                    break;
                }
                #endregion

                SqlCommand sqlCommand = null;

                //SELECT * FROM [WashingtonStats] WHERE Id = 1

                string[] commandArray = command.ToLower().Split(' '); //?

                switch (commandArray[0])
                {
                    case "select":

                        sqlCommand = new SqlCommand(command, sqlConnection);

                        sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine($"{sqlDataReader["Id"]} {sqlDataReader["Player"]} {sqlDataReader["NO"]} {sqlDataReader["Age"]} {sqlDataReader["Country"]} {sqlDataReader["Points"]}");

                            Console.WriteLine(new String('-', 30));
                        }
                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }

                        break;
                    case "insert":

                        sqlCommand = new SqlCommand(command, sqlConnection);

                        Console.WriteLine($"Добавлено: {sqlCommand.ExecuteNonQuery()} строк(а)");

                        break;

                    case "update":

                        sqlCommand = new SqlCommand(command, sqlConnection);

                        Console.WriteLine($"Изменено: {sqlCommand.ExecuteNonQuery()} строк(а)");

                        break;

                    case "delete":

                        sqlCommand = new SqlCommand(command, sqlConnection);

                        Console.WriteLine($"Удал: {sqlCommand.ExecuteNonQuery()} строк(а)");

                        break;

                    case "sortby":

                        //sortby Player asc

                        sqlCommand = new SqlCommand($"SELECT * FROM [WashingtonDB] ORDER BY {commandArray[1]} {commandArray[2]}", sqlConnection);


                        break;
                        default:

                        Console.WriteLine($"Команда {command} некорректна!");
                        break;

                       
                }
            }
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }

}
