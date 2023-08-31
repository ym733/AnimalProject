using System.ComponentModel.DataAnnotations;

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
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string confirmPassword { get; set; }
	}
}
