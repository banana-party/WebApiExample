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

		[HttpPost("AddMessage")]
		public IActionResult AddMessage([FromBody] AddMessageRequest request)
		{
			if (request is null || request.Body is null || request.Recipients is null || request.Subject is null)
				return BadRequest();

			_messagesQueue.AddMessage(request.Recipients, new Message(request.Subject, request.Body));
			return Created(string.Empty, request);
		}

		[HttpPost("AddMessages")]
		public IActionResult AddMessages([FromBody] AddMessagesRequest request)
		{
			if (request is null || request.Messages is null || request.Recipients is null)
				return BadRequest();

			_messagesQueue.AddMessages(request.Recipients, request.Messages);
			return Created(string.Empty, request);
		}

		[HttpGet("GetMessage")]
		public IActionResult GetMessage(long userId)
		{
			return Ok(_messagesQueue.GetMessage(userId));
		}

		[HttpGet("GetMessages")]
		public IActionResult GetMessages(long userId, int take)
		{
			return Ok(_messagesQueue.GetMessages(userId, take));
		}
	}
}
