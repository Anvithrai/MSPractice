
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()

        {
            List<ProductDto>? list = new();

            ResponseDto? response = await _productService.GetAllProductAsync();
            if(response != null && response.IsSuccess) 
            {
                list=JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);  

           
        }
        public async Task<IActionResult> CreateProduct()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto model)
        {
            if(ModelState.IsValid)
            {
                ResponseDto response=await _productService.CreateProductAsync(model);
                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDto? response=await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();

        }

        [HttpPost]
		public async Task<IActionResult> ProductDelete(ProductDto productDTO)
		{
			ResponseDto? response = await _productService.DeleteProductAsync(productDTO.ProductId);
			if (response != null && response.IsSuccess)
			{
                return RedirectToAction(nameof(ProductIndex));
			
			}
			return View(productDTO);

		}
        [HttpGet]
        public async Task<IActionResult> ProductUpdate(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDto productDTO)
        {
            ResponseDto? response = await _productService.UpdateProductAsync(productDTO);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));

            }
            return View(productDTO);

        }



    }
}
