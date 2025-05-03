//using Capstone.Product.Common.Model;
using Product.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Product.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, ADMIN, user, USER")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ResponseDTO _responseDTO;
        public ProductController(IProductService productService)
        {
            _productService = productService;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _responseDTO.Result = await _productService.GetProducts();
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return Ok(_responseDTO);
        }

        [HttpGet]
        [Route("{productId:int}")]
        public IActionResult Get(int productId)
        {
            try
            {
                _responseDTO.Result = _productService.FindProductById(productId);
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("addproduct")]
        public IActionResult AddProduct([FromBody]ProductDTO product)
        {
            try
            {
                _responseDTO.Result = _productService.AddProduct(product);
                _responseDTO.Message = "Successfully added the Product";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return Ok(_responseDTO);
        }
        
        [HttpPut("editproduct/{id:int}")]
        public IActionResult EditProduct(int id, [FromBody] ProductDTO productDto)
        {
            try
            {
                var product = _productService.FindProductById(id);
                if (product == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Product not found";                    
                }
                else
                {
                    _responseDTO.Result = _productService.EditProduct(id, productDto);
                    _responseDTO.IsSuccess = true;
                    _responseDTO.Message = "Product Updated Successfully.";
                }                
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("updateinventory/{productId:int}/{quantity:int}")]
        public async Task<IActionResult> UpdateInventory(int productId, int quantity)
        {
            try
            {
                _responseDTO.Result = await _productService.UpdateInventory(productId, quantity);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Inventory updated successfully";
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpDelete("deleteproduct/{productId:int}")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                _responseDTO.Result = _productService.DeleteProduct(productId);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Product Deleted Successfully";

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("addcategory")]
        public IActionResult AddCategory([FromBody] CategoryDTO category)
        {
            try
            {
                _responseDTO.Result = _productService.AddCategory(category);
                _responseDTO.Message = "Successfully added the Category";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("editcategory/{categoryid:int}")]
        public IActionResult EditCategory(int categoryid, [FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = _productService.FindCategoryById(categoryid);
                if (category == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Category not found";
                }
                else
                {
                    _responseDTO.Result = _productService.EditCategory(categoryid, categoryDto);
                    _responseDTO.IsSuccess = true;
                    _responseDTO.Message = "Category Updated Successfully.";
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpDelete("deletecategory/{categoryid:int}")]
        public IActionResult DeleteCategory(int categoryid)
        {
            try
            {
                _responseDTO.Result = _productService.DeleteCategory(categoryid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Category Deleted Successfully.";
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

    }
}
