namespace BW_1_S4_L1.Models
{
    public class Carrello
    {
        public int IdCarrello { get; set; }
        public int IdProductFK { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public DateTime Date { get; set; }

        //relazioni uno a molti
        public Product Product { get; set; }
    }
}
