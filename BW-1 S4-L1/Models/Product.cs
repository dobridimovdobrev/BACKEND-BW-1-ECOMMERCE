using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BW_1_S4_L1.Models
{
    public class Product
    {
        public int IdProduct { get; set; }

        public string Image { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public List<Category> Category { get; set; }
    }
}
