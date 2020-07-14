using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Jw.Winform.Ctrls
{
    public class JwIconDesigner : ControlDesigner
    {
        private readonly DesignerVerbCollection designerVerbs = new DesignerVerbCollection();

        private IDesignerHost designerHost;

        //private ISelectionService selectionService;

        //public override SelectionRules SelectionRules
        //{
        //    get
        //    {
        //        return base.SelectionRules;
        //    }
        //}

        public IDesignerHost DesignerHost
        {
            get
            {
                return designerHost ?? (designerHost = (IDesignerHost)(GetService(typeof(IDesignerHost))));
            }
        }

        //public ISelectionService SelectionService
        //{
        //    get
        //    {
        //        return selectionService ?? (selectionService = (ISelectionService)(GetService(typeof(ISelectionService))));
        //    }
        //}
        public override DesignerVerbCollection Verbs
        {
            get
            {
                return designerVerbs;
            }
        }

        public JwIconDesigner()
        {
            var verb1 = new DesignerVerb("选择图标", OnSelected);
            designerVerbs.AddRange(new[] { verb1 });
        }

        private void OnSelected(Object sender, EventArgs e)
        {
            var icon = (JwIcon)Control;
            var old = icon.IconText;
            using(var f = new JwIcons.IconSelection())
            {
                if(f.ShowDialog() == DialogResult.OK)
                {
                    icon.IconText = f.SelectKey;
                    RaiseComponentChanged(TypeDescriptor.GetProperties(icon)["IconText"],
                                  old, icon.IconText);
                }
            }
        }
    }
}
