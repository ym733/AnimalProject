﻿using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
	public class Login
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }

	}
}
