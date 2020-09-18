using Jw.Winform.Ctrls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Forms
{ 
    public class JwNotify
    {
        private static JwNotify manager = null;
        private Dictionary<string, JwNotifyForm> _Forms = new Dictionary<string, JwNotifyForm>();
        private JwNotify() { }
        static JwNotify()
        {
            manager = new JwNotify();
        }
        public static void Info(string message, bool autoClose =true, int delay = 5000, bool closeable = false)
        {
            manager.Show(message, ThemeType.Info, "icon-info-fill", closeable, autoClose, delay);
        }
        public static void Error(string message, bool autoClose = true, int delay = 5000, bool closeable = false)
        {
            manager.Show(message, ThemeType.Danger, "op2-reeor", closeable, autoClose, delay);
        }
        public static void Succ(string message, bool autoClose = true, int delay = 5000, bool closeable = false)
        {
            manager.Show(message, ThemeType.Success, "op2-succ", closeable, autoClose, delay);
        }
        public static void Warning(string message, bool autoClose = true, int delay = 5000, bool closeable = false)
        {
            manager.Show(message, ThemeType.Warning, "icon-warning-fill", closeable, autoClose, delay);
        }

        public string Show(string message, ThemeType theme, string icon, bool closeable, bool autoClose, int delay)
        {
            var fnotify = new JwNotifyForm
            {
                ShowCloseButton = closeable,
                Message = message,
                MessageIcon = icon,
                Theme = theme,
                AutoClose = autoClose,
                CloseDelay = delay,
                StartPosition = System.Windows.Forms.FormStartPosition.Manual
            };
            fnotify.FormClosed += FNotifyFormClosed;
            _Forms.Add(fnotify.Uuid, fnotify);
            var screen = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));
            var x = screen.WorkingArea.X + screen.WorkingArea.Width - fnotify.Width;
            var y = screen.WorkingArea.Y + screen.WorkingArea.Height - fnotify.Height;
            fnotify.Location = new Point(x, y);
            fnotify.Show();
            return fnotify.Uuid;
        }

        private void FNotifyFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (!(sender is JwNotifyForm)) return;
            var uuid = ((JwNotifyForm)sender).Uuid;
            if(_Forms.ContainsKey(uuid))
            {
                _Forms.Remove(uuid);
            }
        }
    }
}
