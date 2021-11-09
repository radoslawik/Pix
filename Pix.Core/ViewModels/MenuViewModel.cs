using Pix.Core.Enums;
using Pix.Core.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pix.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ICommand OpenFileCommand { get; }
        public ICommand ToggleMenuCommand { get; }
        [Reactive]
        public string FilePath { get; set; }
        public string FileName => Path.GetFileNameWithoutExtension(FilePath);
        public Palette[] AvailablePalettes { get; }
        [Reactive]
        public Palette SelectedPalette { get; set; }
        [Reactive]
        public bool ForceAllColors { get; set; }
        [Reactive]
        public bool OrderByLuminance { get; set; }

        public static Icon MenuIcon => Icon.Menu;
        public static Icon OpenIcon => Icon.Open;
        public static Icon ImageIcon => Icon.Image;
        public static Icon PaletteIcon => Icon.Palette;
        public static Icon SettingsIcon => Icon.Settings;

        private readonly MainWindowViewModel _parent;
        private readonly IDialogService _dialogService;

        public delegate MenuViewModel Factory(MainWindowViewModel parent);
        public MenuViewModel(IDialogService dialogService, MainWindowViewModel parent)
        {
            _parent = parent;
            _dialogService = dialogService;
            AvailablePalettes = (Palette[])Enum.GetValues(typeof(Palette));
            OpenFileCommand = ReactiveCommand.CreateFromTask(OpenFile);
            ToggleMenuCommand = ReactiveCommand.Create(_parent.ToggleMenu);
        }

        private async Task OpenFile()
        {
            FilePath = await _dialogService.ShowOpenFileDialog("Choose file", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            _parent.Initialize(FilePath);
            this.RaisePropertyChanged(nameof(FileName));
        }

    }
}
