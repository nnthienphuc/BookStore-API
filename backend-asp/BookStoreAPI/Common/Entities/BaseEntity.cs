using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Common.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsDeleted { get; set; } = false;
}
