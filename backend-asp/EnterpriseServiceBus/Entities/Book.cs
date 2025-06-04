namespace EnterpriseServiceBus.Entities;
public partial class Book : BaseEntity
{
    public string Isbn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public Guid CategoryId { get; set; }

    public Guid AuthorId { get; set; }


    public Guid PublisherId { get; set; }

    public short YearOfPublication { get; set; }

    public decimal Price { get; set; }

    public string Image { get; set; } = null!;

    public int Quantity { get; set; }

}
