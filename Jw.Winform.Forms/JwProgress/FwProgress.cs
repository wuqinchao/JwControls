using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Forms
{
    public partial class FwProgress : Form
    {
        public FwProgress()
        {
            InitializeComponent();
        }
        public ProgressCompleteArgs Result { get; set; }
        public IProgressService Service { get; set; }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if(Service.CanCancel)
            {
                Service.Cancel();
            }
        }

        private void FwProgress_Load(object sender, EventArgs e)
        {
            btn_cancel.Visible = Service.CanCancel;
            Service.OnProgressChanged += Service_OnProgressChanged;
            Service.OnComplete += Service_OnComplete;
            StartService();
        }

        private void StartService()
        {
            Task t = new Task(new Action(() => {
                Service.Start();
            }));
            t.Start();
        }

        private void Service_OnComplete(object sender, ProgressCompleteArgs args)
        {
            this.BeginInvoke(new Action(() =>
            {
                Result = args;
                this.Close();
            }));
        }

        private void Service_OnProgressChanged(object sender, ProgressArgs args)
        {
            this.BeginInvoke(new Action(() => {
                t_value.Value = args.Progress;
                t_title.Text = args.Name;
                t_task.Text = args.TaskName;
                t_left.Text = args.LeftInfo;
                t_left.Visible = !string.IsNullOrEmpty(args.LeftInfo);
                t_right.Text = args.RightInfo;
                t_right.Visible = !string.IsNullOrEmpty(args.RightInfo);
            }));
        }
    }
}
