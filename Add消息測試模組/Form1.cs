using Campus.Message;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddMessageTestModule
{
    public partial class Form1 : BaseForm
    {
        List<CustomRecord> Lo_list = new List<CustomRecord>();
        Campus.Message.AlertCustom ac_run { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Campus.Message.CustomRecord cr = new Campus.Message.CustomRecord();

            #region 狀態為...

            if (cbNew.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.News;
            }
            else if (cbMessage.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Warning_Blue;
            }
            else if (cbWarning.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Warning_Red;
            }
            else if (cbPost.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Star;
            }
            else if (cbError.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Error;
            }

            #endregion

            cr.Title = textBoxX1.Text;
            cr.Content = textBoxX2.Text;

            List<CustomRecord> _Lo_list = new List<CustomRecord>();
            _Lo_list.Add(cr);

            if (Campus.Message.MessageRobot.IsShow)
            {

                Campus.Message.MessageRobot.AddMessage(cr);

            }
            else
            {
                Campus.Message.MessageRobot.ShowMessage(cr);
            }

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Campus.Message.CustomRecord cr = new Campus.Message.CustomRecord();

            #region 狀態為...

            if (cbNew.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.News;
            }
            else if (cbMessage.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Warning_Blue;
            }
            else if (cbWarning.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Warning_Red;
            }
            else if (cbPost.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Star;
            }
            else if (cbError.Checked)
            {
                cr.Type = Campus.Message.CrType.Type.Error;
            }

            #endregion

            cr.Title = textBoxX1.Text;
            cr.Content = textBoxX2.Text;

            List<CustomRecord> list = new List<CustomRecord>();
            list.Add(cr);
            Campus.Message.AlertCustom ac = new AlertCustom(list);
            ac.ShowMessage(300);

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
