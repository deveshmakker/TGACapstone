using Product.Application.DTO;
using Product.Application.Interfaces;
using AutoMapper;
using Product.Common.Interfaces;
using Product.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Capstone.Product.Common.Model;

namespace Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this._repository = productRepository;
            this._mapper = mapper;
        }
        public CategoryDTO AddCategory(CategoryDTO categoryDto)
        {
            return _mapper.Map<CategoryDTO>(_repository.AddCategory(_mapper.Map<CategoryDetails>(categoryDto)));
        }

        public ProductDTO AddProduct(ProductDTO productDto)
        {
            var category = _repository.FindCategoryById(productDto.CategoryId);

            var productDetails = new ProductDetails
            {
                CategoryId = category.Id,
                Description = productDto.Description,
                DiscountPercentage = productDto.DiscountPercentage,
                IsAvailable = productDto.IsAvailable,
                Name = productDto.Name,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity                
            };

            return _mapper.Map<ProductDTO>(_repository.AddProduct(productDetails));
        }

        public bool DeleteCategory(int categoryid)
        {
            return _repository.DeleteCategory(categoryid);
        }

        public bool DeleteProduct(int productid)
        {
            return _repository.DeleteProduct(productid);
        }

        public CategoryDTO EditCategory(int categoryid, CategoryDTO categoryDto)
        {
            var category = _repository.EditCategory(categoryid,_mapper.Map<CategoryDetails>(categoryDto));

            return _mapper.Map<CategoryDTO>(category);
        }

        public ProductDTO EditProduct(int productid, ProductDTO productDto)
        {
            var product = _repository.EditProduct(productid, _mapper.Map<ProductDetails>(productDto));

            return _mapper.Map<ProductDTO>(product);
        }

        public ProductDTO FindProductById(int productId)
        {
            //todo - Add Caching for products using redis and read from redis
            var product = _repository.FindProductById(productId);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            //todo - Add Caching for products using redis and read from redis
            var products = _repository.GetProducts();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);            
        }

        public async Task<bool> UpdateInventory(int productId, int quantity)
        {            
            return _repository.UpdateInventory(productId, quantity);
        }
        public CategoryDTO FindCategoryById(int categoryid)
        {
            var category = _repository.FindCategoryById(categoryid);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
