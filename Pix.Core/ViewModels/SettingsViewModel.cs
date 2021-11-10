using Pix.Core.Enums;
using Pix.Core.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pix.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; }
        [Reactive]
        public Theme CurrentTheme { get; set; }
        public IReadOnlyCollection<Theme> Themes { get; }
        public delegate SettingsViewModel Factory();

        private readonly IPixelService _pixelService;
        private readonly IDialogService _dialogService;
        public SettingsViewModel(IThemeService themeService, IPixelService pixelService, IDialogService dialogService)
        {
            _pixelService = pixelService;
            _dialogService = dialogService;
            SaveCommand = ReactiveCommand.CreateFromTask(Save);
            Themes = Enum.GetValues<Theme>();
            this.WhenAnyValue(x => x.CurrentTheme).Subscribe(themeService.SetTheme);
            CurrentTheme = Themes.FirstOrDefault();
        }

        private async Task Save()
        {
            var file = await _dialogService.ShowSaveFileDialog("Save image", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "out");
            _pixelService.SaveBitmap(file);
        }
    }
}
