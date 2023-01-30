using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models;

public class Product
{
    [Key]// 可以用 [Key]来标识主键, 当然 ef Core会自动将id认为是主键
    public int Id { get; set; }

    // 因为 dotnet6 的项目默认都启用可为空的 引用类型
    // 如果没有这样 =null!
    // 编译器会警告我们说: 看不到 Name是在哪里初始化的
    // Note: 这里 =null! 的意思是 变量不可为 null
    public string Name { get; set; } = null!;

    // Tips: 或者如果你想在程序中使用 可null
    // 但是不希望数据库中 可 null, 可以使用 [Required]特性
    /// <code>
    /// [Required]
    /// public string? Name { get; set; }
    /// </code>
    [Display(Name = "价格")]
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }
}
