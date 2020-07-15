using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Ctrls
{
    public static class JwTheme
    {
        public static Dictionary<ThemeType, JwIconThemeItem> JwIconThemeDict
        {
            get => JwIconTheme.ThemeDict;
        }

        public static Dictionary<ThemeType, JwNavbarThemeStatus> JwNavbarThemeDict
        {
            get => JwNavbarTheme.ThemeDict;
        }

        static JwTheme()
        {
        }

        public static Color HexToColor(this string input)
        {
            return ColorTranslator.FromHtml(input);
        }
    }
}
