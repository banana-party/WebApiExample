﻿using System.Collections.Generic;
using WebApiExample.Domain;

namespace WebApiExample.Dto
{
	public class AddMessagesRequest
	{
		public IEnumerable<Message> Messages { get; set; }
		public IEnumerable<long> Recipients { get; set; }
	}
}
