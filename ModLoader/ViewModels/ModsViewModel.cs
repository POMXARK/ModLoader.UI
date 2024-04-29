
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
    public class ModsViewModel : ViewModelBase
    {
        private Database _db;

        [Reactive] string SearchText { get; set; }
        [Reactive] bool IsBusy { get; set; }
        [Reactive] ObservableCollection<Mod> Mods { get; set; } = new();

        [Reactive] ObservableCollection<Mod> ResultMods { get; set; } = new();
        [Reactive] ObservableCollection<Mod> ResultDelitedMods { get; set; } = new();

        [Reactive] ObservableCollection<Mod> DelitedMods { get; set; } = new();

        public ReactiveCommand<Mod, Unit> UpdateItemCommand { get; }

        [Reactive] ObservableCollection<Mod> SelectedResultMods { get; set; } = new();
        [Reactive] ObservableCollection<Mod> SelectedResultDelitedMods { get; set; } = new();

        [Reactive] ObservableCollection<Pack> Packs { get; set; } = new();

        [Reactive] public Pack SelectedPack { get; set; }

        public ModsViewModel(Database db)
        {
            _db = db;

            UpdateItemCommand = ReactiveCommand.Create((Mod obj) => { UpdateItem(obj); });

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

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(0.35), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Where(x =>
                {
                    if (!string.IsNullOrEmpty(x)) return true;
                    
                    LoadMods();
                    if (SelectedPack != null) FilterPack(SelectedPack.Id);
                    return false;
                })
                .Subscribe(search => {
                    Debug.WriteLine(search);
                    DoSearch(search);
                });
        }

        private async void DoSearch(string search)
        {
            IsBusy = true;

            foreach (var item in SelectedResultMods.ToList()) SelectedResultMods.Remove(item);
            foreach (var item in SelectedResultDelitedMods.ToList()) SelectedResultDelitedMods.Remove(item);
            
            foreach (var item in Mods.Where(x => x.Name.IndexOf(search) != -1)) SelectedResultMods.Add(item);
            foreach (var item in DelitedMods.Where(x => x.Name.IndexOf(search) != -1)) SelectedResultDelitedMods.Add(item);

            IsBusy = false;
        }

        public void LoadMods()
        {

            foreach (var item in ResultMods.ToList()) ResultMods.Remove(item);
            foreach (var item in ResultDelitedMods.ToList()) ResultDelitedMods.Remove(item);

            foreach (var item in Mods.ToList()) Mods.Remove(item);
            foreach (var item in DelitedMods.ToList()) DelitedMods.Remove(item);

            var ModPack = _db.Mods.Include(x => x.Packs).ToList();
            
            foreach (var item in ModPack.Where(x => x.isDelited)) DelitedMods.Add(item);
            foreach (var item in ModPack.Where(x => !x.isDelited)) Mods.Add(item);
            foreach (var item in ModPack.Where(x => x.isDelited)) ResultDelitedMods.Add(item);
            foreach (var item in ModPack.Where(x => !x.isDelited)) ResultMods.Add(item);
        }

        public async void UpdateItem(Mod item)
        {
            await Task.Run(() =>
            {
                using (var context = new Database())
                {
                    //Thread.Sleep(1500); // test async
                    item.isDelited = !item.isDelited;
                    context.Mods.Attach(item);
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChangesAsync();
                }

            });

            Mods.Clear();
            DelitedMods.Clear();
            LoadMods();
            FilterPack(SelectedPack.Id);
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
