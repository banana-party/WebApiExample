using System.Collections.Generic;
using WebApiExample.Domain;

namespace WebApiExample.Dal
{
	public interface IMessagesQueue
	{
		void AddMessage(long userId, Message message);
		void AddMessages(IEnumerable<long> userIds, Message message);
		Message GetMessage(long userId);
		IEnumerable<Message> GetMessages(long userId, int take);
	}
}
