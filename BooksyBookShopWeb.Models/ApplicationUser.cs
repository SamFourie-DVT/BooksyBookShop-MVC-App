using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BooksyBookShopWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = String.Empty;

        public string StreetAddress { get; set; } = String.Empty;

        public string City { get; set; } = String.Empty;

        public string Province { get; set; } = String.Empty;

        public string PostalCode { get; set; } = String.Empty;
    }
}
