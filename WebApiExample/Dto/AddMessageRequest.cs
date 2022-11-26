using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Dto
{
	public class AddMessageRequest
	{
		[Required]
		public string Subject { get; set; }
		[Required]
		public string Body { get; set; }
		[Required]
		public IEnumerable<long> Recipients { get; set; }
	}
}
