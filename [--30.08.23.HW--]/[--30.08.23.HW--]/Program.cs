using System.Data;
using System.Data.SqlClient;

namespace ___30._08._23.HW___
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=Admin-PC\\SQLEXPRESS;Initial Catalog=FruitsAndVeggies;Integrated Security=true;Connection Timeout=30;";

            try
            {
                using(SqlConnection conn  = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Пiдключення успiшне");


                    Console.WriteLine("Ви хочете створити таблицю? [Y/N]");
                    string isTableCreated = Console.ReadLine();
                    if (isTableCreated.ToLower() == "y")
                    {
                        string queryCmd = "create table FruitsAndVeggies(ID int identity(1, 1) primary key,Name nvarchar(100) not null check (Name <>''),Type bit not null,Color nvarchar(100) not null check (Color <>''),Calories int not null check (Calories >= 0))";

                        using (SqlCommand cmd = new SqlCommand(queryCmd, conn))
                        {
                            Console.WriteLine(cmd.ExecuteNonQuery());
                        }
                    }

                    Console.WriteLine("Чи потрiбно заповнити таблицю тестовими данними? [Y/N]");
                    string isDataNotCreated = Console.ReadLine();
                    if(isDataNotCreated.ToLower() == "y")
                    {
                        string queryCmd = "insert into FruitsAndVeggies values ('Cucumber', 0, 'Green', 18),('Apple', 1, 'Red', 95),('Carrot', 0, 'Orange', 41),('Banana', 1, 'Yellow', 105),('Grapes', 1, 'Purple', 69),('Spinach', 0, 'Green', 23),('Orange', 1, 'Orange', 62);";

                        using(SqlCommand cmd = new SqlCommand(queryCmd, conn))
                        {
                            Console.WriteLine(cmd.ExecuteNonQuery());
                        }
                    }

                    string query = "select * from FruitsAndVeggies";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();

                    int choice = -1;
                    while(choice != 0)
                    {
                        Console.WriteLine("\nВведiть свiй вибiр");
                        Console.WriteLine("1 - Показати всю iнформацiю");
                        Console.WriteLine("2 - Показати всi назви овочiв та фруктiв");
                        Console.WriteLine("3 - Показати всi кольори");
                        Console.WriteLine("4 - Показати максимальну калорiйнiсть");
                        Console.WriteLine("5 - Показати мiнiмальну калорiйнiсть");
                        Console.WriteLine("6 - Показати середню калорiйнiсть");
                        Console.WriteLine();
                        Console.WriteLine("7 - Показати кiлькiсть овочiв");
                        Console.WriteLine("8 - Показати кiлькiсть фруктiв");
                        Console.WriteLine("9 - Показати кiлькiсть овочiв i фруктiв заданого кольору");
                        Console.WriteLine("10 - Показати кiлькiсть овочiв i фруктiв кожного кольору");
                        Console.WriteLine("11 - Показати овочi та фрукти з калорiйнiстю нижче вказаної");
                        Console.WriteLine("12 - Показати овочi та фрукти з калорiйнiстю вище вказаної");
                        Console.WriteLine("13 - Показати овочi та фрукти з калорiйнiстю у вказаному дiапазонi");
                        Console.WriteLine("14 - Показати усi овочi та фрукти жовтого або червоного кольору");
                        Console.WriteLine();
                        Console.WriteLine("0 - Вихiд");

                        choice = int.Parse(Console.ReadLine());
                        switch(choice)
                        {
                            case 1: Console.Clear();
                                GetData(query, adapter, ds, conn);

                                Console.WriteLine($"{"Name".PadRight(12)}{"Type".PadRight(12)}{"Color".PadRight(12)}{"Calories".PadRight(10)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine($"{row["Name"].ToString().PadRight(12)}{row["Type"].ToString().PadRight(12)}{row["Color"].ToString().PadRight(12)}{row["Calories"].ToString().PadRight(10)}");
                                }
                                break;
                            case 2:
                                Console.Clear();
                                GetData(query, adapter, ds, conn);

                                Console.WriteLine($"{"Name".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Name"].ToString().PadRight(12));
                                }
                                break;
                            case 3:
                                Console.Clear();
                                GetData($"select Color from FruitsAndVeggies group by Color", adapter, ds, conn);

                                Console.WriteLine($"{"Color".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Color"].ToString().PadRight(12));
                                }
                                break;
                            case 4:
                                Console.Clear();
                                GetData($"select max(Calories) as 'Max calories' from FruitsAndVeggies", adapter, ds, conn);

                                Console.WriteLine($"{"Max calories".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Max calories"].ToString().PadRight(12));
                                }
                                break;
                            case 5:
                                Console.Clear();
                                GetData($"select min(Calories) as 'Min calories' from FruitsAndVeggies", adapter, ds, conn);

                                Console.WriteLine($"{"Min calories".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Min calories"].ToString().PadRight(12));
                                }
                                break;
                            case 6:
                                Console.Clear();
                                GetData($"select avg(Calories) as 'Average calories' from FruitsAndVeggies", adapter, ds, conn);

                                Console.WriteLine($"{"Average calories".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Average calories"].ToString().PadRight(12));
                                }
                                break;
                            case 7:
                                Console.Clear();
                                GetData($"select count(*) as 'Number of veggies' from FruitsAndVeggies where Type=0", adapter, ds, conn);

                                Console.WriteLine($"{"Number of veggies".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Number of veggies"].ToString().PadRight(12));
                                }
                                break;
                            case 8:
                                Console.Clear();
                                GetData($"select count(*) as 'Number of fruits' from FruitsAndVeggies where Type=1", adapter, ds, conn);

                                Console.WriteLine($"{"Number of fruits".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Number of fruits"].ToString().PadRight(12));
                                }
                                break;
                            case 9:
                                Console.Clear();

                                Console.WriteLine("Введiть бажаний колiр");
                                string color = Console.ReadLine();

                                GetData($"select count(*) as 'Number' from FruitsAndVeggies where Color='{color}'", adapter, ds, conn);

                                Console.WriteLine($"Number of {color.ToLower()}-colored fruits and vegetables\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine(row["Number"].ToString().PadRight(12));
                                }
                                break;
                            case 10:
                                Console.Clear();

                                GetData($"select count(*) as 'Number', Color from FruitsAndVeggies group by Color", adapter, ds, conn);

                                Console.WriteLine($"{"Color".PadRight(12)}{"Number".PadRight(12)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine($"{row["Color"].ToString().PadRight(12)}{row["Number"].ToString().PadRight(12)}");
                                }
                                break;
                            case 11:
                                Console.Clear();

                                Console.WriteLine("Введiть максимально допустиму калорiйнiсть");
                                int maxCalories = int.Parse(Console.ReadLine());

                                GetData($"{query} where Calories < {maxCalories}", adapter, ds, conn);

                                Console.WriteLine($"{"Name".PadRight(12)}{"Type".PadRight(12)}{"Color".PadRight(12)}{"Calories".PadRight(10)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine($"{row["Name"].ToString().PadRight(12)}{row["Type"].ToString().PadRight(12)}{row["Color"].ToString().PadRight(12)}{row["Calories"].ToString().PadRight(10)}");
                                }
                                break;
                            case 12:
                                Console.Clear();

                                Console.WriteLine("Введiть мiнiмально допустиму калорiйнiсть");
                                int minCalories = int.Parse(Console.ReadLine());

                                GetData($"{query} where Calories > {minCalories}", adapter, ds, conn);

                                Console.WriteLine($"{"Name".PadRight(12)}{"Type".PadRight(12)}{"Color".PadRight(12)}{"Calories".PadRight(10)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine($"{row["Name"].ToString().PadRight(12)}{row["Type"].ToString().PadRight(12)}{row["Color"].ToString().PadRight(12)}{row["Calories"].ToString().PadRight(10)}");
                                }
                                break;
                            case 13:
                                Console.Clear();

                                Console.WriteLine("Введiть мiнiмальну границю калорiйностi");
                                int start = int.Parse(Console.ReadLine());

                                Console.WriteLine("Введiть максимальну границю калорiйностi");
                                int end = int.Parse(Console.ReadLine());

                                GetData($"{query} where Calories between {start} and {end}", adapter, ds, conn);

                                Console.WriteLine($"{"Name".PadRight(12)}{"Type".PadRight(12)}{"Color".PadRight(12)}{"Calories".PadRight(10)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine($"{row["Name"].ToString().PadRight(12)}{row["Type"].ToString().PadRight(12)}{row["Color"].ToString().PadRight(12)}{row["Calories"].ToString().PadRight(10)}");
                                }
                                break;
                            case 14:
                                Console.Clear();

                                GetData($"{query} where Color = 'Red' or Color = 'Yellow'", adapter, ds, conn);

                                Console.WriteLine($"{"Name".PadRight(12)}{"Type".PadRight(12)}{"Color".PadRight(12)}{"Calories".PadRight(10)}\n");

                                foreach (DataRow row in ds.Tables["FruitsAndVeggies"].Rows)
                                {
                                    Console.WriteLine($"{row["Name"].ToString().PadRight(12)}{row["Type"].ToString().PadRight(12)}{row["Color"].ToString().PadRight(12)}{row["Calories"].ToString().PadRight(10)}");
                                }
                                break;
                            case 0:
                                break;
                            default:
                                Console.WriteLine("Помилковий вибiр!");
                                break;
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void GetData(string query, SqlDataAdapter adapter, DataSet ds, SqlConnection conn)
        {
            adapter = new SqlDataAdapter(query, conn);
            ds.Clear();

            adapter.Fill(ds, "FruitsAndVeggies");
        }
    }
}