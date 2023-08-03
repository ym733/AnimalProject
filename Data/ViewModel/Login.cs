using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class Login
	{
		[Required(ErrorMessage = "Field required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Field required")]
		public string Password { get; set; }

	}
}
