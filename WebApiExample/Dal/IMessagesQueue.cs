using System.Collections.Generic;
using WebApiExample.Domain;

namespace WebApiExample.Dal
{
	public interface IMessagesQueue
	{
		void AddMessage(IEnumerable<long> userIds, Message messages);
		void AddMessages(IEnumerable<long> userIds, IEnumerable<Message> messages);
		Message GetMessage(long userId);
		IEnumerable<Message> GetMessages(long userId, int take);
	}
}
