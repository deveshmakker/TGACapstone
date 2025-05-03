using Product.Application.DTO;

namespace Product.Application.Interfaces
{
    public interface IProductService
    {
         Task<IEnumerable<ProductDTO>> GetProducts();
        ProductDTO FindProductById(int productId);
        ProductDTO AddProduct(ProductDTO product);
        ProductDTO EditProduct(int productid, ProductDTO product);
        bool DeleteProduct(int productid);
        Task<bool> UpdateInventory(int productId, int quantity);
        CategoryDTO AddCategory(CategoryDTO category);        
        CategoryDTO EditCategory(int categoryid, CategoryDTO category);
        bool DeleteCategory(int categoryid);
        CategoryDTO FindCategoryById(int categoryid);
    }
}
