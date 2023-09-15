using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class ConnectedUser
	{
		public ConnectedUser(int Id, string Name, string connectionId) {
			id = Id;
			name = Name;
			connectionID = connectionId;
		}

		public int id { get; set; }
		public string name { get; set; }
		public string connectionID { get; set; }
	}
}
