using Avalonia.FreeDesktop.DBusIme;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using DynamicData;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ModLoader.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ModLoader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Database _db;
        [Reactive] string SearchText { get; set; }
        [Reactive] bool IsBusy { get; set; }

        ObservableCollection<Mod> Mods { get; } = new();
        ObservableCollection<Mod> DelitedMods { get; } = new();

        ObservableCollection<Mod> ResultMods { get; } = new();
        ObservableCollection<Mod> ResultDelitedMods { get; } = new();

        public MainWindowViewModel(Database db)
        {
            _db = db;

           this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(0.35), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Where(x => {
                    if (!string.IsNullOrEmpty(x)) return true; else LoadMods();
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

            foreach (var item in ResultMods.ToList()) ResultMods.Remove(item);
            foreach (var item in ResultDelitedMods.ToList()) ResultDelitedMods.Remove(item);
            
            foreach (var item in Mods.Where(x => x.Name.IndexOf(search) != -1)) ResultMods.Add(item);
            foreach (var item in DelitedMods.Where(x => x.Name.IndexOf(search) != -1)) ResultDelitedMods.Add(item);

            IsBusy = false;
        }

        public void LoadMods()
        {
            foreach (var item in ResultMods.ToList()) ResultMods.Remove(item);
            foreach (var item in ResultDelitedMods.ToList()) ResultDelitedMods.Remove(item);

            foreach (var item in _db.Mods.Where(x => x.isDelited)) DelitedMods.Add(item);
            foreach (var item in _db.Mods.Where(x => !x.isDelited)) Mods.Add(item);
            foreach (var item in _db.Mods.Where(x => x.isDelited)) ResultDelitedMods.Add(item);
            foreach (var item in _db.Mods.Where(x => !x.isDelited)) ResultMods.Add(item);
        }

        public async void UpdateItemCommand(Mod item)
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
        }
    }
}
