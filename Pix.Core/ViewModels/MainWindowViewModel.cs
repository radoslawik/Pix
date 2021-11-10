using Pix.Core.Enums;
using Pix.Core.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pix.Core.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IPixelService _pixelService;
        public ICommand PixelizeCommand { get; }
        public EditorViewModel Editor { get; }
        public MenuViewModel Menu { get; }
        public SettingsViewModel Settings { get; }

        [Reactive]
        public bool IsMenuOpen { get; set; }
        [Reactive]
        public int[]? PixelData { get; set; }
        [Reactive]
        public int? BitmapWidth { get; set; }
        [Reactive]
        public int? BitmapHeight { get; set; }

        public MainWindowViewModel(IPixelService pixelService, EditorViewModel.Factory colorToolFactory,
            MenuViewModel.Factory menuFactory, SettingsViewModel.Factory settingsFactory)
        {
            _pixelService = pixelService;
            Settings = settingsFactory();
            Editor = colorToolFactory(this);
            Menu = menuFactory(this);
            PixelizeCommand = ReactiveCommand.CreateFromTask(Pixelize);
            this.WhenAnyValue(x => x.Menu.SelectedPalette).Subscribe(PaletteChanged);
            this.WhenAnyValue(x => x.Editor.Colors, x => x.Editor.BlockSize, x => x.Menu.ForceAllColors, x => x.Menu.OrderByLuminance).Subscribe(async x => await Pixelize());
        }

        public async void Initialize(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                (BitmapWidth, BitmapHeight) = _pixelService.Initialize(path);
                await Pixelize();
            }
        }

        public void ToggleMenu()
        {
            IsMenuOpen ^= true;
        }

        private void PaletteChanged(Palette newPalette)
        {
            Editor.UpdateColors(newPalette);
        }

        public async Task Pixelize()
        {
            if (BitmapHeight is not null && BitmapWidth is not null)
            {
                PixelData = await _pixelService.GetPixelizedStream(Editor.BlockSize, Menu.ForceAllColors, Menu.OrderByLuminance, Editor.Colors.ToArray());
            }
        }
    }
}
