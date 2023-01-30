using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]-[action]")]
    [ApiController]
    public class RecipesController : Controller
    {
        [HttpGet]
        public ActionResult GetRecipes([FromQuery] int count)
        {
            // string[] recipes = { "Oxtail", "Curry Chicken", "Dumping", "Milk" };
            Recipes[] recipes ={
                new() {Title="Oxtail"},
                new() {Title="Curry Chicken"},
                new() {Title="Dumping"},
            };
            // string[] recipes = {  };
            if (!recipes.Any())
            {
                return NotFound();
            }
            return Ok(recipes.Take(count));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRecipes([FromBody] Recipes newRecipes)
        {
            // validate and save to DB
            bool badThingsHappened = false;
            if (badThingsHappened) { return BadRequest(); }

            var water = await BoilWaterAsync();
            System.Console.WriteLine("✨✨" + water + "✨✨");
            System.Console.WriteLine(newRecipes);
            return Created("", newRecipes);
        }

        async Task<string> BoilWaterAsync()
        {
            System.Console.WriteLine("start the kettle and add water");
            System.Console.WriteLine("waiting for the kettle to boil");
            await Task.Delay(500);// 烧水 simulate waiting for the kettle to boil 
            System.Console.WriteLine("kettle finished boiling");
            // await Task.FromResult("water");
            return "water";
        }

        [HttpDelete]
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;
            if (badThingsHappened)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // // GET: RecipesController
        // public ActionResult Index()
        // {
        //     return View();
        // }

        // // GET: RecipesController/Details/5
        // public ActionResult Details(int id)
        // {
        //     return View();
        // }

        // // GET: RecipesController/Create
        // public ActionResult Create()
        // {
        //     return View();
        // }

        // // POST: RecipesController/Create
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Create(IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }

        // // GET: RecipesController/Edit/5
        // // public ActionResult Edit(int id)
        // // {
        // //     return View();
        // // }

        // // POST: RecipesController/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Edit(int id, IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }

        // // GET: RecipesController/Delete/5
        // public ActionResult Delete(int id)
        // {
        //     return View();
        // }

        // // POST: RecipesController/Delete/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Delete(int id, IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }
    }
}