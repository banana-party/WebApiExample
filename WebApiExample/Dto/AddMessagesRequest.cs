using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiExample.Domain;

namespace WebApiExample.Dto
{
	public class AddMessagesRequest
	{ 
		[Required]
		public IEnumerable<Message> Messages { get; set; }
		[Required]
		public IEnumerable<long> Recipients { get; set; }
	}
}
