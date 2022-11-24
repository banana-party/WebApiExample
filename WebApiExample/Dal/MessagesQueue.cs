using System.Collections.Concurrent;
using System.Collections.Generic;
using WebApiExample.Domain;

namespace WebApiExample.Dal
{
	public class MessagesQueue : IMessagesQueue
	{
		private readonly ConcurrentDictionary<long, ConcurrentQueue<Message>> _queueByUser;

		public MessagesQueue()
		{
			_queueByUser = new ConcurrentDictionary<long, ConcurrentQueue<Message>>();
		}
		private void AddMessage(long userId, Message message)
		{
			var queue = _queueByUser.GetOrAdd(userId, x => new ConcurrentQueue<Message>());
			queue.Enqueue(message);
		}

		public void AddMessage(IEnumerable<long> userIds, Message message)
		{
			foreach (var userId in userIds)
				AddMessage(userId, message);
		}

		public void AddMessages(IEnumerable<long> userIds, IEnumerable<Message> messages)
		{
			foreach (var message in messages)
			{
				AddMessage(userIds, message);
			}
		}

		public Message GetMessage(long userId)
		{
			if (_queueByUser.TryGetValue(userId, out var queue) && queue.TryDequeue(out var message))
				return message;
			return null;
		}

		public IEnumerable<Message> GetMessages(long userId, int take)
		{
			if (_queueByUser.TryGetValue(userId, out var queue))			
				for (int i = 0; i < take; i++)
					if (queue.TryDequeue(out var message))					
						yield return message;			
		}
	}
}
