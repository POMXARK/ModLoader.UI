using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using ModLoader.Models;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModLoader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Database _db;

        ObservableCollection<Mod> Mods { get; } = new();

        public MainWindowViewModel(Database db)
        {
            _db = db;

             LoadMods();
        }


        public void LoadMods()
        {
            foreach (var item in _db.Mods.Where(x => x.isDelited == false))
            {
                Mods.Add(item);
            }
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

            LoadMods();
        }
    }
}
