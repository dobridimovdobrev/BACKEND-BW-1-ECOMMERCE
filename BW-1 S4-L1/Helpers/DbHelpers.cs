
using BW_1_S4_L1.Helpers.Enums;

using BW_1_S4_L1.Models;

using Microsoft.Data.SqlClient;

namespace BW_1_S4_L1.Helpers
{
    public static class DbHelpers
    {

        private const string _masterConnectionString = "Server= LAPTOP-4087GM5S\\SQLEXPRESS ; User " +
           "Id= sa; Password=sa  Database = master; TrustServerCertificate = true; Trusted_Connection = true ";

        private const string _ShopConnectionString = "Server= LAPTOP-4087GM5S\\SQLEXPRESS ; User " +
            "Id= sa; Password=sa ; Database = Shop; TrustServerCertificate = true; Trusted_Connection = true ";


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
                if (ex.Number == (int)ECodiceErroreDB.DatabaseEsistente)
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


        //CREAZIONE TABELLE
        private static void CreateTable()
        {
            using var connection = new SqlConnection(_ShopConnectionString);
            connection.Open();


            var commandText = """
                CREATE TABLE Product (
                IdProduct INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
                Image NVARCHAR(2000) NOT NULL,
                Name NVARCHAR (50) NOT NULL,
                Date DATETIME NOT NULL,
                Price DECIMAL (10,2) NOT NULL,
                Description NVARCHAR(500) NOT NULL,
                Quantity INT NOT NULL
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
                if (ex.Number == (int)ECodiceErroreDB.TabellaEsistente)
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

        public static List<Product> GetProducts()
        {
            var products = new List<Product>();
            using var connection = new SqlConnection(_ShopConnectionString);
            connection.Open();
            var commandText = """
                SELECT * FROM Product 
                """;
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var product = new Product
                {
                    IdProduct = reader.GetInt32(0),
                    Image = reader.GetString(1),
                    Name = reader.GetString(2),
                    Date = reader.GetDateTime(3),
                    Price = reader.GetDecimal(4),
                    Description = reader.GetString(5),
                    Quantity = reader.GetInt32(6)
                };
                products.Add(product);
            }
            return products;
        }
        public static Product getProductById(int productId)
        {
            using var connection = new SqlConnection(_ShopConnectionString);
            connection.Open();
            var commandText = """
                SELECT * FROM Product WHERE IdProduct = @IdProduct
                """;
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddWithValue("@IdProduct", productId);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var product = new Product
                {
                    IdProduct = reader.GetInt32(0),
                    Image = reader.GetString(1),
                    Name = reader.GetString(2),
                    Date = reader.GetDateTime(3),
                    Price = reader.GetDecimal(4),
                    Description = reader.GetString(5),
                    Quantity = reader.GetInt32(6)
                };
                return product;
            }
            return null;
        }
        public static void SaveProduct(Product product)
        {
            using var connection = new SqlConnection(_ShopConnectionString);
            connection.Open();
            var commandText = """
                INSERT INTO Product (Image, Name, Date, Price, Description, Quantity)
                VALUES (@Image, @Name, @Date, @Price, @Description, @Quantity)
                """;
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddWithValue("@Image", product.Image);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.ExecuteNonQuery();
        }

        public static void ModifyProduct(Product product)
        {
            using var connection = new SqlConnection(_ShopConnectionString);
            connection.Open();
            var commandText = """
                UPDATE Product SET Image = @Image, Name = @Name, Date = @Date, Price=@Price, Description = @Description, Quantity = @Quantity
                WHERE IdProduct = @IdProduct
                """;
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddWithValue("@IdProduct", product.IdProduct);
            command.Parameters.AddWithValue("@Image", product.Image);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Date", product.Date);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.ExecuteNonQuery();

        }

        public static void DeleteProduct(int id)
        {
            using var connection = new SqlConnection(_ShopConnectionString);
            connection.Open();
            var commandText = """
                DELETE FROM Product WHERE IdProduct = @IdProduct
                """;
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddWithValue("@IdProduct", id);
            command.ExecuteNonQuery();
        }
    }
}
