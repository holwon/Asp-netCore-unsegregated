using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Controllers;
[Route("/[controller]")]//此处是以Controller类的名称来自动映射的, 其效果与下面一句等同
                        //[Route("/Products")]
[ApiController]
public class ProductsController : ControllerBase
{
    public ProductsController(JsonFileProductService productService)
    {
        ProductService = productService;
    }

    public JsonFileProductService ProductService { get; }

    [HttpGet]
    public IEnumerable<Product> GetProducts() => ProductService.GetProducts();

    //// [HttpPatch] "[FromBody]"
    //[Route("Rate")]// https://localhost:44376/products/rate?productId=jenlooper-cactus&Rating=4
    //[HttpGet]
    //public ActionResult Get(
    //    [FromQuery] string productId,
    //    [FromQuery] int Rating)
    //{
    //    ProductService.AddRating(productId, Rating);
    //    return Ok();
    //}

    [HttpPatch]
    public ActionResult Patch([FromBody] RatingRequest request)
    {
        if (request?.ProductId == null)
            return BadRequest();

        ProductService.AddRating(request.ProductId, request.Rating);

        // return NoContent();
        return Ok();
    }

    public class RatingRequest
    {
        public string? ProductId { get; set; }
        public int Rating { get; set; }
    }


    [Route("Test")]
    [HttpGet]
    public string GetAction() => "Action Test";
}
