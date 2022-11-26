using System.Collections.Generic;
using WebApiExample.Domain;

namespace WebApiExample.Dto
{
	public class GetMessagesResponse
	{
		public IEnumerable<Message> Messages { get; set; }
	}
}
