using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWebAppFE.Models
{
    public  class Publisher 
    {
        public Guid id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
