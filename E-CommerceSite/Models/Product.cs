using System.ComponentModel.DataAnnotations;

namespace E_CommerceSite.Models
{
    /// <summary>
    /// Represent's a Product currently being sold
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The Product's unique identifier
        /// </summary>
        [Key]
        public int ProductID { get; set; }

        /// <summary>
        /// The Product's name
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// The Product's sales price
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double ProductPrice { get; set; }
    }

    /// <summary>
    /// Represent's an individual Product that has been added 
    /// to the current user's "shopping cart" cookie
    /// </summary>
    public class ProductCartViewModel
    {
        /// <summary>
        /// The Product's unique identifier
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// The Product's unique identifier
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The Product's unique identifier
        /// </summary>
        public double ProductPrice { get; set; }

        /// <summary>
        /// Constructs a Product for the purpose of being added to the "shopping cart" cookie, with the given data
        /// </summary>
        /// <param name="productID">The Product's unique identifier</param>
        /// <param name="productName">The Product's unique identifier</param>
        /// <param name="productPrice">The Product's unique identifier</param>
        public ProductCartViewModel(int productID, string productName, double productPrice)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
        }
    }
}
