using OnlineBookPurchase.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace OnlineBookPurchase.Models
{
    public class Book:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Book Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Name { get; set;}

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Description { get; set; }

        public Data.BookCategory BookCategory { get; set; }


        // Relationship

        public List<Book_Writer> Book_Writer { get; set; }


        // Publications

        public int PublicationId { get; set; }
        [ForeignKey("PublicationId")]

        public Publications Publications { get; set; }
    }
}
