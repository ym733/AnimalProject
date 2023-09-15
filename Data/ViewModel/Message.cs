
namespace ViewModel
{
	public class Message
	{
		public int msgID { get; set; }
		public int senderID { get; set; }
		public bool isGlobal { get; set; }
		public string timesent { get; set; }
		public int receiverID { get; set; }
		public string text { get; set; }
	}
}
