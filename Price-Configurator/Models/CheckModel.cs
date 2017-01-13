namespace Price_Configurator.Models
{
    public class CheckModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Checked { get; set; }

        public int ProductId { get; set; }
    }
}