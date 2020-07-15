using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Ctrls
{
    public static class JwIconTheme
    {
        public static Dictionary<ThemeType, JwIconThemeItem> ThemeDict = new Dictionary<ThemeType, JwIconThemeItem>();
        static JwIconTheme()
        {
            ThemeDict.Add(ThemeType.primary, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#007bff"),
                ForeColor = ColorTranslator.FromHtml("#ffffff"),
                BordColor = ColorTranslator.FromHtml("#007bff"),
                BackActiveColor = ColorTranslator.FromHtml("#0069d9"),
                ForeActiveColor = ColorTranslator.FromHtml("#ffffff"),
                BordActiveColor = ColorTranslator.FromHtml("#0062cc"),
                BackDisableColor = ColorTranslator.FromHtml("#007bff"),
                ForeDisableColor = ColorTranslator.FromHtml("#ffffff"),
                BordDisableColor = ColorTranslator.FromHtml("#007bff")
            });
            ThemeDict.Add(ThemeType.secondary, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#6c757d"),
                ForeColor = ColorTranslator.FromHtml("#ffffff"),
                BordColor = ColorTranslator.FromHtml("#6c757d"),
                BackActiveColor = ColorTranslator.FromHtml("#5a6268"),
                ForeActiveColor = ColorTranslator.FromHtml("#ffffff"),
                BordActiveColor = ColorTranslator.FromHtml("#545b62"),
                BackDisableColor = ColorTranslator.FromHtml("#6c757d"),
                ForeDisableColor = ColorTranslator.FromHtml("#ffffff"),
                BordDisableColor = ColorTranslator.FromHtml("#6c757d")
            });
            ThemeDict.Add(ThemeType.success, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#28a745"),
                ForeColor = ColorTranslator.FromHtml("#ffffff"),
                BordColor = ColorTranslator.FromHtml("#28a745"),
                BackActiveColor = ColorTranslator.FromHtml("#218838"),
                ForeActiveColor = ColorTranslator.FromHtml("#ffffff"),
                BordActiveColor = ColorTranslator.FromHtml("#1e7e34"),
                BackDisableColor = ColorTranslator.FromHtml("#28a745"),
                ForeDisableColor = ColorTranslator.FromHtml("#ffffff"),
                BordDisableColor = ColorTranslator.FromHtml("#28a745")
            });
            ThemeDict.Add(ThemeType.danger, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#dc3545"),
                ForeColor = ColorTranslator.FromHtml("#ffffff"),
                BordColor = ColorTranslator.FromHtml("#dc3545"),
                BackActiveColor = ColorTranslator.FromHtml("#c82333"),
                ForeActiveColor = ColorTranslator.FromHtml("#ffffff"),
                BordActiveColor = ColorTranslator.FromHtml("#bd2130"),
                BackDisableColor = ColorTranslator.FromHtml("#dc3545"),
                ForeDisableColor = ColorTranslator.FromHtml("#ffffff"),
                BordDisableColor = ColorTranslator.FromHtml("#dc3545")
            });
            ThemeDict.Add(ThemeType.info, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#17a2b8"),
                ForeColor = ColorTranslator.FromHtml("#ffffff"),
                BordColor = ColorTranslator.FromHtml("#17a2b8"),
                BackActiveColor = ColorTranslator.FromHtml("#138496"),
                ForeActiveColor = ColorTranslator.FromHtml("#ffffff"),
                BordActiveColor = ColorTranslator.FromHtml("#117a8b"),
                BackDisableColor = ColorTranslator.FromHtml("#17a2b8"),
                ForeDisableColor = ColorTranslator.FromHtml("#ffffff"),
                BordDisableColor = ColorTranslator.FromHtml("#17a2b8")
            });
            ThemeDict.Add(ThemeType.warning, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#ffc107"),
                ForeColor = ColorTranslator.FromHtml("#212529"),
                BordColor = ColorTranslator.FromHtml("#ffc107"),
                BackActiveColor = ColorTranslator.FromHtml("#e0a800"),
                ForeActiveColor = ColorTranslator.FromHtml("#212529"),
                BordActiveColor = ColorTranslator.FromHtml("#d39e00"),
                BackDisableColor = ColorTranslator.FromHtml("#ffc107"),
                ForeDisableColor = ColorTranslator.FromHtml("#212529"),
                BordDisableColor = ColorTranslator.FromHtml("#ffc107")
            });
            ThemeDict.Add(ThemeType.light, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#f8f9fa"),
                ForeColor = ColorTranslator.FromHtml("#212529"),
                BordColor = ColorTranslator.FromHtml("#dae0e5"),
                BackActiveColor = ColorTranslator.FromHtml("#e2e6ea"),
                ForeActiveColor = ColorTranslator.FromHtml("#212529"),
                BordActiveColor = ColorTranslator.FromHtml("#dae0e5"),
                BackDisableColor = ColorTranslator.FromHtml("#f8f9fa"),
                ForeDisableColor = ColorTranslator.FromHtml("#212529"),
                BordDisableColor = ColorTranslator.FromHtml("#f8f9fa")
            });
            ThemeDict.Add(ThemeType.dark, new JwIconThemeItem()
            {
                BackColor = ColorTranslator.FromHtml("#343a40"),
                ForeColor = ColorTranslator.FromHtml("#ffffff"),
                BordColor = ColorTranslator.FromHtml("#343a40"),
                BackActiveColor = ColorTranslator.FromHtml("#23272b"),
                ForeActiveColor = ColorTranslator.FromHtml("#ffffff"),
                BordActiveColor = ColorTranslator.FromHtml("#1d2124"),
                BackDisableColor = ColorTranslator.FromHtml("#343a40"),
                ForeDisableColor = ColorTranslator.FromHtml("#ffffff"),
                BordDisableColor = ColorTranslator.FromHtml("#343a40")
            });
        }
    }
    public class JwIconThemeItem
    {
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Color BordColor { get; set; }

        public Color BackActiveColor { get; set; }
        public Color ForeActiveColor { get; set; }
        public Color BordActiveColor { get; set; }

        public Color BackDisableColor { get; set; }
        public Color ForeDisableColor { get; set; }
        public Color BordDisableColor { get; set; }
    }
}
