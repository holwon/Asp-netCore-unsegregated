namespace LearnCourseCore.WebSite.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        // 建立 Category与 Product的表关系
        public IEnumerable<Product> Products { get; set; }
    }
}
