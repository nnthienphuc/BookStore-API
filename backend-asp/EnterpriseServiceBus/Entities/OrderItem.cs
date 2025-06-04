using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseServiceBus.Entities;


[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    [Column("orderId")]
    public Guid OrderId { get; set; }

    [Key]
    [Column("bookId")]
    public Guid BookId { get; set; }

    [Column("quantity")]
    public short Quantity { get; set; }

    [Column("price", TypeName = "decimal(8, 0)")]
    public decimal Price { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

}
