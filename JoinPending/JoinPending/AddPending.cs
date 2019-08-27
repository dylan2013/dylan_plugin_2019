using FISCA.Presentation.Controls;
using FISCA.UDT;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoinPending
{
    public partial class AddPending : BaseForm
    {
        FISCA.Data.QueryHelper _q = new FISCA.Data.QueryHelper();

        IDType type = IDType.學生系統編號;

        public AddPending()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string StudentIDLong = textBoxX1.Text;
            List<string> studentList1 = new List<string>();
            List<string> studentList2 = new List<string>();
            if (checkBoxX1.Checked) //斷行
            {
                StudentIDLong = StudentIDLong.Replace("\r\n", ",");
                studentList1 = Seting(StudentIDLong);
            }
            else //逗號
            {
                StudentIDLong = StudentIDLong.Replace("\r\n", "");
                StudentIDLong = StudentIDLong.Replace(textBoxX2.Text, ",");
                studentList1 = Seting(StudentIDLong);
            }

            StringBuilder sb = new StringBuilder();
            if (type == IDType.學號)
                sb.Append(string.Format("select id,name,student_number,id_number from student where {0} in('{1}')", "student_number", string.Join("','", studentList1)));
            else if (type == IDType.身分證號)
                sb.Append(string.Format("select id,name,student_number,id_number from student where {0} in('{1}')", "id_number", string.Join("','", studentList1)));
            else
                sb.Append(string.Format("select id,name,student_number,id_number from student where {0} in('{1}')", "id", string.Join("','", studentList1)));

            DataTable dt = new DataTable();
            try
            {
                dt = _q.Select(sb.ToString());
            }
            catch
            {
                MsgBox.Show("資料取得錯誤!!");
                return;
            }
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty("" + row["id"]))
                {
                    if (!studentList2.Contains("" + row["id"]))
                    {
                        studentList2.Add("" + row["id"]);
                    }
                }
            }

            K12.Presentation.NLDPanels.Student.AddToTemp(studentList2);

        }

        public List<string> Seting(string StudentIDLong)
        {
            List<string> list = new List<string>();
            string[] Students = StudentIDLong.Split(',');
            foreach (string each in Students)
            {
                if (!string.IsNullOrEmpty(each))
                {
                    list.Add(each);
                }
            }
            return list;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            K12.Presentation.NLDPanels.Student.RemoveFromTemp(K12.Presentation.NLDPanels.Student.TempSource);
        }

        private void checkBoxX3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxX1.WatermarkText = "請填入學生系統編號";
            type = IDType.學生系統編號;
        }

        private void checkBoxX4_CheckedChanged(object sender, EventArgs e)
        {
            textBoxX1.WatermarkText = "請填入學號";
            type = IDType.學號;
        }

        private void checkBoxX5_CheckedChanged(object sender, EventArgs e)
        {
            textBoxX1.WatermarkText = "請填入身分證號";
            type = IDType.身分證號;
        }
    }

    enum IDType { 學生系統編號, 學號, 身分證號 }
}
