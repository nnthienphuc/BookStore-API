using System.ComponentModel.DataAnnotations.Schema;
namespace EnterpriseServiceBus.Entities;


[Table("Order")]
public partial class Order : BaseEntity
{
    [Column("staffId")]
    public Guid StaffId { get; set; }

    [Column("customerId")]
    public Guid CustomerId { get; set; }

    [Column("promotionId")]
    public Guid? PromotionId { get; set; }

    [Column("createdTime")]
    public DateTime CreatedTime { get; set; }

    [Column("totalAmount", TypeName = "decimal(11, 3)")]
    public decimal TotalAmount { get; set; }

    [Column("subTotalAmount", TypeName = "decimal(11, 3)")]
    public decimal SubTotalAmount { get; set; }

    [Column("promotionAmount", TypeName = "decimal(11, 3)")]
    public decimal PromotionAmount { get; set; }

    [Column("status")]
    public bool Status { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
