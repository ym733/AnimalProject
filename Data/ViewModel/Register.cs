using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class Register
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string dateOfBirth { get; set; }
		[Required]
		public string Password { get; set; }

		[Required]
		public string confirmPassword { get; set; }
	}
}
