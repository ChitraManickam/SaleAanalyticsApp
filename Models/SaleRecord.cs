namespace SaleAanalyticsApp.Models
{
    public class SaleRecord
    {
        public int Id { get; set; }

        public required string ProductName { get; set; }

        public decimal Price { get; set; }

        public DateTime SaleDate { get; set; }

        public required string Region { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
