using System.ComponentModel.DataAnnotations.Schema;
using BookStoreWebAppFE.Components.Helper;

namespace BookStoreWebAppFE.Models
{
    public partial class Order 
    {
        public Guid id { get; set; }
        public Guid StaffId { get; set; }
        public Guid CustomerId { get; set; }

        public Guid? PromotionId { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public decimal SubTotalAmount { get; set; }

        public decimal PromotionAmount { get; set; }

        public bool Status { get; set; }

        public string? Note { get; set; }
        public string StaffName { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string? PromotionName { get; set; }
        public  List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public bool IsDeleted { get; set; } = false;
    }

}
