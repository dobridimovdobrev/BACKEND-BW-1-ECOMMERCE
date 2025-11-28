using Microsoft.Data.SqlClient;
using BW_1_S4_L1.Models;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using BW_1_S4_L1.Helpers.Enums;

namespace BW_1_S4_L1.Helpers
{
    public static class DbHelper
    {
        private const string _masterConnectionString = "Server=DESKTOP-O7GHHQB\\SQLEXPRESS;User=sa;Password=Svqtiqduh1!;Database=master;Trusted_Connection=True;TrustServerCertificate=True";
        private const string _shopConnectionString = "Server=DESKTOP-O7GHHQB\\SQLEXPRESS;User=sa;Password=Svqtiqduh1!;Database=Shop;Trusted_Connection=True;TrustServerCertificate=True";

        //inizializzare database
        public static void InitializeDatabase()
        {
            CreateDb();
            CreateProductTable();
            CreateCategoryTable();
            CreateProductCategoryTable();
            CreateCarrelloTable();
            InsertCategories();
            Console.WriteLine("Database inizializzato con successo!");
        }


        // creare database Shop
        public static void CreateDb()
        {
            using var connection = new SqlConnection(_masterConnectionString);
            connection.Open();
            var commandSql = new SqlCommand("create database Shop", connection);

            //eseguire il commando e gestire gli errori
            try
            {
                commandSql.ExecuteNonQuery();
                Console.WriteLine("Database creato con successo!");
            }
            catch (SqlException ex)
            {
                if (ex.Number == (int)ECodiciDb.DatabaseEsistente)
                {
                    Console.WriteLine("La database e stata già creata!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

        }

        //creare la tabella Product
        public static void CreateProductTable()
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
                create table Product(
                    IdProduct INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                    Name NVARCHAR(50) NOT NULL,
                    Description NVARCHAR(1000) NOT NULL,           
                    Price DECIMAL(6,2) NOT NULL,
                    Image NVARCHAR(500) NOT NULL,
                    Quantity INT NOT NULL,
                    Date DATETIME NOT NULL 
             )";

            var commandSql = new SqlCommand(commandText, connection);


            try
            {
                commandSql.ExecuteNonQuery();
                Console.WriteLine("Tabella Product creata con successo!");
            }
            catch (SqlException ex)
            {
                if (ex.Number == (int)ECodiciDb.TabellaEsistente)
                {
                    Console.WriteLine("La tabella Product e stata già creata!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

        }


        //creare la tabella category
        public static void CreateCategoryTable()
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();
            var commandText = @"
                create table Category(
                     IdCategory INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                     Name NVARCHAR(50) NOT NULL
             )";


            var commandSql = new SqlCommand(commandText, connection);

            try
            {
                commandSql.ExecuteNonQuery();
                Console.WriteLine("La tabella Category e stata creata con successo!");
            }
            catch (SqlException ex)
            {
                if (ex.Number == (int)ECodiciDb.TabellaEsistente)
                {
                    Console.WriteLine("La tabella Category e già stata creata");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

        }

        //creare tabella Product_Category

        public static void CreateProductCategoryTable()
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
                    create table Product_Category(

                    IdProductFK INT NOT NULL,
                    IdCategoryFK INT NOT NULL,
                    PRIMARY KEY (IdProductFK, IdCategoryFK),
                    FOREIGN KEY (IdProductFK) REFERENCES Product(IdProduct) ON DELETE CASCADE,
                    FOREIGN KEY (IdCategoryFK) REFERENCES Category(IdCategory) ON DELETE CASCADE
                )";

            var commandSql = new SqlCommand(commandText, connection);

            try
            {
                commandSql.ExecuteNonQuery();
                Console.WriteLine("La tabella Product_Category e stata creata con successo");
            }
            catch (SqlException ex)
            {
                if (ex.Number == (int)ECodiciDb.TabellaEsistente)
                {
                    Console.WriteLine("La tabella Product_Category e già stata creata");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }


        // creare tabella carrello

        public static void CreateCarrelloTable()
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
                create table Carrello(
                    IdCarrello INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                    IdProductFK INT NOT NULL,
                    Quantity INT NOT NULL,
                    Size NVARCHAR(10) NULL,        
                    Date DATETIME NOT NULL,
                    FOREIGN KEY (IdProductFK) REFERENCES Product(IdProduct) ON DELETE CASCADE
            )";

            var commandSql = new SqlCommand(commandText, connection);

            try
            {
                commandSql.ExecuteNonQuery();
                Console.WriteLine("La tabella Carrello e stata creata con successo");
            }

            catch (SqlException ex)
            {
                if (ex.Number == (int)ECodiciDb.TabellaEsistente)
                {
                    Console.WriteLine("La tabella Carrello e già stata creata");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }


        }


        // inserire le categorie

        public static void InsertCategories()
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
            INSERT INTO Category (Name) VALUES 
            ('Mans'),
            ('Womens'),
            ('Kids')";

            var command = new SqlCommand(commandText, connection);

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Categorie inserite con successo!");
            }
            catch (SqlException)
            {
                Console.WriteLine("Le categorie sono già state inserite");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // prendi prodotti per categoria
        public static List<Product> GetProductsByCategory(int categoryId)
        {
            var products = new List<Product>();

            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
        SELECT p.* 
        FROM Product p
        INNER JOIN Product_Category pc ON p.IdProduct = pc.IdProductFK
        WHERE pc.IdCategoryFK = @categoryId";

            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@categoryId", categoryId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    IdProduct = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Image = reader.GetString(4),
                    Quantity = reader.GetInt32(5),
                    Date = reader.GetDateTime(6)
                });
            }

            return products;
        }

        public static void AddProductToCategory(int productId, int categoryId)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
        INSERT INTO Product_Category (IdProductFK, IdCategoryFK) 
        VALUES (@productId, @categoryId)";

            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@productId", productId);
            command.Parameters.AddWithValue("@categoryId", categoryId);

            command.ExecuteNonQuery();
        }

        // modifica per ritornare id
        public static int AddProductAndGetId(Product product)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
        INSERT INTO Product (Name, Description, Price, Image, Quantity, Date) 
        OUTPUT INSERTED.IdProduct
        VALUES (@name, @desc, @price, @image, @qty, @date)";

            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@desc", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@image", product.Image);
            command.Parameters.AddWithValue("@qty", product.Quantity);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            return (int)command.ExecuteScalar();
        }

        //lista tutti prodotti

        public static List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Product", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    IdProduct = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Image = reader.GetString(4),
                    Quantity = reader.GetInt32(5),
                    Date = reader.GetDateTime(6)
                });
            }

            return products;
        }

        // visualizza prodotto by Id

        public static Product? GetProductById(int id)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Product WHERE IdProduct = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Product
                {
                    IdProduct = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Image = reader.GetString(4),
                    Quantity = reader.GetInt32(5),
                    Date = reader.GetDateTime(6)
                };
            }

            return null;
        }

        //creare un prodotto

        public static void AddProduct(Product product)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
            INSERT INTO Product (Name, Description, Price, Image, Quantity, Date) 
            VALUES (@name, @desc, @price, @image, @qty, @date)";

            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@desc", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@image", product.Image);
            command.Parameters.AddWithValue("@qty", product.Quantity);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            command.ExecuteNonQuery();
        }

        //aggiornare prodotto

        public static void ModifyProduct(Product product)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
        UPDATE Product 
        SET Name = @name, 
            Description = @desc, 
            Price = @price, 
            Image = @image, 
            Quantity = @qty 
        WHERE IdProduct = @id";

            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", product.IdProduct);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@desc", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@image", product.Image);
            command.Parameters.AddWithValue("@qty", product.Quantity);

            command.ExecuteNonQuery();
        }

        //elimina prodotto

        public static void DeleteProduct(int id)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var command = new SqlCommand("DELETE FROM Product WHERE IdProduct = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }

        // visualizza categorie
        public static List<Category> GetAllCategories()
        {
            var categories = new List<Category>();

            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Category", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new Category
                {
                    IdCategory = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return categories;
        }

        //aggiungi al carrello

        public static void AddToCart(int productId, int quantity, string? size)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
        INSERT INTO Carrello (IdProductFK, Quantity, Size, Date) 
        VALUES (@productId, @qty, @size, @date)";

            var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@productId", productId);
            command.Parameters.AddWithValue("@qty", quantity);
            command.Parameters.AddWithValue("@size", size ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            command.ExecuteNonQuery();
        }

        // visualizza carrello 

        public static List<Carrello> GetCartItems()
        {
            var cartItems = new List<Carrello>();

            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Carrello", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                cartItems.Add(new Carrello
                {
                    IdCarrello = reader.GetInt32(0),
                    IdProductFK = reader.GetInt32(1),
                    Quantity = reader.GetInt32(2),
                    Size = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Date = reader.GetDateTime(4)
                });
            }

            return cartItems;
        }

        public static List<Carrello> GetCartItemsWithProducts()
        {
            var cartItems = new List<Carrello>();

            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var commandText = @"
        SELECT c.IdCarrello, c.IdProductFK, c.Quantity, c.Size, c.Date,
               p.IdProduct, p.Name, p.Description, p.Price, p.Image, p.Quantity as Stock, p.Date as ProductDate
        FROM Carrello c
        INNER JOIN Product p ON c.IdProductFK = p.IdProduct";

            var command = new SqlCommand(commandText, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                cartItems.Add(new Carrello
                {
                    IdCarrello = reader.GetInt32(0),
                    IdProductFK = reader.GetInt32(1),
                    Quantity = reader.GetInt32(2),
                    Size = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Date = reader.GetDateTime(4),
                    Product = new Product
                    {
                        IdProduct = reader.GetInt32(5),
                        Name = reader.GetString(6),
                        Description = reader.GetString(7),
                        Price = reader.GetDecimal(8),
                        Image = reader.GetString(9),
                        Quantity = reader.GetInt32(10),
                        Date = reader.GetDateTime(11)
                    }
                });
            }

            return cartItems;
        }

        //elimina dal carrello

        public static void RemoveFromCart(int cartId)
        {
            using var connection = new SqlConnection(_shopConnectionString);
            connection.Open();

            var command = new SqlCommand("DELETE FROM Carrello WHERE IdCarrello = @id", connection);
            command.Parameters.AddWithValue("@id", cartId);

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Prodotto rimosso dal carrello!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }
        }

    }
}

