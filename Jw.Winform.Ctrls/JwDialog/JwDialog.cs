using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class JwDialog
    {
        public static void AlertInfo(string message, string title = "信息") 
        {
            Alert(message, title, ThemeType.primary, "icon-info-fill");
        }
        public static void AlertSucc(string message, string title = "信息")
        {
            Alert(message, title, ThemeType.success, "op2-succ");
        }
        public static void AlertWarning(string message, string title = "警告")
        {
            Alert(message, title, ThemeType.warning, "icon-warning-fill");
        }
        public static void AlertError(string message, string title = "错误") 
        {
            Alert(message, title, ThemeType.danger, "op2-reeor");
        }

        public static void Alert(string message, string title, ThemeType theme, string icon)
        {
            using(var f = new JwAlert())
            {
                f.Theme = theme;
                f.Title = title;
                f.Message = message;
                f.TitleIcon = icon;

                f.ShowDialog();
            }
        }

        public static DialogResult Confirm(string message, string title = "请确认", ThemeType theme = ThemeType.primary, string icon = "icon-help-fill")
        {
            using (var f = new JwConfirm())
            {
                f.Theme = theme;
                f.Title = title;
                f.Message = message;
                f.TitleIcon = icon;

                return f.ShowDialog();
            }
        }
    }
}
