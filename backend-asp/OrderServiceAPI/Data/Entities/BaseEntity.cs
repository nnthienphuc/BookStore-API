using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServiceAPI.Data.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("isDeleted")]
    public bool IsDeleted { get; set; } = false;
}
