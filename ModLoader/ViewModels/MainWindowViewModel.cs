using System;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ModLoader.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
    {
        private Database _db;
        [Reactive] ViewModelBase Content { get; set; }

        public MainWindowViewModel(Database db)
		{
			_db= db;

			Content = new ModsViewModel(_db);	
		}
	}
}