namespace E_CommerceSite.Models
{
    public class ProductCatalogViewModel
    {
        /// <summary>
        /// The current page of the Products catalog (the page the user is currently viewing)
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// A list containing all Products do be displayed on the current page
        /// </summary>
        public List<Product> ProductsToDisplay { get; private set; }

        /// <summary>
        /// The last page of the Products catalog
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// Constructs a page of the Products catalog with the given Products, page number, and last page
        /// </summary>
        /// <param name="currPage">The current page of the Products catalog</param>
        /// <param name="productsToDisplay">A list containing all Products do be displayed on the current page</param>
        /// <param name="lastPage">The last page of the Products catalog</param>
        public ProductCatalogViewModel(int currPage, List<Product> productsToDisplay, int lastPage)
        {
            CurrentPage = currPage;
            ProductsToDisplay = productsToDisplay;
            LastPage = lastPage;
        }
    }
}
