using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksyBookShopWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        [Required]
        public string  ISBN { get; set; } = String.Empty;

        [Required]
        public string  Author { get; set; } = String.Empty;

        [Required]
        [Display(Name = "List Price")]
        [Range(1,10000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50-99")]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        [Required]
        [ValidateNever]
        public string ImageUrl { get; set; } = String.Empty;

        //FK
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        //FK
        [Required]
        [Display(Name ="Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
