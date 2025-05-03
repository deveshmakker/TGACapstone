using Capstone.Product.Common.Model;
using Product.Common.Model;


namespace Product.Common.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<ProductDetails> GetProducts();
        ProductDetails FindProductById(int productId);
        ProductDetails AddProduct(ProductDetails product);
        ProductDetails EditProduct(int productid, ProductDetails product);
        bool DeleteProduct(int productid);
        bool UpdateInventory(int productId, int quantity);
        CategoryDetails AddCategory(CategoryDetails category);
        CategoryDetails EditCategory(int categoryid, CategoryDetails category);
        CategoryDetails FindCategoryById(int categoryid);
        bool DeleteCategory(int categoryid);
    }
}
