using AutoMapper;
using kcdnug.Mobile.Repositories.Models;
using kcdnug.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace kcdnug.Mobile.Repositories
{
	public static class AutoMapperInitialize
	{
		public static void Initialize()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<EventSummaryDto, EventSummary>());
		}
	}
}
