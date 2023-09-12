
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
	public class Animal
	{
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }

		public int age { get; set; }

		[Required]
		public int CategoryID { get; set; }
	}
}
