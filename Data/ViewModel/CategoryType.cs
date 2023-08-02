using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viewModel
{
    public class CategoryType
    {
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public IFormFile files { get; set; }
    }
}
