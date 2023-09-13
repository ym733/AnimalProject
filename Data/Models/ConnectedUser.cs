using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class ConnectedUser
	{
		public string ConnectionId;
		public string Name;

		public ConnectedUser(string connectionId, string name)
		{
			ConnectionId = connectionId;
			Name = name;
		}
	}
}
