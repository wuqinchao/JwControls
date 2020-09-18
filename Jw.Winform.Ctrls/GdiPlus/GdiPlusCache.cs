using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class GdiPlusCache
    {
		[ThreadStatic]
		private static Graphics _CacheGraphics;
		public static Graphics CacheGraphics
		{
			get
			{
				if (_CacheGraphics == null)
				{
					_CacheGraphics = Graphics.FromHdc(IntPtr.Zero);
				}
				return _CacheGraphics;
			}
		}
	}
}
