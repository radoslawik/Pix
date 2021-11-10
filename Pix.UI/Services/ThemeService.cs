using Avalonia.Markup.Xaml.Styling;
using Pix.Core.Enums;
using Pix.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Pix.UI.Services
{
    public class ThemeService : IThemeService
    {
        private readonly Dictionary<Theme,StyleInclude> _styles;
        public ThemeService()
        {
            _styles = new();
            _styles[Theme.Original] = CreateStyle("avares://Pix.UI/Themes/OriginalTheme.axaml");
            _styles[Theme.Light] = CreateStyle("avares://Pix.UI/Themes/LightTheme.axaml");
            _styles[Theme.Dark] = CreateStyle("avares://Pix.UI/Themes/DarkTheme.axaml");
        }
        public void SetTheme(Theme newTheme)
        {
            if (Avalonia.Application.Current.Styles.Count != 0)
            {
                Avalonia.Application.Current.Styles[0] = _styles[newTheme];
            }
        }

        private static StyleInclude CreateStyle(string url)
        {
            var self = new Uri("resm:Styles?assembly=Pix.UI");
            return new StyleInclude(self)
            {
                Source = new Uri(url)
            };
        }
    }
}
