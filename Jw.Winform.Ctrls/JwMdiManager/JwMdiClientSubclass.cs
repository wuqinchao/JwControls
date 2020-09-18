using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
	[SuppressUnmanagedCodeSecurity]
	internal class JwMdiClientSubclass : NativeWindow
	{
		private JwMdiManager _manager;

		private Rectangle displayRect = Rectangle.Empty;

		private bool IsManagerEnabled
		{
			get
			{
				if (_manager != null)
				{
					return _manager.Enabled;
				}
				return false;
			}
		}

		private Rectangle DisplayRectangle => displayRect;

		public bool Hooked => _manager != null;

		//IntPtr IMdiClientWindow.Handle => base.Handle;

		internal JwMdiClientSubclass(JwMdiManager mdiManager)
		{
			_manager = mdiManager;
			UpdateBorderStyle();
		}

		protected override void WndProc(ref Message m)
		{
			JwMdiManager ultraTabbedMdiManager = _manager;
			if (ultraTabbedMdiManager == null)
			{
				base.WndProc(ref m);
				return;
			}
			switch (m.Msg)
			{
			case (int)WinApi.Messages.WM_MDIRESTORE:
			case (int)WinApi.Messages.WM_MDIMAXIMIZE:
			case (int)WinApi.Messages.WM_MDITILE:
			case (int)WinApi.Messages.WM_MDICASCADE:
			case (int)WinApi.Messages.WM_MDIICONARRANGE:
				//if (ultraTabbedMdiManager.Enabled)
				//{
					m.Result = IntPtr.Zero;
					return;
				//}
				//break;
			case (int)WinApi.Messages.WM_MDINEXT:
			{
				Form mdiChild = null;
				if (m.WParam == IntPtr.Zero)
				{
					Control control = Control.FromChildHandle(base.Handle);
					if (control is Form)
					{
						mdiChild = ((Form)control).ActiveMdiChild;
					}
				}
				else
				{
					mdiChild = (Control.FromHandle(m.WParam) as Form);
				}
				bool forward = m.LParam == IntPtr.Zero;
				Form nextWindow = GetNextWindow(mdiChild, forward);
				if (ultraTabbedMdiManager.TabNavigationMode == MdiTabNavigationMode.VisibleOrder)
				{
					nextWindow?.Activate();
					m.Result = IntPtr.Zero;
					return;
				}
				if (!SendActivationNotification(nextWindow))
				{
					break;
				}
				if (AllowMdiChildActivate(nextWindow))
				{
					ultraTabbedMdiManager.SubclasserActivatingMdiChild = true;
					try
					{
						base.WndProc(ref m);
					}
					finally
					{
						ultraTabbedMdiManager.SubclasserActivatingMdiChild = false;
						ultraTabbedMdiManager.AfterSubclasserFormActivation(nextWindow);
					}
				}
				else
				{
					m.Result = IntPtr.Zero;
				}
				return;
			}
			case (int)WinApi.Messages.WM_MDIACTIVATE:
			{
				Form form = Control.FromHandle(m.WParam) as Form;
				if (!SendActivationNotification(form))
				{
					break;
				}
				if (AllowMdiChildActivate(form))
				{
					ultraTabbedMdiManager.SubclasserActivatingMdiChild = true;
					try
					{
						base.WndProc(ref m);
					}
					finally
					{
						ultraTabbedMdiManager.SubclasserActivatingMdiChild = false;
						ultraTabbedMdiManager.AfterSubclasserFormActivation(form);
					}
				}
				else
				{
					m.Result = IntPtr.Zero;
				}
				return;
			}
			case (int)WinApi.Messages.WM_NCPAINT:
			{
				base.WndProc(ref m);
				if (!IsManagerEnabled)
				{
					return;
				}
				IntPtr intPtr = IntPtr.Zero;
				Graphics graphics = null;
				IntPtr hWnd = m.HWnd;
				try
				{
					Rectangle rectangle = new Rectangle(Point.Empty, DisplayRectangle.Size);
					if (m.WParam == IntPtr.Zero || m.WParam.ToInt64() == 1)
					{
						intPtr = WinApi.GetWindowDC(hWnd);
					}
					else
					{
						intPtr = WinApi.GetDCEx(hWnd, m.WParam, 129);
						if (intPtr == IntPtr.Zero)
						{
							intPtr = WinApi.GetWindowDC(m.HWnd);
						}
					}
					if (intPtr != IntPtr.Zero)
					{
						SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
						securityPermission.Assert();
						graphics = Graphics.FromHdc(intPtr);
						CodeAccessPermission.RevertAssert();
						DrawBorder(graphics, rectangle, rectangle);
						m.Result = IntPtr.Zero;
					}
				}
				finally
				{
					graphics?.Dispose();
					if (intPtr != IntPtr.Zero)
					{
								WinApi.ReleaseDC(hWnd, intPtr);
					}
				}
				return;
			}
			case (int)WinApi.Messages.WM_NCCALCSIZE:
				if (IsManagerEnabled && m.WParam.ToInt64() == 1)
				{
					NativeWindowMethods.NCCALCSIZE_PARAMS nCCALCSIZE_PARAMS = (NativeWindowMethods.NCCALCSIZE_PARAMS)m.GetLParam(typeof(NativeWindowMethods.NCCALCSIZE_PARAMS));
					UIElementBorderWidths borderWidth = GetBorderWidth();
					displayRect = nCCALCSIZE_PARAMS.rectProposed.Rect;
					nCCALCSIZE_PARAMS.rectProposed.left += borderWidth.Left;
					nCCALCSIZE_PARAMS.rectProposed.right -= borderWidth.Right;
					nCCALCSIZE_PARAMS.rectProposed.top += borderWidth.Top;
					nCCALCSIZE_PARAMS.rectProposed.bottom -= borderWidth.Bottom;
					Marshal.StructureToPtr(nCCALCSIZE_PARAMS, m.LParam, fDeleteOld: false);
				}
				break;
			case (int)WinApi.Messages.WM_ERASEBKGND:
				base.WndProc(ref m);
				if (IsManagerEnabled)
				{
					m.Result = new IntPtr(1);
				}
				return;
			case (int)WinApi.Messages.WM_PARENTNOTIFY:
				if ((m.WParam.ToInt32() & 0xFFFF) == 1)
				{
						_manager.MdiClientCreateNotification();
				}
				break;
			}
			base.WndProc(ref m);
		}

		private bool SendActivationNotification(Form form)
		{
			if (_manager == null)
			{
				return false;
			}
			return _manager.ShouldSubclasserNotifyFormActivation(form);
		}

		private bool AllowMdiChildActivate(Form form)
		{
			if (_manager != null)
			{
				return _manager.ShouldSubclasserAllowFormActivation(form);
			}
			return true;
		}

		private Form GetNextWindow(Form mdiChild, bool forward)
		{
			if (mdiChild == null)
			{
				return null;
			}
			if (_manager != null && manager.TabNavigationMode == MdiTabNavigationMode.VisibleOrder)
			{
				MdiTab mdiTab = manager.TabFromForm(mdiChild);
				MdiTab mdiTab2 = null;
				if (mdiTab != null)
				{
					mdiTab2 = mdiTab.GetNextPreviousTab(wrap: true, forward);
				}
				return mdiTab2?.Form;
			}
			MdiClient mdiClient = Control.FromHandle(base.Handle) as MdiClient;
			int childIndex = mdiClient.Controls.GetChildIndex(mdiChild, throwException: false);
			if (childIndex < 0)
			{
				return null;
			}
			int count = mdiClient.Controls.Count;
			Control control = null;
			if (forward)
			{
				for (int i = childIndex + 1; i < count * 2; i++)
				{
					Control control2 = mdiClient.Controls[i % count];
					if (control2.CanSelect)
					{
						control = control2;
						break;
					}
				}
			}
			else
			{
				for (int num = childIndex - 1 + count; num > childIndex; num--)
				{
					Control control3 = mdiClient.Controls[num % count];
					if (control3.CanSelect)
					{
						control = control3;
						break;
					}
				}
			}
			return control as Form;
		}

		private void Unhook()
		{
			_manager = null;
			UpdateBorderStyle();
		}

		private void DrawBorder(Graphics g, Rectangle rect, Rectangle clipRect)
		{
			if (!IsManagerEnabled)
			{
				return;
			}
			Color color = _manager.BorderColor;
			Pen pen = null;
			switch (_manager.BorderStyle)
			{
			case MdiClientBorderStyle.Inset:
				if (color.IsEmpty)
				{
					color = SystemColors.Control;
				}
				if (color == SystemColors.Control)
				{
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Left | Border3DSide.Top, SystemColors.ControlDark, ref pen);
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Right | Border3DSide.Bottom, SystemColors.ControlLightLight, ref pen);
					rect.Inflate(-1, -1);
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Left | Border3DSide.Top, SystemColors.ControlDarkDark, ref pen);
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Right | Border3DSide.Bottom, SystemColors.ControlLight, ref pen);
				}
				else
				{
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Left | Border3DSide.Top, DrawUtility.Dark(color), ref pen);
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Right | Border3DSide.Bottom, DrawUtility.LightLight(color), ref pen);
					rect.Inflate(-1, -1);
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Left | Border3DSide.Top, DrawUtility.DarkDark(color), ref pen);
					DrawBorderHelper(g, rect, clipRect, Border3DSide.Right | Border3DSide.Bottom, DrawUtility.Light(color), ref pen);
				}
				break;
			case MdiClientBorderStyle.Solid:
				if (color.IsEmpty)
				{
					color = SystemColors.ControlDark;
				}
				DrawBorderHelper(g, rect, clipRect, Border3DSide.All, color, ref pen);
				break;
			}
			pen?.Dispose();
		}

		private void DrawBorderHelper(Graphics g, Rectangle rect, Rectangle invalidRect, Border3DSide sides, Color color, ref Pen pen)
		{
			if (sides == (Border3DSide)0)
			{
				return;
			}
			Rectangle rectangle = rect;
			rectangle.Width--;
			rectangle.Height--;
			if (rectangle.Height > 0 && rectangle.Width > 0)
			{
				if (pen == null)
				{
					pen = Infragistics.Win.Utilities.CreatePen(color);
				}
				else if (pen.Color != color)
				{
					pen.Color = Infragistics.Win.Utilities.GetNonSystemColor(color);
				}
				if ((sides & Border3DSide.Left) != 0 && invalidRect.Left <= rectangle.Left)
				{
					g.DrawLine(pen, rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
				}
				if ((sides & Border3DSide.Right) != 0 && invalidRect.Right >= rectangle.Right - 1)
				{
					g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
				}
				if ((sides & Border3DSide.Top) != 0 && invalidRect.Top <= rectangle.Top)
				{
					g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
				}
				if ((sides & Border3DSide.Bottom) != 0 && invalidRect.Bottom >= rectangle.Bottom - 1)
				{
					g.DrawLine(pen, rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
				}
			}
		}

		private UIElementBorderWidths GetBorderWidth()
		{
			UIElementBorderWidths result = default(UIElementBorderWidths);
			if (IsManagerEnabled)
			{
				switch (manager.BorderStyle)
				{
				case MdiClientBorderStyle.Inset:
				{
					int num9 = result.Bottom = 2;
					int num11 = result.Top = num9;
					int num14 = result.Left = (result.Right = num11);
					break;
				}
				case MdiClientBorderStyle.Solid:
				{
					int num2 = result.Bottom = 1;
					int num4 = result.Top = num2;
					int num7 = result.Left = (result.Right = num4);
					break;
				}
				}
			}
			return result;
		}

		protected override void OnHandleChange()
		{
			base.OnHandleChange();
			if (IsManagerEnabled)
			{
				UpdateBorderStyle();
			}
		}

		private void UpdateBorderStyle()
		{
			if (!IsManagerEnabled)
			{
				NativeWindowMethods.UpdateBorderStyle(base.Handle, BorderStyle.Fixed3D);
			}
			else
			{
				NativeWindowMethods.UpdateBorderStyle(base.Handle, BorderStyle.None);
			}
		}

		void IMdiClientWindow.BorderColorChanged()
		{
			if (IsManagerEnabled)
			{
				SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
				try
				{
					securityPermission.Assert();
					NativeWindowMethods.RedrawWindowApi(base.Handle, IntPtr.Zero, IntPtr.Zero, 1089u);
				}
				catch (SecurityException)
				{
				}
			}
		}

		void IMdiClientWindow.VerifyBorderStyle()
		{
			UpdateBorderStyle();
		}

		void IMdiClientWindow.Unsubclass()
		{
			Unhook();
		}

		bool IMdiClientWindow.Activate(UltraTabbedMdiManager manager, MdiClient mdiClient)
		{
			if (mdiClient == null || manager == null)
			{
				return false;
			}
			if (this.manager == manager && mdiClient.Handle == base.Handle)
			{
				return true;
			}
			if (base.Handle != mdiClient.Handle)
			{
				return false;
			}
			this.manager = manager;
			UpdateBorderStyle();
			return true;
		}
	}
}
