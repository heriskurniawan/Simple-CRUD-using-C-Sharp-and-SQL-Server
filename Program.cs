using System;
using System.Data.SqlClient;
namespace ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            helper.helper field = new helper.helper();
            int id = field.id;
            string _id = id.ToString();
            string first_name = field.first_name;
            string last_name = field.last_name;
            string operation, backtop;
            SqlCommand commandSelect, commandInsert, commandEdit, commandDelete;
            String sqlSelect, sqlInsert, sqlEdit, sqlDelete = "";
            SqlDataReader dataReader;
            string conn = "Data Source=USER-PC;Initial Catalog=demo;User ID=sa;Password=P@ssw0rd;";

            try
            {
            transaksi:
                //Get All data
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    sqlSelect = "SELECT id,fisrt_name,last_name FROM dbo.siswa";
                    commandSelect = new SqlCommand(sqlSelect, con);
                    dataReader = commandSelect.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}", dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2));
                    }
                }
                //CRUD operation
                using (SqlConnection con = new SqlConnection(conn))
                {

                    Console.WriteLine("Enter your operation Press(1) for INSERT, (2) EDIT, (3) DELETE: ");
                    operation = Console.ReadLine().ToString();
                    //If press "1" insert opration running...
                    if (operation == "1")
                    {
                        Console.WriteLine("Enter your first name: ");
                        first_name = Console.ReadLine();
                        Console.WriteLine("Enter your last name: ");
                        last_name = Console.ReadLine();

                        con.Open();

                        sqlInsert = "INSERT INTO dbo.siswa VALUES (@first_name, @last_name)";
                        commandInsert = new SqlCommand(sqlInsert, con);

                        commandInsert.Parameters.AddWithValue("@first_name", first_name);
                        commandInsert.Parameters.AddWithValue("@last_name", last_name);
                        commandInsert.ExecuteNonQuery();

                    }
                    //If press "2" Update opration running...
                    else if (operation == "2")
                    {
                        Console.WriteLine("Enter ID: ");
                        _id = Console.ReadLine().ToString();
                        Console.WriteLine("Enter your first name: ");
                        first_name = Console.ReadLine();
                        Console.WriteLine("Enter your last name: ");
                        last_name = Console.ReadLine();

                        con.Open();

                        sqlEdit = "UPDATE dbo.siswa SET fisrt_name=@first_name,last_name=@last_name WHERE id=@id ";
                        commandEdit = new SqlCommand(sqlEdit, con);
                        commandEdit.Parameters.AddWithValue("@id", _id);
                        commandEdit.Parameters.AddWithValue("@first_name", first_name);
                        commandEdit.Parameters.AddWithValue("@last_name", last_name);
                        commandEdit.ExecuteNonQuery();

                    }
                    //If press "3" Delete opration running...
                    else if (operation == "3")
                    {
                        Console.WriteLine("Enter ID for deleted :");
                        _id = Console.ReadLine().ToString();
                        con.Open();
                        sqlDelete = "DELETE FROM dbo.siswa WHERE id=" + _id;
                        commandDelete = new SqlCommand(sqlDelete, con);
                        commandDelete.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("No operation for running...");
                        Console.WriteLine("Are you role back operation? Press (y) or (n)");
                        backtop = Convert.ToString(Console.ReadLine());
                        //Back to TOP
                        if (backtop == "y" || backtop == "Y")
                        {
                            goto transaksi;

                        }
                    }
                    Console.WriteLine("Are you role back operation? Press (y) or (n)");
                    backtop = Convert.ToString(Console.ReadLine());

                    if (backtop == "y" || backtop == "Y")
                    {
                        goto transaksi;

                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}