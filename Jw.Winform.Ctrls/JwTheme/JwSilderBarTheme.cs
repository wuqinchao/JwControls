using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Ctrls
{
    [Serializable]
    public class JwSilderBarThemeStatus
    {
        public JwSilderBarThemeInfo Normal { get; set; }
        public JwSilderBarThemeInfo Active { get; set; }
        public JwSilderBarThemeInfo Disable { get; set; }

        public JwSilderBarThemeStatus Clone()
        {
            var r = new JwSilderBarThemeStatus();
            r.Normal = Normal.Clone();
            r.Active = Active.Clone();
            r.Disable = Disable.Clone();
            return r;
        }
    }
    [Serializable]
    public class JwSilderBarThemeInfo
    {
        public Color Line { get; set; }
        public Color Value { get; set; }
        public Color Thumb { get; set; }
        public Color ThumbBorder { get; set; }
        public JwSilderBarThemeInfo Clone()
        {
            var r = new JwSilderBarThemeInfo();
            r.Line = this.Line;
            r.Value = this.Value;
            r.Thumb = this.Thumb;
            r.ThumbBorder = this.ThumbBorder;
            return r;
        }
    }
    public static class JwSilderBarTheme
    {
        public static Dictionary<ThemeType, JwSilderBarThemeStatus> ThemeDict = new Dictionary<ThemeType, JwSilderBarThemeStatus>();
        static JwSilderBarTheme()
        {
            ThemeDict.Add(ThemeType.Primary, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#007bff".HexToColor(), Thumb ="#ffffff".HexToColor(), ThumbBorder= "#007bff".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#007bff".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#007bff".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#007bff".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#007bff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Secondary, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#6c757d".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#6c757d".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#6c757d".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#6c757d".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#6c757d".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#6c757d".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Success, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#28a745".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#28a745".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#28a745".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#28a745".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#28a745".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#28a745".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Danger, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#dc3545".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#dc3545".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#dc3545".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#dc3545".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#dc3545".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#dc3545".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Info, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#17a2b8".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#17a2b8".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#17a2b8".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#17a2b8".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#17a2b8".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#17a2b8".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Warning, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#ffc107".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#ffc107".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#ffc107".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#ffc107".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#ffc107".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#ffc107".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Light, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#f8f9fa".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#f8f9fa".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#f8f9fa".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#f8f9fa".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#f8f9fa".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#f8f9fa".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Dark, new JwSilderBarThemeStatus()
            {
                Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#343a40".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#343a40".HexToColor() },
                Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#343a40".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#343a40".HexToColor() },
                Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#343a40".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#343a40".HexToColor() },
            });
        }
    }
}
