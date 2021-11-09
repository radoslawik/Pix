using Pix.Core.Enums;
using Pix.Core.Helpers;
using Pix.Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pix.Core.ViewModels
{
    public class ColorToolViewModel : ViewModelBase
    {
        public ICommand AddColorCommand { get; }
        [Reactive]
        public ObservableCollection<ColorModel> Colors { get; set; }
        [Reactive]
        public ColorModel? SelectedColor { get; set; }
        [Reactive]
        public bool IsUpdating { get; set; }
        [Reactive]
        public int BlockSize { get; set; }
        public bool DisplayTool => SelectedColor is not null || IsUpdating;
        public static int MinBlockSize => 1;
        public static int MaxBlockSize => 25;
        public static int MaxColorVal => 255;
        public static int MinColorVal => 0;

        private readonly MainWindowViewModel _parent;
        public delegate ColorToolViewModel Factory(MainWindowViewModel parent);
        public ColorToolViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            AddColorCommand = ReactiveCommand.Create(AddColor);
            this.WhenAnyValue(x => x.SelectedColor, x => x.IsUpdating).Subscribe(x => UpdateToolVisibility());
            this.WhenAnyValue(x => x.SelectedColor.R, x => x.SelectedColor.G, x => x.SelectedColor.B).Subscribe(async x => await UpdateSelectedColor());
        }

        public async Task UpdateSelectedColor()
        {
            IsUpdating = true;
            var selected = SelectedColor;
            var index = Colors.IndexOf(selected);
            Colors.Remove(selected);
            Colors.Insert(index, selected);
            SelectedColor = selected;
            await _parent.Pixelize();
            IsUpdating = false;
        }

        internal void UpdateColors(Palette selectedPalette)
        {
            Colors = new(ColorsProvider.GetColors(selectedPalette));
            SelectedColor = null;
        }

        private void AddColor()
        {
            Colors.Add(new ColorModel(128, 128, 128));
        }

        private void UpdateToolVisibility()
        {
            this.RaisePropertyChanged(nameof(DisplayTool));
        }
    }
}
