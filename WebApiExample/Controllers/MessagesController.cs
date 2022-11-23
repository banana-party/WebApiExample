using Microsoft.AspNetCore.Mvc;
using WebApiExample.Dal;
using WebApiExample.Domain;
using WebApiExample.Dto;

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
		public IActionResult AddMessages([FromBody] AddMessagesRequest request)
		{
			_messagesQueue.AddMessages(request.Recipients, new Message(request.Subject, request.Body));
			return Created(string.Empty, request);
		}

		[HttpGet]
		public IActionResult GetMessages(long userId)
		{
			return Ok(_messagesQueue.GetMessage(userId));
		}
	}
}
