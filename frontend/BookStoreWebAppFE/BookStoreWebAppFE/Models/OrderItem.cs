using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWebAppFE.Models
{
    public  class OrderItem
    {
        public Guid OrderId { get; set; }

        public Guid BookId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

    }

}
