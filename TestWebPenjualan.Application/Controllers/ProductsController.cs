using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TestWebPenjualan.Application.Auth;
using TestWebPenjualan.Application.Helpers;
using TestWebPenjualan.Application.Interfaces;
using TestWebPenjualan.Domain.Dtos.HttpResponse;
using TestWebPenjualan.Domain.Dtos.Login;
using TestWebPenjualan.Domain.Dtos.Product;
using TestWebPenjualan.Domain.Helpers;


namespace TestWebPenjualan.Application.Controllers
{
    [ServiceFilter(typeof(AuthorizeActionFilter))]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductApiHelper _productApiHelper;
        public ProductsController(
            ILogger<ProductsController> logger,
            IProductApiHelper productApiHelper)
        {
            _logger = logger;
            _productApiHelper = productApiHelper;

        }

        public async Task<IActionResult> Index()
        {
            var unitTypes = await _productApiHelper.GetUnitTypes();

            ViewData["unitTypes"] = unitTypes;

            return View();
        }

        public async Task<IActionResult> Add()
        {
            await GetUnitTypes();
            await GetCategories();
            await GetBrands();

            return View();
        }

        [HttpPost("products/add")]
        [ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(CreateProductDto productToAdd)
        {
            if (!ModelState.IsValid)
            {
                await GetUnitTypes();
                await GetCategories();
                await GetBrands();
                return View();
            }            

            var result = await _productApiHelper.AddProduct(productToAdd);

            if (result.Success)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("Index", "Products");
            }
            else
            {
                await GetUnitTypes();
                await GetCategories();
                await GetBrands();
                TempData["ErrorMessage"] = result.Message;
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            await GetUnitTypes();
            await GetCategories();
            await GetBrands();

            var product = await _productApiHelper.GetProductByProductId(id);

            var productToUpdate = product.Adapt<UpdateProductDto>();

            return View(productToUpdate);
        }

        [HttpPost("products/edit")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(UpdateProductDto productToUpdate)
        {
            if (!ModelState.IsValid)
            {
                await GetUnitTypes();
                await GetCategories();
                await GetBrands();
                return View(productToUpdate);
            }

            var result = await _productApiHelper.UpdateProduct(productToUpdate);

            if (result.Success)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("Index", "Products");
            }
            else
            {
                await GetUnitTypes();
                await GetCategories();
                await GetBrands();
                TempData["ErrorMessage"] = result.Message;
            }

            return View(productToUpdate);
        }

        private async Task GetUnitTypes()
        {
            var unitTypes = await _productApiHelper.GetUnitTypes();

            ViewData["unitTypes"] = unitTypes;
        }

        private async Task GetCategories()
        {
            var categories = await _productApiHelper.GetCategories();

            ViewData["categories"] = categories;
        }

        private async Task GetBrands()
        {
            var brands = await _productApiHelper.GetBrands();

            ViewData["brands"] = brands;
        }

        public async Task<IActionResult> GetProductsInJson(GetProductsWithPagingFilter filter)
        {

            List<Product> products = new List<Product>();
            var totalRows = 0;

            try
            {
                products = await _productApiHelper.GetProducts(filter);
                totalRows = await _productApiHelper.GetProductsRowsCount(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            var returnObj = new
            {
                draw = filter.Draw,
                recordsTotal = totalRows,
                recordsFiltered = totalRows,
                data = products
            };

            return Json(returnObj);
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productApiHelper.DeleteProduct(productId);

            if (result.Success)
            {
                TempData["Message"] = result.Message;
                var returnObj = new
                {
                    Success = true,
                    Message = result.Message
                };

                return Json(returnObj);
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;               
            }
            return View("Index");
        }
    }
}
