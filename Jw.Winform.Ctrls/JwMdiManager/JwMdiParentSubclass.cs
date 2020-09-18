using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    [SuppressUnmanagedCodeSecurity]
    public class JwMdiParentSubclass : NativeWindow
    {
        private JwMdiManager _manager;

		public bool Hooked { get => _manager != null; }

        public JwMdiParentSubclass(JwMdiManager manager)
        {
            _manager = manager;
        }
		protected override void WndProc(ref Message m)
		{
			var manager = _manager;
			if (manager == null)
			{
				base.WndProc(ref m);
				return;
			}
			// TODO
			base.WndProc(ref m);
		}
		public void Unhook()
		{
			_manager = null;
		}
		public bool Activate(JwMdiManager manager, Form mdiParent)
		{
			if (mdiParent == null || manager == null)
			{
				return false;
			}
			if (this._manager == manager && mdiParent.Handle == base.Handle)
			{
				return true;
			}
			if (base.Handle != mdiParent.Handle)
			{
				return false;
			}
			this._manager = manager;
			return true;
		}
	}
}
