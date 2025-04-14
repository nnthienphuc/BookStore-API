using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStoreAPI.Services.BookService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services.OrderService.Entities;

[PrimaryKey("OrderId", "BookId")]
[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    [Column("orderId")]
    public Guid OrderId { get; set; }

    [Key]
    [Column("bookId")]
    public Guid BookId { get; set; }

    [Column("quantity")]
    public short Quantity { get; set; }

    [Column("price", TypeName = "decimal(8, 0)")]
    public decimal Price { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("OrderItems")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;
}
