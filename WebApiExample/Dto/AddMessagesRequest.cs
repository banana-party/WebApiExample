﻿using System.Collections.Generic;

namespace WebApiExample.Dto
{
	public class AddMessagesRequest
	{
		public string Subject { get; set;}
		public string Body { get; set; }
		public IEnumerable<long> Recipients { get; set; }
	}
}
