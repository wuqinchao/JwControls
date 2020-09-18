using Jw.Winform.Ctrls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Forms
{
    public class JwAlert
    {
        public static void Info(string message, string title = "信息") 
        {
            Alert(message, title, ThemeType.Primary, "icon-info-fill");
        }
        public static void Succ(string message, string title = "信息")
        {
            Alert(message, title, ThemeType.Success, "op2-succ");
        }
        public static void Warning(string message, string title = "警告")
        {
            Alert(message, title, ThemeType.Warning, "icon-warning-fill");
        }
        public static void Error(string message, string title = "错误") 
        {
            Alert(message, title, ThemeType.Danger, "op2-reeor");
        }

        public static void Alert(string message, string title, ThemeType theme, string icon)
        {
            using(var f = new JwAlertForm())
            {
                f.Theme = theme;
                f.Title = title;
                f.Message = message;
                f.TitleIcon = icon;

                f.ShowDialog();
            }
        }

        public static DialogResult Confirm(string message, string title = "请确认", ThemeType theme = ThemeType.Primary, string icon = "icon-help-fill")
        {
            using (var f = new JwConfirmForm())
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
