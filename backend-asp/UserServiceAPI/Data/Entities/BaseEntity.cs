using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserServiceAPI.Data.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("isDeleted")]
    public bool IsDeleted { get; set; } = false;
}
