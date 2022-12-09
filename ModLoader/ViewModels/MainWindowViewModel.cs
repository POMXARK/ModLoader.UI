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


        private Database _db;

        public ReactiveCommand<Mod, Unit> UpdateItemCommand { get; }

        [Reactive] ObservableCollection<Mod> SelectedResultMods { get; set; } = new();
        [Reactive] ObservableCollection<Mod> SelectedResultDelitedMods { get; set; } = new();

        [Reactive] ObservableCollection<Pack> Packs { get; set; } = new();

        [Reactive] public Pack SelectedPack { get; set; }


        public MainWindowViewModel(Database db)
        {
            _db = db;

            UpdateItemCommand = ReactiveCommand.Create((Mod obj) => {  }); // UpdateItem(obj);

            foreach (var item in Packs.ToList()) Packs.Remove(item);
            foreach (var item in _db.Packs.Include(x => x.Mods).ToList()) Packs.Add(item);



            this.WhenAnyValue(x => x.SelectedPack)
                .Throttle(TimeSpan.FromSeconds(0.35), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    Logger.Write(_db.ChangeTracker.DebugView.LongView);
                    FilterPack(x.Id);
                });

        }

        private async void FilterPack(uint packId)
        {
            foreach (var item in SelectedResultMods.ToList()) SelectedResultMods.Remove(item);
            foreach (var item in SelectedResultDelitedMods.ToList()) SelectedResultDelitedMods.Remove(item);

            foreach (var item in Packs.Where(x => x.Id == packId).First().Mods.Where(x => !x.isDelited)) SelectedResultMods.Add(item);
            foreach (var item in Packs.Where(x => x.Id == packId).First().Mods.Where(x => x.isDelited)) SelectedResultDelitedMods.Add(item);
        }
    }


}