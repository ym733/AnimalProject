using System.Data.SqlClient;

namespace AnimalProvider
{
	public class Message: Core.Disposable
	{
		public List<Entities.Message> getGlobalMessages()
		{
			using var DAL = new DataAccess.DataAccessLayer();
			return DAL.ExecuteReader<Entities.Message>("Animal.spGetGlobalMessages");
		}

		public List<Entities.PrivateMessage> getPrivateMessages(int user1ID, int user2ID)
		{
			using var DAL = new DataAccess.DataAccessLayer();
			DAL.Parameters = new List<SqlParameter>
				{
					new SqlParameter{ ParameterName = "@user1id", Value = user1ID },
					new SqlParameter{ ParameterName = "@user2id", Value = user2ID }

			};

			return DAL.ExecuteReader<Entities.PrivateMessage>("Animal.spGetPrivateMessages");
		}

		public  bool sendGlobalMessage(int senderID, string text)
		{
			using var DAL = new DataAccess.DataAccessLayer();
			DAL.Parameters = new List<SqlParameter> {
				new SqlParameter{ ParameterName = "@senderid", Value =  senderID },
				new SqlParameter{ ParameterName = "@text", Value =  text }

			};

			return DAL.ExecuteNonQuery("Animal.spSendGlobalMessage");
		}

		public bool sendPrivateMessage(int senderID, int receiverID, string text)
		{
			using var DAL = new DataAccess.DataAccessLayer();
			DAL.Parameters = new List<SqlParameter> {
				new SqlParameter{ ParameterName = "@senderid", Value =  senderID },
				new SqlParameter{ ParameterName = "@receiverid", Value =  receiverID },
				new SqlParameter{ ParameterName = "@text", Value =  text }

			};

			return DAL.ExecuteNonQuery("Animal.spSendPrivateMessage");
		}
	}
}
