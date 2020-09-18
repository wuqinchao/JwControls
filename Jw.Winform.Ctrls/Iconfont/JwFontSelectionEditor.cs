using Jw.Winform.Ctrls.JwIcons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Jw.Winform.Ctrls
{
    public class JwFontSelectionEditor : System.Drawing.Design.UITypeEditor
    {
        private IWindowsFormsEditorService service = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            if (context != null && context.Instance != null && provider != null)
            {
                service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                ListBox lbValues = new ListBox();
                lbValues.BorderStyle = BorderStyle.None;
                var fonts = JwIconfontManager.GetFonts();
                var ctrl = context.Instance as IIconfont;
                var old = ctrl.IconFontName;
                for (var i = 0; i < fonts.Length; i++)
                {
                    lbValues.Items.Add(fonts[i]);
                    if (fonts[i].ToUpper().Trim().Equals(old.ToUpper().Trim(), StringComparison.Ordinal))
                    {
                        lbValues.SelectedIndex = i;
                    }
                }
                lbValues.Click += (sender, arg) =>
                {
                    service.CloseDropDown();
                };
                service.DropDownControl(lbValues);
                if (lbValues.SelectedItem != null)
                    return lbValues.SelectedItem.ToString();
                else
                    return old;
            }

            return value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }
}
