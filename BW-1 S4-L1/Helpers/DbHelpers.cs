using Microsoft.Data.SqlClient;

namespace BW_1_S4_L1.Helpers
{
    public static class DbHelpers
    {
        private const string _masterConnectionString = "Server= PRESTITO\\SQLEXPRESS ; User " +
           "Id= sa; Password= sa1234; Database = master; TrustServerCertificate = true; Trusted_Connection = true ";

        private const string _ShopConnectionString = "Server= PRESTITO\\SQLEXPRESS ; User " +
            "Id= sa; Password= sa1234; Database = Shop; TrustServerCertificate = true; Trusted_Connection = true ";
        public static void InitializeDatabase()
        {
            CreateDatabase();
            CreateTable();
        }

        private static void CreateDatabase()
        {
            using var connection = new SqlConnection(_masterConnectionString);
            connection.Open();

            var commandText = """
                CREATE DATABASE Shop;
                """;

            var command = connection.CreateCommand();
            command.CommandText = commandText;


            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1801)
                {
                    Console.WriteLine("Database already exists.");
                }
                else
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);

            }
        }

        private static void CreateTable()
        {
            using var connection = new SqlConnection(_ShopConnectionString);
            connection.Open();


            var commandText = """
                CREATE TABLE Product (
                IdProduct INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
                Img NVARCHAR(2000) NOT NULL,
                Name NVARCHAR (50) NOT NULL,
                Date DATE NOT NULL,
                Price DECIMAL (10,2) NOT NULL,
                Description NVARCHAR(500) NOT NULL
                );

                CREATE TABLE Category(
                IdCategory INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
                Name NVARCHAR (50) NOT NULL
                );

                CREATE TABLE Product_Category(              
                IdProductFK INT NOT NULL,
                IdCategoryFK INT NOT NULL,
                PRIMARY KEY (IdProductFK, IdCategoryFK),
                FOREIGN KEY (IdProductFK) REFERENCES Product(IdProduct),
                FOREIGN KEY (IdCategoryFK) REFERENCES Category(IdCategory)
                )

                """;



            var command = connection.CreateCommand();

            command.CommandText = commandText;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2714)
                {
                    Console.WriteLine("Table already exsisting");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }



    }
}
