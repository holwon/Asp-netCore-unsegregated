using System.ComponentModel.DataAnnotations.Schema;

namespace LearnCourseCore.WebSite.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        // [ForeignKey("Category")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
