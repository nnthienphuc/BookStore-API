using System.ComponentModel.DataAnnotations;

namespace BookStoreWebAppFE.Models
{
    public class Promotion
    {
        public Guid id { get; set; }
        [Required(ErrorMessage = "Không Được Để Trống !")]
        public string name { get; set; } = null!;

        public DateTime startDate { get; set; } = DateTime.Now;

        public DateTime endDate { get; set; } = DateTime.Now;

        public decimal condition { get; set; }

        public double discountPercent { get; set; }

        public int quantity { get; set; }
        public bool IsDeleted { get; set; }

    }

}
