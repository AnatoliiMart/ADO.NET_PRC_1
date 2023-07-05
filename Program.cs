using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_PRC_1
{
    internal class Program
    {
        static SqlConnection connection;
        static void Main()
        {
            connection = new SqlConnection
            {
                ConnectionString =
                @"Data Source=DESKTOP-VR0F9ME\SQLEXPRESS;
                Initial Catalog=StudentsMarks;
                Integrated Security=true;"
            };

            try
            {
                //открываем соединение
                connection.Open();
                Console.WriteLine("Connection openned");

                Console.WriteLine("\n*************ЗАДАНИЕ 3*************");
                Console.WriteLine("\n----------------3.1----------------");
                ShowAllStudentsAndMarks();
                Console.WriteLine("\n----------------3.2----------------");
                ShowStudentsFIO();
                Console.WriteLine("\n----------------3.3----------------");
                ShowAverageMarks();
                Console.WriteLine("\n----------------3.4----------------");
                ShowAverageMarksMoreThanInParams(6);
                Console.WriteLine("\n----------------3.5----------------");
                ShowUniqueMinMarkedSubj();

                Console.WriteLine("\n*************ЗАДАНИЕ 4*************");
                Console.WriteLine("\n----------------4.1----------------");
                ShowMinAverageMark();
                Console.WriteLine("\n----------------4.2----------------");
                ShowMaxAverageMark();
                Console.WriteLine("\n----------------4.3----------------");
                ShowCountStudentsOnMinMarkByMath();
                Console.WriteLine("\n----------------4.4----------------");
                ShowCountStudentsOnMaxMarkByMath();
                //Задание 4.5 не сделал так как у нас нет групп в таблице 
                Console.WriteLine("\n----------------4.6----------------");
                ShowAverageMarkOnGroup();
                Console.WriteLine("\n-----------------------------------");
            }
            finally
            {
                //закрываем соединение
                connection.Close();
                Console.WriteLine("Connection closed");
            }
        }

        static void ShowAllStudentsAndMarks()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * 
                                FROM Marks";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}\t{reader[2]}\t{reader[3]}\t{reader[4]}");

            reader.Close();
        }

        static void ShowStudentsFIO()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT ID, StudentFIO 
                                FROM Marks";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}");

            reader.Close();
        }

        static void ShowAverageMarks()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT ID, AverageMark 
                                FROM Marks";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}");

            reader.Close();
        }

        static void ShowAverageMarksMoreThanInParams(float mark)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT ID, AverageMark " +
                              $"FROM Marks " +
                              $"WHERE AverageMark > {mark} ";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}");

            reader.Close();
        }

        static void ShowUniqueMinMarkedSubj()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT ID, NameMinMarkedSubj
                                FROM Marks ";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}");

            reader.Close();
        }

        static void ShowMinAverageMark()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT MIN(AverageMark) 
                                FROM Marks";
            Console.WriteLine("Minimal average mark: " + cmd.ExecuteScalar().ToString());
        }

        static void ShowMaxAverageMark()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT MAX(AverageMark) 
                                FROM Marks";
            Console.WriteLine("Maximal average mark: " + cmd.ExecuteScalar().ToString());
        }

        static void ShowCountStudentsOnMinMarkByMath()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(*) 
                                FROM Marks
                                WHERE NameMinMarkedSubj = 'Math'";
            Console.WriteLine("Min mark by math: " + cmd.ExecuteScalar().ToString());
        }

        static void ShowCountStudentsOnMaxMarkByMath()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(*) 
                                FROM Marks
                                WHERE NameMaxMarkedSubj = 'Math'";
            Console.WriteLine("Max mark by math: " + cmd.ExecuteScalar().ToString());
        }

        static void ShowAverageMarkOnGroup()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT SUM(AverageMark)/COUNT(*)
                                FROM Marks";
            Console.WriteLine("Average mark on group: " + Math.Round((double)cmd.ExecuteScalar(), 3));
        }
    }
}
