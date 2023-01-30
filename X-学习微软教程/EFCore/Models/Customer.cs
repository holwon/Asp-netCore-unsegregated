using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "nvarchar(200)")]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "nvarchar(200)")]
        public string? Address { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Phone { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Email { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
