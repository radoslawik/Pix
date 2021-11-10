using Pix.Core.Enums;
using Pix.Core.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pix.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        [Reactive]
        public Theme CurrentTheme { get; set; }
        public IReadOnlyCollection<Theme> Themes { get; }
        public delegate SettingsViewModel Factory();
        public SettingsViewModel(IThemeService themeService)
        {
            Themes = Enum.GetValues<Theme>();
            this.WhenAnyValue(x => x.CurrentTheme).Subscribe(themeService.SetTheme);
            CurrentTheme = Themes.FirstOrDefault();
        }
    }
}
