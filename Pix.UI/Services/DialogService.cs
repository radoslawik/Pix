using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Pix.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pix.UI.Services
{
    public class DialogService : IDialogService
    {
        public async Task<string> ShowOpenFileDialog(string title, string directory)
        {
            var path = string.Empty;
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                && desktop.MainWindow is not null)
            {
                var extensions = new List<string>() { "jpg", "png", "bmp" };
                var filters = new List<FileDialogFilter>() {
                    new FileDialogFilter { Name = "Images", Extensions = extensions }
                };
                var dialog = new OpenFileDialog
                {
                    Title = title,
                    Directory = directory,
                    Filters = filters,
                    AllowMultiple = false,
                };

                var results = await dialog.ShowAsync(desktop.MainWindow);
                path = results.FirstOrDefault() ?? path;
            }
            return path;
        }
    }
}
