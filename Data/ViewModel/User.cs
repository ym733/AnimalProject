﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string DateOfBirth { get; set; }

        [Required]
		public string Password { get; set; }

		public string? SaltHash { get; set; }

		[Required]
		public int roleID { get; set; }
    }
}
