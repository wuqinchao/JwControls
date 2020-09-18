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
        public static Dictionary<ThemeType, JwLabelThemeStatus> JwLabelThemeDict
        {
            get => JwLabelTheme.ThemeDict;
        }
        public static Dictionary<ThemeType, JwCommonThemeStatus> JwCommonThemeDict
        {
            get => JwCommonTheme.ThemeDict;
        }
        public static Dictionary<ThemeType, JwSilderBarThemeStatus> JwSilderBarThemeDict
        {
            get => JwSilderBarTheme.ThemeDict;
        }
        public static Dictionary<ThemeType, JwIconThemeStatus> JwIconThemeDict
        {
            get => JwIconTheme.ThemeDict;
        }
        public static Dictionary<ThemeType, JwButtonThemeStatus> JwButtonThemeDict
        {
            get => JwButtonTheme.ThemeDict;
        }
        public static Dictionary<ThemeType, JwNavbarThemeStatus> JwNavbarThemeDict
        {
            get => JwNavbarTheme.ThemeDict;
        }
        public static Dictionary<ThemeType, JwDialogThemeStatus> JwDialogThemeDict
        {
            get => JwDialogTheme.ThemeDict;
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
