using Product.Common.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Capstone.Product.Common.Model
{

    public class ProductDetails
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        public CategoryDetails Category { get; set; }

    }
}