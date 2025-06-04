namespace EnterpriseServiceBus.DTOs.Category;

public class CategoryUpdateDTO
{
    public required string Name { get; set; }
    public required bool IsDeleted { get; set; }
}
