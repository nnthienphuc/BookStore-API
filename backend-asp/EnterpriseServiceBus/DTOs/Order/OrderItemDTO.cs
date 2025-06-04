namespace EnterpriseServiceBus.DTOs;


public class OrderItemDTO
{
    public Guid OrderId { get; set; }
    public Guid BookId { get; set; }
    public short Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
}
