using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Ctrls
{
    public class JwCommonThemeStatus
    {
        public JwCommonThemeInfo Normal;
        public JwCommonThemeInfo Active;
        public JwCommonThemeInfo Disable;

        public JwCommonThemeStatus Clone()
        {
            var r = new JwCommonThemeStatus();
            r.Normal = Normal.Clone();
            r.Active = Active.Clone();
            r.Disable = Disable.Clone();
            return r;
        }
    }
    public class JwCommonThemeInfo
    {
        public Color Back;
        public Color Border;
        public Color Fore; 
        public JwCommonThemeInfo Clone()
        {
            var r = new JwCommonThemeInfo();
            r.Back = this.Back;
            r.Border = this.Border;
            r.Fore = this.Fore;
            return r;
        }
    }
    public static class JwCommonTheme
    {
        public static Dictionary<ThemeType, JwCommonThemeStatus> ThemeDict = new Dictionary<ThemeType, JwCommonThemeStatus>();
        static JwCommonTheme()
        {
            ThemeDict.Add(ThemeType.Primary, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back= "#007bff".HexToColor(), Border="#007bff".HexToColor(), Fore="#ffffff".HexToColor()},
                Active = new JwCommonThemeInfo() { Back = "#0069d9".HexToColor(), Border = "#0062cc".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#007bff".HexToColor(), Border = "#007bff".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Secondary, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back = "#6c757d".HexToColor(), Border = "#6c757d".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwCommonThemeInfo() { Back = "#5a6268".HexToColor(), Border = "#545b62".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#6c757d".HexToColor(), Border = "#6c757d".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Success, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back = "#28a745".HexToColor(), Border = "#28a745".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwCommonThemeInfo() { Back = "#218838".HexToColor(), Border = "#1e7e34".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#28a745".HexToColor(), Border = "#28a745".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Danger, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back = "#dc3545".HexToColor(), Border = "#dc3545".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwCommonThemeInfo() { Back = "#c82333".HexToColor(), Border = "#bd2130".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#dc3545".HexToColor(), Border = "#dc3545".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Info, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back = "#17a2b8".HexToColor(), Border = "#17a2b8".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwCommonThemeInfo() { Back = "#138496".HexToColor(), Border = "#117a8b".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#17a2b8".HexToColor(), Border = "#17a2b8".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Warning, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back = "#ffc107".HexToColor(), Border = "#ffc107".HexToColor(), Fore = "#212529".HexToColor() },
                Active = new JwCommonThemeInfo() { Back = "#e0a800".HexToColor(), Border = "#d39e00".HexToColor(), Fore = "#212529".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#ffc107".HexToColor(), Border = "#ffc107".HexToColor(), Fore = "#212529".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Light, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back = "#f8f9fa".HexToColor(), Border = "#dae0e5".HexToColor(), Fore = "#212529".HexToColor() },
                Active = new JwCommonThemeInfo() { Back = "#e2e6ea".HexToColor(), Border = "#dae0e5".HexToColor(), Fore = "#212529".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#f8f9fa".HexToColor(), Border = "#f8f9fa".HexToColor(), Fore = "#212529".HexToColor() },
            });
            ThemeDict.Add(ThemeType.Dark, new JwCommonThemeStatus()
            {
                Normal = new JwCommonThemeInfo() { Back = "#343a40".HexToColor(), Border = "#343a40".HexToColor(), Fore = "#ffffff".HexToColor() },
                Active = new JwCommonThemeInfo() { Back = "#23272b".HexToColor(), Border = "#1d2124".HexToColor(), Fore = "#ffffff".HexToColor() },
                Disable = new JwCommonThemeInfo() { Back = "#343a40".HexToColor(), Border = "#343a40".HexToColor(), Fore = "#ffffff".HexToColor() },
            });
        }
    }
}
