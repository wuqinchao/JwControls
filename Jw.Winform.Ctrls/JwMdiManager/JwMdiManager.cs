using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class JwMdiManager : Component
    {
        private IContainer container;
        private Form _MdiParent;
        private bool _IsBound = false;
		private MdiClient _MdiClient;
		private JwMdiParentSubclass mdiParentSubclasser;
		public JwMdiManager()
        {
        }
        public JwMdiManager(IContainer container)
        {
            this.container = container;
            if (this.container != null)
            {
                this.container.Add(this);
            }
        }
        [Browsable(false)]
        public Form MdiParent 
		{ 
			get => _MdiParent;
			set
			{
                if (!_IsBound)
                {

					_MdiParent = value;
					BindMdiParent();

				}
			}
		}
		public bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		public void UnBindMdiParent()
        {
			if (!_IsBound || MdiParent == null) return;
			MdiParent.ControlAdded -= OnMdiParentFormControlAdded;
			MdiParent.ControlRemoved -= OnMdiParentFormControlRemoved;
			MdiParent.MdiChildActivate -= OnMdiParentMdiChildActivated;
			_IsBound = false;
			_MdiParent = null;

		}
        public void BindMdiParent()
        {
            if (base.DesignMode || _MdiParent == null /*|| IsInitializing*/)
            {
                return;
            }
            //EnsureOnlyOneTabbedMdiManager(mdiParent);
            if (!_IsBound)
			{ 
				//MdiParentManager.RegisterTabbedMdiManager(this, mdiParent);
				MdiParent.ControlAdded += OnMdiParentFormControlAdded;
				MdiParent.ControlRemoved += OnMdiParentFormControlRemoved;
				MdiParent.MdiChildActivate += OnMdiParentMdiChildActivated;
				_IsBound = true;
				if (!MdiParent.IsHandleCreated)
				{
					MdiParent.HandleCreated += OnMdiParentHandleCreated;
				}
				else
				{
					SubclassMdiParent(MdiParent);
				}
				//HookMdiClient(null);
				//if (mdiClient != null)
				//{
				//	CreateTabsForForms(MdiParent.MdiChildren);
				//}
			}
		}
		private void OnMdiParentHandleCreated(object sender, EventArgs e)
		{
			//JwAlert.Info("OnMdiParentHandleCreated");
			//if (!IsMdiParentSubclassed)
			//{
			//	SubclassMdiParent(sender as Form);
			//}
			//if (IsMdiParentSubclassed)
			//{
			//	Control control = sender as Control;
			//	if (control != null)
			//	{
			//		control.HandleCreated -= OnMdiParentHandleCreated;
			//	}
			//}
		}
		private void OnMdiParentFormControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control is MdiClient)
			{
				//JwAlert.Info("OnMdiParentFormControlAdded");
				//HookMdiClient(e.Control as MdiClient);
			}
		}
		private void OnMdiParentFormControlRemoved(object sender, ControlEventArgs e)
		{
			if (e.Control is MdiClient)
			{
				//JwAlert.Info("OnMdiParentFormControlRemoved");
				//UnhookMdiClient(e.Control as MdiClient);
			}
		}
		private void OnMdiParentMdiChildActivated(object sender, EventArgs e)
		{
			//JwAlert.Info("OnMdiParentMdiChildActivated");

			if(MdiParent.MdiChildren.Length>1)
            {
				MdiParent.LayoutMdi(MdiLayout.ArrangeIcons);
            }
			//if (IsRestoringTabs || IsReleasingTabs || IsInitializing)
			//{
			//	return;
			//}
			//CacheLastActiveTabGroup();
			//if (cachedActiveTab != null && cachedActiveTab.TabGroup != null)
			//{
			//	cachedActiveTab.TabGroup.DirtyTabItem(cachedActiveTab, invalidate: true, textChanged: false, imageChanged: false);
			//}
			//cachedActiveTab = ActiveTab;
			//if (cachedActiveTab != null && cachedActiveTab.TabGroup != null)
			//{
			//	cachedActiveTab.TabGroup.DirtyTabItem(cachedActiveTab, invalidate: true, textChanged: false, imageChanged: false);
			//}
			//if (!IsActivatingMdiChild && !SubclasserActivatingMdiChild)
			//{
			//	EnsureActiveTabIsSelected(addToGroupIfNecessary: true);
			//	if (ActiveTab != null)
			//	{
			//		ActiveTab.EnsureTabInView();
			//	}
			//	InvokeTabActivatedEvent();
			//}
		}
		private bool IsMdiParentSubclassed
		{
			get
			{
				if (mdiParentSubclasser == null)
				{
					return false;
				}
				return mdiParentSubclasser.Hooked;
			}
		}
		private void SubclassMdiParentUnsafe(Form mdiParent)
		{
			JwMdiParentSubclass mdiParentSubclasser = new JwMdiParentSubclass(this);
			mdiParentSubclasser.AssignHandle(mdiParent.Handle);
			this.mdiParentSubclasser = mdiParentSubclasser;
		}
		private void SubclassMdiParent(Form mdiParent)
		{
			if (IsMdiParentSubclassed || !mdiParent.IsHandleCreated)
			{
				return;
			}
			try
			{
				if (mdiParentSubclasser == null || !mdiParentSubclasser.Activate(this, mdiParent))
				{
					SubclassMdiParentUnsafe(mdiParent);
				}
			}
			catch (SecurityException)
			{
				//hasUnmanagedCodeRights = false;
			}
		}
		private void UnsubclassMdiParent()
		{
			if (mdiParentSubclasser != null)
			{
				mdiParentSubclasser.Unhook();
			}
		}
	}
}
