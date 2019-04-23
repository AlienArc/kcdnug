using kcdnug.Mobile.Repositories;
using kcdnug.Mobile.Repositories.Models;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;

namespace kcdnug.Mobile.ViewModels
{
	public class EventsPageViewModel : BindableBase
	{
		private List<EventSummary> events;

		public EventsPageViewModel(IEventsRepository eventsRepository)
		{
			EventsRepository = eventsRepository;
			LoadEventsCommand = new DelegateCommand(LoadEventsCommandExecute);
		}

		private async void LoadEventsCommandExecute()
		{

			var events = await EventsRepository.GetUpcomingEvents();

			Events = events.Data.ToList();

		}

		public List<EventSummary> Events
		{
			get => events;
			private set => SetProperty(ref events, value);
		}

		private IEventsRepository EventsRepository { get; }

		public ICommand LoadEventsCommand { get; }
	}
}
