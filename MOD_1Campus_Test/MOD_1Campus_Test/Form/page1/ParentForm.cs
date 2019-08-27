using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOD_1Campus_Test
{
    public partial class ParentForm : BaseForm
    {
        BackgroundWorker BGW = new BackgroundWorker();

        List<Stud> studList { get; set; }

        //條件
        string searchText = "";

        public ParentForm()
        {
            InitializeComponent();
        }

        private void ParentForm_Load(object sender, EventArgs e)
        {

            BGW.DoWork += BGW_DoWork;
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;

            //畫面啟動,先執行第一次資料取得
            RunWorker();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (!BGW.IsBusy)
            {
                RunWorker();
            }
            else
            {
                MsgBox.Show("系統忙碌中");
            }
        }

        private void RunWorker()
        {
            btnSelect.Enabled = false;
            searchText = textBoxX1.Text;
            BGW.RunWorkerAsync();
        }

        private void BGW_DoWork(object sender, DoWorkEventArgs e)
        {

            StringBuilder sb_sql = new StringBuilder();
            sb_sql.Append("select student.id as student_id,student.name as student_name,student.student_number,");
            sb_sql.Append("student.seat_no,class.class_name,class.grade_year,parent.relationship,");
            sb_sql.Append("parent.account as parent_account,student.student_code,student.parent_code,");
            sb_sql.Append("sa_login_name,student.father_name,student.mother_name,student.custodian_name  ");
            sb_sql.Append("from student ");
            sb_sql.Append("left join student_parent parent on student.id=parent.ref_student_id ");
            sb_sql.Append("left join class on class.id=student.ref_class_id ");
            sb_sql.Append("where account is not null ");

            //當使用者輸入條件時
            if (searchText != "")
            {
                sb_sql.Append(string.Format("and (student.name like '%{0}%' or  student.student_number like '%{0}%' or ", searchText));
                sb_sql.Append(string.Format("parent.account like '%{0}%' or student.student_code like '%{0}%' or student.parent_code like '%{0}%' or ", searchText));
                sb_sql.Append(string.Format("student.sa_login_name like '%{0}%' or student.father_name like '%{0}%' or ", searchText));
                sb_sql.Append(string.Format("student.mother_name like '%{0}%' or  student.custodian_name like '%{0}%') ", searchText));
            }
            sb_sql.Append("order by class.grade_year,class.display_order,class.grade_year,student.seat_no,student.name");


            //取得學生代碼家長代碼 student - parent_code student_code
            DataTable dt = tool._Q.Select(sb_sql.ToString());
            studList = new List<Stud>();
            foreach (DataRow row in dt.Rows)
            {
                Stud stud = new Stud(row);
                studList.Add(stud);
            }
        }

        private void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSelect.Enabled = true;

            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    helpCount.Text = string.Format("已關連行動應用帳號共：{0} 組", studList.Count);

                    dataGridViewX1.Rows.Clear();
                    foreach (Stud each in studList)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridViewX1);

                        row.Cells[colClassName.Index].Value = each.class_name;
                        row.Cells[colSentNo.Index].Value = each.seat_no;
                        row.Cells[colStudentNumber.Index].Value = each.student_number;
                        row.Cells[colStudentName.Index].Value = each.student_name;
                        row.Cells[colStudentAccount.Index].Value = each.student_account;

                        row.Cells[colStudentCode.Index].Value = each.student_code;
                        row.Cells[colParentCode.Index].Value = each.parent_code;
                        row.Cells[colRelationship.Index].Value = each.relationship;
                        row.Cells[colParentEmail.Index].Value = each.parent_account;

                        row.Cells[colFatherName.Index].Value = each.father_name;
                        row.Cells[colMotherName.Index].Value = each.mother_name;
                        row.Cells[colCustodianName.Index].Value = each.custodian_name;

                        row.Tag = each;
                        dataGridViewX1.Rows.Add(row);
                    }
                }
                else
                {
                    MsgBox.Show("發生錯誤：\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("已取消");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ParentByStudent pbs = new ParentByStudent();
            pbs.ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region 匯出
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "匯出家長使用查詢清單";
            saveFileDialog1.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            DataGridViewExport export = new DataGridViewExport(dataGridViewX1);
            export.Save(saveFileDialog1.FileName);

            if (new CompleteForm().ShowDialog() == DialogResult.Yes)
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
            #endregion
        }
    }
}
