using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BW_1_S4_L1.Models
{
    public class ProductDetails
    {

        public int IdProduct { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
