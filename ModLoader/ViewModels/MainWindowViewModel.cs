using Avalonia.Media.Imaging;
using ReactiveUI.Fody.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace ModLoader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive] Bitmap? Cover { get; set; }

        public MainWindowViewModel()
        {
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
