using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiExample.Messages;

namespace WebApiExample.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MessagesController : Controller
	{
		private IMessagesQueue _messagesQueue;
		public MessagesController(IMessagesQueue messagesQueue)
		{
			_messagesQueue = messagesQueue;
		}

		[HttpPost]
		public IActionResult AddMessages(string subject, string body, IEnumerable<long> recipients)
		{
			_messagesQueue.AddMessages(recipients, new Message(subject,	body));
			return Ok();
		}

		[HttpGet]
		public IActionResult GetMessages(long userId)
		{
			return Ok(_messagesQueue.GetMessage(userId));
		}

		[HttpGet]
		public IActionResult GetMessages(long userId, int take)
		{
			return Ok(_messagesQueue.GetMessages(userId, take));
		}
	}
}
