using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStoreAPI.Services.BookService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services.PublisherService.Entities;

[Table("Publisher")]
[Index("Name", Name = "IX_Publisher", IsUnique = true)]
public partial class Publisher
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("Publisher")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
