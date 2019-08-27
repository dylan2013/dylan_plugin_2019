using FISCA.Presentation.Controls;
using iCampusManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iCampusManagerPlugin
{
    public partial class AddTempForm : BaseForm
    {
        public AddTempForm()
        {
            InitializeComponent();
        }

        private void btnAddTemp_Click(object sender, EventArgs e)
        {

            FISCA.Data.QueryHelper _q = new FISCA.Data.QueryHelper();

            string inputContext = textBoxX1.Text;
            string[] ContentLines = inputContext.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            DataTable dt = _q.Select(string.Format("select * from $school where title in ('{0}')", string.Join("','", ContentLines)));
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add("" + row["uid"]);
            }
            SchoolPanel.SetSchoolPanel.AddToTemp(list);
        }
    }
}
