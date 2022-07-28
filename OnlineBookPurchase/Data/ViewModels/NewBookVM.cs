using OnlineBookPurchase.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace OnlineBookPurchase.Models
{
    public class NewBookVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [Display(Description = "Image name")]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Description = "Name")]
        public string Name { get; set;}

        [Required(ErrorMessage = "Price is required")]
        [Display(Description = "Price name")]
        public double Price { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [Display(Description = "Description name")]
        public string Description { get; set; }

        [Required(ErrorMessage = "BookCategory is required")]
        [Display(Description = "BookCategory name")]
        public Data.BookCategory BookCategory { get; set; }


        // Relationship
        [Required(ErrorMessage = "Writer(s) is required")]
        [Display(Description = "Writer(s) name")]
        public List<int> WriterIds { get; set; }

        [Required(ErrorMessage = "Publication is required")]
        [Display(Description = "Publication name")]
        public int PublicationId { get; set; }
    }
}
