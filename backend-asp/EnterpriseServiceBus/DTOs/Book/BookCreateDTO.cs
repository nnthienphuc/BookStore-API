namespace EnterpriseServiceBus.DTOs.Book;


public class BookCreateDTO
{
    public required string Isbn { get; set; }
    public required string Title { get; set; }
    public required Guid CategoryId { get; set; }
    public required Guid AuthorId { get; set; }
    public required Guid PublisherId { get; set; }
    public required short YearOfPublication { get; set; }
    public required decimal Price { get; set; }
    public required string Image { get; set; }
    public required int Quantity { get; set; }
}
