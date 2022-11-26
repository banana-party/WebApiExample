using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
		public IActionResult AddMessage([FromBody][Required] AddMessageRequest request)
		{
			_messagesQueue.AddMessage(request.Recipients, new Message(request.Subject, request.Body));
			return Created(string.Empty, request);
		}

		[HttpPost("AddMessages")]
		public IActionResult AddMessages([FromBody][Required] AddMessagesRequest request)
		{
			_messagesQueue.AddMessages(request.Recipients, request.Messages);
			return Created(string.Empty, request);
		}

		[HttpGet("GetMessage")]
		public IActionResult GetMessage(long userId)
		{
			var message = new GetMessageResponse() { Message = _messagesQueue.GetMessage(userId) };
			return message.Message != null ? Ok(message) : NotFound("There are no messages for this user");
		}

		[HttpGet("GetMessages")]
		public IActionResult GetMessages(long userId, int take)
		{
			var messages = new GetMessagesResponse() { Messages = _messagesQueue.GetMessages(userId, take) };
			return messages.Messages.Any() ? Ok(messages) : NotFound("There are no messages for this user");
		}
	}
}
