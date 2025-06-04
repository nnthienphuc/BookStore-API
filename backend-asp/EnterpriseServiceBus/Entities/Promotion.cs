namespace EnterpriseServiceBus.Entities;
public partial class Promotion : BaseEntity
{

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    public decimal Condition { get; set; }

    public decimal DiscountPercent { get; set; }

    public short Quantity { get; set; }

}
