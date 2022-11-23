using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WebApiExample.Messages
{
    public class MessagesQueue : IMessagesQueue
    {
        private readonly ConcurrentDictionary<long, ConcurrentQueue<Message>> _queueByUser;

        public MessagesQueue()
        {
            _queueByUser = new ConcurrentDictionary<long, ConcurrentQueue<Message>>();
        }

        public void AddMessage(long userId, Message message)
        {
            var queue = _queueByUser.GetOrAdd(userId, x => new ConcurrentQueue<Message>());
            queue.Enqueue(message);
        }

        public void AddMessages(IEnumerable<long> userIds, Message message)
        {
            foreach (var userId in userIds)
                AddMessage(userId, message);
        }

        public Message GetMessage(long userId)
        {
            if (_queueByUser.TryGetValue(userId, out var queue) && queue.TryDequeue(out var message))
                return message;
            return null;
        }

		public IEnumerable<Message> GetMessages(long userId, int take)
		{
			for (int i = 0; i < take; i++)
			{
                if (_queueByUser.TryGetValue(userId, out var queue) && queue.TryDequeue(out var message))
                {
                    yield return message;
                }
            }
		}
	}
}
