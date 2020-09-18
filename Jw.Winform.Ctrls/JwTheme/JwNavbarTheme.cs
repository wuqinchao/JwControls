using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Ctrls
{
    public class JwNavbarThemeStatus
    {
        public JwNavbarThemeInfo Normal;
        public JwNavbarThemeInfo Active;

        public JwNavbarThemeStatus Clone()
        {
            var r = new JwNavbarThemeStatus();
            r.Normal = Normal.Clone();
            r.Active = Active.Clone();
            return r;
        }
    }

    public class JwNavbarThemeInfo
    {
        public Color Back;
        public Color TopNodeBack;
        public Color TopNodeFore;
        public Color TopNodeBorder;
        public Color ClildNodeBack;
        public Color ChildNodeIcon;
        public Color ChildNodeFore;
        public Color ChildNodeBorder;
        public JwNavbarThemeInfo Clone()
        {
            var r = new JwNavbarThemeInfo();
            r.Back = this.Back;
            r.TopNodeBack = this.TopNodeBack;
            r.TopNodeFore = this.TopNodeFore;
            r.TopNodeBorder = this.TopNodeBorder;
            r.ClildNodeBack = this.ClildNodeBack;
            r.ChildNodeIcon = this.ChildNodeIcon;
            r.ChildNodeFore = this.ChildNodeFore;
            r.ChildNodeBorder = this.ChildNodeBorder;
            return r;
        }
    }

    public class JwNavbarTheme
    {
        public static Dictionary<ThemeType, JwNavbarThemeStatus> ThemeDict = new Dictionary<ThemeType, JwNavbarThemeStatus>();

        static JwNavbarTheme()
        {
            ThemeDict.Add(ThemeType.Dark, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack = "#282828".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore = "#999999".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack = "#282828".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore = "#ffffff".HexToColor(),
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
            ThemeDict.Add(ThemeType.Primary, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack = "#0866c6".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore = "#ffffff".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack = "#0866c6".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore = "#ffffff".HexToColor(), 
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
            ThemeDict.Add(ThemeType.Secondary, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#6c757d".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#6c757d".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
            ThemeDict.Add(ThemeType.Success, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#1e7e34".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#1e7e34".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
            ThemeDict.Add(ThemeType.Danger, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#bd2130".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#bd2130".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
            ThemeDict.Add(ThemeType.Warning, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#d39e00".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#212529".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#d39e00".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#212529".HexToColor(),
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
            ThemeDict.Add(ThemeType.Info, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#117a8b".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#117a8b".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#ffffff".HexToColor(),
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
            ThemeDict.Add(ThemeType.Light, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#f8f9fa".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#212529".HexToColor(),
                    ClildNodeBack = "#eeeeee".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
                Active = new JwNavbarThemeInfo()
                {
                    Back = "#282828".HexToColor(),
                    TopNodeBack   = "#f8f9fa".HexToColor(), TopNodeBorder = "#232323".HexToColor(), TopNodeFore   = "#212529".HexToColor(),
                    ClildNodeBack = "#ffffff".HexToColor(), ChildNodeIcon = "#bebebe".HexToColor(), ChildNodeFore = "#333333".HexToColor(), ChildNodeBorder = "#dddddd".HexToColor(),
                },
            });
        }
    }
}
