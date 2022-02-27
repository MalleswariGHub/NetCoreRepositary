using CateLogAPI.Entities;
using CateLogAPI.Repositary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CateLogAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatelogController:ControllerBase
    {
        private readonly IProductRepositary _repositary;
        private readonly ILogger<CatelogController> _logger;

        public CatelogController(IProductRepositary repositary, ILogger<CatelogController> logger)
        {
            _repositary = repositary?? throw new ArgumentNullException(nameof(repositary));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var product = await _repositary.GetProducts();
            return Ok(product);
        }

        [HttpGet("{id:length(24)}",Name = "GetProducts")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string Id)
        {
            var prodcut = await _repositary.GetProducts(Id);
            if(prodcut==null)
            {
                _logger.LogError($"Prodcut with ID :{Id} ,not found");
                return NotFound();

            }
            return Ok(prodcut);
        }


        [Route("[action]/{CategoryName}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string CategoryName)
        {
            var prodcut = await _repositary.GetProductByCategory(CategoryName);
            if (prodcut == null)
            {
                _logger.LogError($"Prodcut with category :{CategoryName} ,not found");
                return NotFound();

            }
            return Ok(prodcut);
        }


        [Route("[action]/{ProductName}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string ProductName)
        {
            var prodcut = await _repositary.GetProductByName(ProductName);
            if (prodcut == null)
            {
                _logger.LogError($"Prodcut with Product :{ProductName} ,not found");
                return NotFound();

            }
            return Ok(prodcut);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repositary.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
          return Ok( await _repositary.UpdateProduct(product));

        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            return Ok(await _repositary.DeleteProduct(Id));

        }

    }
}
