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
        //public JwNavbarThemeInfo Disable;
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

        public String BackHex { set { Back = value.HexToColor(); } }
        public String TopNodeBackHex { set { TopNodeBack = value.HexToColor(); } }
        public String TopNodeForeHex { set { TopNodeFore = value.HexToColor(); } }
        public String TopNodeBorderHex { set { TopNodeBorder = value.HexToColor(); } }
        public String ClildNodeBackHex { set { ClildNodeBack = value.HexToColor(); } }
        public String ChildNodeIconHex { set { ChildNodeIcon = value.HexToColor(); } }
        public String ChildNodeForeHex { set { ChildNodeFore = value.HexToColor(); } }
        public String ChildNodeBorderHex { set { ChildNodeBorder = value.HexToColor(); } }
    }

    public class JwNavbarTheme
    {
        public static Dictionary<ThemeType, JwNavbarThemeStatus> ThemeDict = new Dictionary<ThemeType, JwNavbarThemeStatus>();

        static JwNavbarTheme()
        {
            ThemeDict.Add(ThemeType.dark, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex = "#999999",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex = "#ffffff",
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex = "#999999",
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
            ThemeDict.Add(ThemeType.primary, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex = "#0866c6", TopNodeBorderHex = "#232323", TopNodeForeHex = "#ffffff",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex = "#0866c6", TopNodeBorderHex = "#232323", TopNodeForeHex = "#ffffff", 
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex = "#999999", 
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
            ThemeDict.Add(ThemeType.secondary, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#6c757d", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#6c757d", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex   = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#999999",
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
            ThemeDict.Add(ThemeType.success, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#1e7e34", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#1e7e34", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex   = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#999999",
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
            ThemeDict.Add(ThemeType.danger, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#bd2130", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#bd2130", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex   = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#999999",
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
            ThemeDict.Add(ThemeType.warning, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#d39e00", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#212529",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#d39e00", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#212529",
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex   = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#999999",
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
            ThemeDict.Add(ThemeType.info, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#117a8b", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#117a8b", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#ffffff",
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex   = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#999999",
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
            ThemeDict.Add(ThemeType.light, new JwNavbarThemeStatus()
            {
                Normal = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#f8f9fa", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#212529",
                    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                Active = new JwNavbarThemeInfo()
                {
                    BackHex = "#282828",
                    TopNodeBackHex   = "#f8f9fa", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#212529",
                    ClildNodeBackHex = "#ffffff", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                },
                //Disable = new JwNavbarThemeInfo()
                //{
                //    BackHex = "#282828",
                //    TopNodeBackHex   = "#282828", TopNodeBorderHex = "#232323", TopNodeForeHex   = "#999999",
                //    ClildNodeBackHex = "#eeeeee", ChildNodeIconHex = "#bebebe", ChildNodeForeHex = "#333333", ChildNodeBorderHex = "#dddddd",
                //},
            });
        }
    }
}
