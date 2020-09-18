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
    public class JwIconSelectionEditor : System.Drawing.Design.UITypeEditor
    {
        private IWindowsFormsEditorService edSvc = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            if (context != null && context.Instance != null && provider != null)
            {

                edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                var ctrl = context.Instance as IIconfont;
                if (edSvc != null)
                {
                    using (var f = new IconSelection(ctrl.IconFontName))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            value = f.SelectKey;
                        }
                    }
                }
            }

            return value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context);
        }
    }

    /* dorpdown
    public class CategoryDropDownEditor : System.Drawing.Design.UITypeEditor
    {
        public CategoryDropDownEditor()
        {
        }
        public override System.Drawing.Design.UITypeEditorEditStyle
            GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            //指定编辑样式为下拉形状, 且基于Control 类型
            return UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, 
            System.IServiceProvider provider, object value)
        {
            //取得编辑器服务对象
            IWindowsFormsEditorService service = 
                (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (service == null)
            {
                return null;
            }
      
            //定义一个用户控件对象
            string original = string.Empty;
            string strReturn = string.Empty;
            CategoryDorpDown dropform = new CategoryDorpDown(service);
            ComboBox obj = dropform.Controls[2] as ComboBox;
            if (obj != null)
            {
                if (value != null&&value.ToString()!="")
                {
                    obj.Text= (string)value;
                }
            }
            service.DropDownControl(dropform);
            strReturn = dropform.strReturnValue;
            if (strReturn + String.Empty != String.Empty)
            {
                return strReturn;
            }
            return (string)value;
        }
    }
     */
}
