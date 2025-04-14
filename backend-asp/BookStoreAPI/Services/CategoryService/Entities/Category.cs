using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStoreAPI.Services.BookService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services.CategoryService.Entities;

[Table("Category")]
[Index("Name", Name = "IX_Category", IsUnique = true)]
public partial class Category
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
