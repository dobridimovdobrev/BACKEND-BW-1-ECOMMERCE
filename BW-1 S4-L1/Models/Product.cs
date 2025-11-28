namespace BW_1_S4_L1.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

        //relazioni molti a motli
        public List<Category> Categories { get; set; }
        public List<Carrello> Carrelli { get; set; }

    }
}
