using LearnCourseCore.WebSite.infrastructure;
using LearnCourseCore.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourseCore.WebSite.Controllers
{
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        [HttpGet("{id}")]
        // 用[FromServices]将 ILogger注册为服务
        public ActionResult<Product> GetProductsById(long id, [FromServices] ILogger<Product> logger)
        {
            logger.LogDebug("GetProducts Action: {0}", id);
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return product;
            }
        }

        [HttpPost]
        public ActionResult SaveProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }
    }
}
