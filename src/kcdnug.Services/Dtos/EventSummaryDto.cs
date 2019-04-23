using System;
using System.Collections.Generic;
using System.Text;

namespace kcdnug.Services.Dtos
{
	public class EventSummaryDto
	{
		public Guid Id { get; set; }
		public DateTime Published { get; set; }
		public DateTime LastUpdated { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public List<string> Urls { get; set; }
		public string ThumbnailUrl { get; set; }
		public List<string> ImageUrls { get; set; }

	}
}
