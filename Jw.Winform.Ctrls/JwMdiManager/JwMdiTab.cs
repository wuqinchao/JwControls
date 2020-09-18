using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class JwMdiTab
    {
        private Form form;
        private string text;
        private Icon icon;
        private Image iconImage;
        private Stream iconStream; 
		private bool isClosingTab;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ReadOnly(true)]
        public Form Form => form;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsFormEnabled => Form?.Enabled ?? false;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsFormVisible
		{
			get
			{
				Form form = Form;
				if (form != null)
				{
					if (form.Visible)
					{
						return !form.IsDisposed;
					}
					return false;
				}
				return false;
			}
		}
		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				if (!(value == text))
				{
					text = value;
				}
			}
		}
		public bool CanActivate
		{
			get
			{
				if (form != null && form.Visible && !form.Disposing)
				{
					return !form.IsDisposed;
				}
				return false;
			}
		}
		private bool FormHasIcon
		{
			get
			{
				if (form != null)
				{
					return form.Icon != null;
				}
				return false;
			}
		}
		void DisposeIconImage()
		{
			icon = null;
			if (iconImage != null)
			{
				iconImage.Dispose();
				iconImage = null;
			}
			if (iconStream != null)
			{
				iconStream.Close();
				iconStream = null;
			}
		}
		private void HookForm()
		{
			if (form != null)
			{
				//form.EnabledChanged += OnMdiChildEnabledChanged;
				//form.VisibleChanged += OnMdiChildVisibleChanged;
				//form.TextChanged += OnMdiChildTextChanged;
				//form.LocationChanged += OnMdiChildLocationChanged;
				//form.SizeChanged += OnMdiChildSizeChanged;
				//form.Closing += OnMdiChildClosing;
				//form.Closed += OnMdiChildClosed;
				//form.Enter += OnMdiChildEnter;
			}
		}
		private void UnhookForm()
		{
			if (form != null)
			{
				//form.EnabledChanged -= OnMdiChildEnabledChanged;
				//form.VisibleChanged -= OnMdiChildVisibleChanged;
				//form.TextChanged -= OnMdiChildTextChanged;
				//form.LocationChanged -= OnMdiChildLocationChanged;
				//form.SizeChanged -= OnMdiChildSizeChanged;
				//form.Closing -= OnMdiChildClosing;
				//form.Closed -= OnMdiChildClosed;
				//form.Enter -= OnMdiChildEnter;
				//RestoreTabFormSettings();
				DisposeIconImage();
			}
		}
		private void CreateFormIcon()
		{
			Form form = this.form;
			if (form == null || form.Icon == null || form.Disposing || form.IsDisposed)
			{
				DisposeIconImage();
			}
			else
			{
				if (form.Icon == this.icon)
				{
					return;
				}
				DisposeIconImage();
				this.icon = form.Icon;
				Icon icon = form.Icon;
				Size size = (Manager != null) ? Manager.ImageSize : UltraTabbedMdiManager.ImageSizeDefault;
				try
				{
					if (size.Width > 0 && size.Height > 0)
					{
						icon = new Icon(icon, size);
					}
				}
				catch (Exception)
				{
				}
				iconStream = new MemoryStream();
				icon.Save(iconStream);
				iconStream.Position = 0L;
				try
				{
					iconImage = Image.FromStream(iconStream);
				}
				catch (Exception ex2)
				{
					if (!(ex2 is ArgumentException))
					{
						throw;
					}
					iconStream.Close();
					iconStream = null;
					Bitmap bitmap = icon.ToBitmap();
					try
					{
						if (bitmap.Size == icon.Size)
						{
							iconImage = bitmap;
							bitmap = null;
						}
						else
						{
							iconImage = CreateScaledImage(bitmap, icon.Size, new Rectangle(Point.Empty, bitmap.Size));
						}
					}
					finally
					{
						bitmap?.Dispose();
					}
				}
				finally
				{
					if (icon != form.Icon)
					{
						icon.Dispose();
					}
				}
			}
		}
		private void OnMdiChildEnabledChanged(object sender, EventArgs e)
		{
			//TabGroup?.DirtyTabItem(this, invalidate: true, textChanged: false, imageChanged: false);
		}

		private void OnMdiChildVisibleChanged(object sender, EventArgs e)
		{
			//Manager?.MdiChildVisibilityChanged(this);
		}

		private void OnMdiChildTextChanged(object sender, EventArgs e)
		{
			//TabGroup?.DirtyTabItem(this, invalidate: true, textChanged: true, imageChanged: false);
		}

		private void OnMdiChildLocationChanged(object sender, EventArgs e)
		{
			//Manager?.ResetFormPosition(this);
		}

		private void OnMdiChildSizeChanged(object sender, EventArgs e)
		{
			//Manager?.ResetFormPosition(this);
		}

		private void OnMdiChildClosing(object sender, CancelEventArgs e)
		{
			if (isClosingTab)
			{
				return;
			}
			//UltraTabbedMdiManager manager = Manager;
			//if (manager != null)
			//{
			//	MdiTabClosingEventArgs mdiTabClosingEventArgs = new MdiTabClosingEventArgs(this, MdiTabCloseReason.FormClosed);
			//	manager.InvokeEvent(TabbedMdiEventIds.TabClosing, mdiTabClosingEventArgs);
			//	if (mdiTabClosingEventArgs.Cancel)
			//	{
			//		e.Cancel = true;
			//	}
			//}
		}

		private void OnMdiChildClosed(object sender, EventArgs e)
		{
			if (isClosingTab)
			{
				return;
			}
			if (Form == null && Form.IsDisposed)
			{
				//UltraTabbedMdiManager manager = Manager;
				//if (manager != null)
				//{
				//	MdiTabClosedEventArgs e2 = new MdiTabClosedEventArgs(this, MdiTabCloseReason.FormClosed);
				//	manager.InvokeEvent(TabbedMdiEventIds.TabClosed, e2);
				//}
			}
			//else if (Manager != null)
			//{
			//	new TabClosedHelper(this, Manager, sender as Form);
			//}
		}

		private void OnMdiChildEnter(object sender, EventArgs e)
		{
			Form form = sender as Form;
			//if (CanActivate && form.ContainsFocus && form.IsMdiChild && Manager != null)
			//{
			//	Form mdiParent = Infragistics.Win.UltraWinTabbedMdi.Utilities.GetMdiParent(form);
			//	if (mdiParent.ActiveMdiChild != null && form != mdiParent.ActiveMdiChild)
			//	{
			//		Manager.ActivateMdiChild(this, invokeTabSelecting: true, invokeTabSelected: true, allowCancel: false, activate: true);
			//	}
			//}
		}
		public static Image CreateScaledImage(Image original, Size destinationSize, Rectangle srcRect)
		{
			if (original == null)
			{
				return null;
			}
			if (destinationSize.Width <= 0 || destinationSize.Height <= 0)
			{
				return null;
			}
			Rectangle rectangle = new Rectangle(Point.Empty, original.Size);
			rectangle.Intersect(srcRect);
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				return null;
			}
			Image image = new Bitmap(original, destinationSize);
			Rectangle destRect = new Rectangle(Point.Empty, destinationSize);
			Graphics graphics = Graphics.FromImage(image);
			graphics.DrawImage(original, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel);
			graphics.Dispose();
			return image;
		}

	}
}
