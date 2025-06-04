namespace EnterpriseServiceBus.DTOs.Author;


public class AuthorUpdateDTO
{
    public required string Name { get; set; }

    public required bool IsDeleted { get; set; }
}
