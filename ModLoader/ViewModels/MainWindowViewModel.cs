using Avalonia.Media.Imaging;
using ModLoader.Models;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace ModLoader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Database _db;

        [Reactive] Bitmap? Cover { get; set; }

        ObservableCollection<Mod> Mods { get; } = new();

        public MainWindowViewModel(Database db)
        {
            _db = db;

            foreach (var item in _db.Mods)
            {
                Mods.Add(item);
            }



            Task.Run(() => LoadCover());

        }


        public async Task<Stream> LoadCoverBitmapAsync()
        {
           return File.OpenRead("C:\\test.png");
        }

        public async Task LoadCover()
        {
            await using (var imageStream = await LoadCoverBitmapAsync())
            {
                Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
            }
        }
    }
}
