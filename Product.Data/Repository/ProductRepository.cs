using Capstone.Product.Common.Model;
using Microsoft.EntityFrameworkCore;
using Product.Common.Interfaces;
using Product.Common.Model;
using Product.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public CategoryDetails AddCategory(CategoryDetails category)
        {
            _productDbContext.Categories.Add(category);
            _productDbContext.SaveChanges();
            return category;
        }

        public ProductDetails AddProduct(ProductDetails product)
        {
            _productDbContext.Products.Add(product);
            _productDbContext.SaveChanges();
            return product;
        }

        public bool DeleteCategory(int categoryid)
        {
            bool result = false;
            _productDbContext.Remove(FindCategoryById(categoryid));
            result = (_productDbContext.SaveChanges() != 0);
            return result;
        }

        public bool DeleteProduct(int productid)
        {
            bool result = false;
            _productDbContext.Remove(FindProductById(productid));
            result = (_productDbContext.SaveChanges() !=0);
            return result;
        }

        public CategoryDetails EditCategory(int categoryid, CategoryDetails category)
        {
            var categoryDetails = FindCategoryById(categoryid);
            _productDbContext.Entry(categoryDetails).CurrentValues.SetValues(category);
            _productDbContext.SaveChanges();
            return category;
        }

        public ProductDetails EditProduct(int productid, ProductDetails product)
        {
            var productDetails = FindProductById(productid);
            _productDbContext.Entry(productDetails).CurrentValues.SetValues(product);
            _productDbContext.SaveChanges();
            return product;
        }

        public CategoryDetails FindCategoryById(int categoryid)
        {
            return _productDbContext.Categories.FirstOrDefault(c => c.Id == categoryid);
        }

        public ProductDetails FindProductById(int productId)
        {
            return _productDbContext.Products
                .Include(p => p.Category)
                .FirstOrDefault(x => x.ProductId == productId);
        }

        public IEnumerable<ProductDetails> GetProducts()
        {
            return _productDbContext.Products
                .Include(p => p.Category)
                .ToList();
        }

        public bool UpdateInventory(int productId, int quantity)
        {
            bool result = false;
            var productDetails = FindProductById(productId);
            productDetails.StockQuantity += quantity;
            result = (_productDbContext.SaveChanges() != 0);
            return result;
        }
    }
}
