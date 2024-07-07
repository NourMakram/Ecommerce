using EcommercePro.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommercePro.DTO
{
    public class orderDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public string ProductDescription { get; set; }
        public List<string> ImageUrls { get; set; }

        public decimal Price { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalSales { get; set; }
        public decimal ProfitPercentage { get; set; }

    }
}
