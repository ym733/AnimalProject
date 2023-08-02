
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class CategoryType
    {
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public string? ImagePath { get; set; }
    }
}
