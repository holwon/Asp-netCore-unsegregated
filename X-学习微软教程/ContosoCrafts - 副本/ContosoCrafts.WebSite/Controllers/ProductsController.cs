using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Controllers;
//[Route("/[controller]/[action]")]//此处是以Controller类和方法的名称来自动映射的, 其效果与下面一句等同
[Route("/[controller]")]//此处是以Controller类和方法的名称来自动映射的, 其效果与下面一句等同
//[Route("/Products/[action]")]//[action]是方法名称, 如/Products/GetProducts
[ApiController]
public class ProductsController : ControllerBase
{
    public ProductsController(JsonFileProductService productService)
    {
        ProductService = productService;
    }

    public JsonFileProductService ProductService { get; }

    [HttpGet]// https://localhost:44376/Products/GetProducts
    public IEnumerable<Product> GetProducts() => ProductService.GetProducts();

    // [HttpPatch] "[FromBody]"
    //[Route("Rate")]// https://localhost:44376/products/rate?productId=jenlooper-cactus&Rating=4
    //[HttpGet]
    //public ActionResult Get(
    //    [FromQuery] string productId,
    //    [FromQuery] int Rating)
    //{
    //    ProductService.AddRating(productId, Rating);
    //    return Ok();
    //}

    /// <summary>
    /// 对[Route("Rate")]进行了修改
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    public ActionResult Patch([FromBody] RatingRequest request)
    {
        if (request?.ProductId == null)
            return BadRequest();

        ProductService.AddRating(request.ProductId, request.Rating);

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
