using Microsoft.EntityFrameworkCore;
using ModLoader.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ModLoader.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
    {
        //      private Database _db;
        //      [Reactive] ViewModelBase Content { get; set; }

        //      public MainWindowViewModel(Database db)
        //{
        //	_db= db;

        //	//Content = new GamesViewModel();	
        //	Content = new ModsViewModel(_db);	
        //}

        public MainWindowViewModel()
        {
        }


    }


}