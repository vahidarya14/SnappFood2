using Core.Application;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace SnappFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListAsync(int pageNumber, int pagesize)
        {
            return Ok(await _productService.ListAsync(pageNumber,  pagesize));
        }

        [HttpPost("CreateNew")]
        public async Task<IActionResult> CreateNew(ProductCreateUpdateDto product)
        {
            return Ok(new{ id = await _productService.CreateNew(product)});
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(long id,ProductCreateUpdateDto product)
        {
            return Ok(new { result = await _productService.Update(id, product) });
        }


        [HttpPost("IncreseInventoryCount")]
        public async Task<IActionResult> IncreseInventoryCount(IncreseInventoryCountDto dto)
        {
            return Ok(new { newInventoryCount = await _productService.IncreseInventoryCount(dto.ProductId, dto.InventoryCount) });
        }


    }
}