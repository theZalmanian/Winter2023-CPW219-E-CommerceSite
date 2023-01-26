using System.ComponentModel.DataAnnotations;

namespace E_CommerceSite.Models
{
    /// <summary>
    /// Represent's a product currently being sold
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The product's unique identifier
        /// </summary>
        [Key]
        public int ProductID { get; set; }

        /// <summary>
        /// The product's name
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// The product's sales price
        /// </summary>
        [Required]
        [Range(0, Double.MaxValue)]
        public double ProductPrice { get; set; }
    }
}
