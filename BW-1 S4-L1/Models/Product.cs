namespace BW_1_S4_L1.Models
{
    public class Product
    {
        public Guid IdProduct { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateOnly Date { get; set; }

    }
}
