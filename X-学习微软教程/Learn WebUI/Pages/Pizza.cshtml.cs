using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Pages
{
    public class PizzaModel : PageModel
    {
        //  [BindProperty] 属性验证并传递“披萨”窗体中的 Pizza 条目
        [BindProperty]
        public Pizza NewPizza { get; set; } = new();

        public List<Pizza> pizzas = new();
        public void OnGet()
        {
            pizzas = PizzaService.GetAll();
        }

        // Gluten 麸质、面筋的意思
        public string GlutenFreeText(Pizza pizza)
        => pizza.IsGlutenFree ? "Gluten Free" : "Not Gluten Free";

        public IActionResult OnPost()
        {
            // ModelState.IsValid 是验证页面的模型是否有效
            // 如果尝试的 PageModel 更改无效，则再次向用户显示“披萨”页面。 会显示一条阐明输入要求的消息

            if (!ModelState.IsValid)
            {
                return Page();
            }
            PizzaService.Add(NewPizza);

            // 重定向到 GET， 这是个很常见的模式。叫做 PRG模式(Post->Redirect->Get  )
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            PizzaService.Delete(id);
            return RedirectToAction("Get");
        }
    }
}
