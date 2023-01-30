namespace EFCore.Models;

public class Order
{
    public int Id { get; set; }

    /// <summary>
    /// 下单时间
    /// </summary>
    public DateTime OrderPlaced { get; set; }

    /// <summary>
    /// 订单完成时间
    /// </summary>
    public DateTime? OrderFulfilled { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    /// <summary>
    /// 订单详情
    /// </summary>
    public ICollection<OrderDetail> OrderDetail { get; set; } = null!;
}