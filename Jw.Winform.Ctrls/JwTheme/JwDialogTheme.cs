using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Ctrls
{
    public class JwDialogThemeStatus
    {
        public JwDialogThemeInfo Normal;
        public JwDialogThemeInfo Active;
        public JwDialogThemeInfo Disable;

        public JwDialogThemeStatus Clone()
        {
            var r = new JwDialogThemeStatus();
            r.Normal = Normal.Clone();
            r.Active = Active.Clone();
            r.Disable = Disable.Clone();
            return r;
        }
    }
    public class JwDialogThemeInfo
    {
        public Color Back;
        public Color Border;
        public Color Fore;
        public JwDialogThemeInfo Clone()
        {
            var r = new JwDialogThemeInfo();
            r.Back = this.Back;
            r.Border = this.Border;
            r.Fore = this.Fore;
            return r;
        }
    }
    public static class JwDialogTheme
    {
        public static Dictionary<ThemeType, JwDialogThemeStatus> ThemeDict = new Dictionary<ThemeType, JwDialogThemeStatus>();
        static JwDialogTheme()
        {
            ThemeDict.Add(ThemeType.Primary, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back= "#007bff".HexToColor(), Border="#007bff".HexToColor(), Fore="#ffffff".HexToColor()},
                Active = new JwDialogThemeInfo() { Back = "#0069d9".HexToColor(), Border = "#0062cc".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#007bff".HexToColor(), Border = "#007bff".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Secondary, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back = "#6c757d".HexToColor(), Border = "#6c757d".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwDialogThemeInfo() { Back = "#5a6268".HexToColor(), Border = "#545b62".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#6c757d".HexToColor(), Border = "#6c757d".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Success, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back = "#28a745".HexToColor(), Border = "#28a745".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwDialogThemeInfo() { Back = "#218838".HexToColor(), Border = "#1e7e34".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#28a745".HexToColor(), Border = "#28a745".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Danger, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back = "#dc3545".HexToColor(), Border = "#dc3545".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwDialogThemeInfo() { Back = "#c82333".HexToColor(), Border = "#bd2130".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#dc3545".HexToColor(), Border = "#dc3545".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Info, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back = "#17a2b8".HexToColor(), Border = "#17a2b8".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwDialogThemeInfo() { Back = "#138496".HexToColor(), Border = "#117a8b".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#17a2b8".HexToColor(), Border = "#17a2b8".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Warning, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back = "#ffc107".HexToColor(), Border = "#ffc107".HexToColor(), Fore = "#212529".HexToColor() },
                Active = new JwDialogThemeInfo() { Back = "#e0a800".HexToColor(), Border = "#d39e00".HexToColor(), Fore = "#212529".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#ffc107".HexToColor(), Border = "#ffc107".HexToColor(), Fore = "#212529".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Light, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back = "#f8f9fa".HexToColor(), Border = "#dae0e5".HexToColor(), Fore = "#212529".HexToColor() },
                Active = new JwDialogThemeInfo() { Back = "#e2e6ea".HexToColor(), Border = "#dae0e5".HexToColor(), Fore = "#212529".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#f8f9fa".HexToColor(), Border = "#f8f9fa".HexToColor(), Fore = "#212529".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Dark, new JwDialogThemeStatus()
            {
                Normal = new JwDialogThemeInfo() { Back = "#343a40".HexToColor(), Border = "#343a40".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwDialogThemeInfo() { Back = "#23272b".HexToColor(), Border = "#1d2124".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwDialogThemeInfo() { Back = "#343a40".HexToColor(), Border = "#343a40".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
        }
    }
}
