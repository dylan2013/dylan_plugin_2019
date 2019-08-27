using FISCA.Presentation.Controls;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMBA評鑑快刪功能
{
    public partial class DeleteForm : BaseForm
    {
        FISCA.UDT.AccessHelper _A = new FISCA.UDT.AccessHelper();
        FISCA.Data.QueryHelper _Q = new FISCA.Data.QueryHelper();
        List<Reply> ReplyList;

        BackgroundWorker bgw = new BackgroundWorker();



        public DeleteForm()
        {
            InitializeComponent();

            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            this.Text = "資料載入中...";


            bgw.RunWorkerAsync();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            ReplyList = _A.Select<Reply>(String.Format("ref_student_id={0}", K12.Presentation.NLDPanels.Student.SelectedSource[0]));

            foreach (Reply each in ReplyList)
            {
                if (each.Status == 0)
                {
                    each.s_Status = "暫存";
                }
                else if (each.Status == 1)
                {
                    each.s_Status = "送出";
                }

                DataTable dt = _Q.Select(string.Format("SELECT * FROM $ischool.emba.teaching_evaluation.survey where uid={0}", each.SurveyID));

                foreach (DataRow dRow in dt.Rows)
                {
                    string category = "" + dRow["Category"];
                    each.s_Category = category;
                }

                CourseRecord course = K12.Data.Course.SelectByID(each.CourseID.ToString());
                each.s_SchoolYear = course.SchoolYear.HasValue ? course.SchoolYear.ToString() : "";
                each.s_Course = course.Name;

                if (course.Semester == 0)
                {
                    each.s_Semester = "夏季學期";
                }
                else
                {
                    each.s_Semester = "第" + course.Semester + "學期";
                }

                TeacherRecord teacher = K12.Data.Teacher.SelectByID(each.TeacherID.ToString());
                each.s_Teacher = teacher.Name;

            }
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            dataGridViewX1.Rows.Clear();
            //只取得一位學生的填答狀態
            if (K12.Presentation.NLDPanels.Student.SelectedSource.Count == 1)
            {
                ReplyList.Sort(SortSchoolYear);

                foreach (Reply each in ReplyList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridViewX1);

                    row.Cells[colStatus.Index].Value = each.s_Status;
                    row.Cells[colAnswer.Index].Value = each.Answer;
                    row.Cells[colSurveyID.Index].Value = each.s_Category;
                    row.Cells[colSemester.Index].Value = each.s_Semester;
                    row.Cells[colSchoolYear.Index].Value = each.s_SchoolYear;
                    row.Cells[colTeacherID.Index].Value = each.s_Teacher;
                    row.Cells[colCourseID.Index].Value = each.s_Course;
                    row.Tag = each;

                    dataGridViewX1.Rows.Add(row);
                }
            }

            this.Text = "評鑑快刪";
        }

        private int SortSchoolYear(Reply a1, Reply a2)
        {
            return a1.UID.CompareTo(a2.UID);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //刪除資料

            List<Reply> ReplyList = new List<Reply>();

            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                Reply each = (Reply)row.Tag;
                ReplyList.Add(each);
            }

            _A.DeletedValues(ReplyList);

            this.Text = "資料載入中...";
            bgw.RunWorkerAsync();

            MsgBox.Show("資料已刪除!!");

        }
    }
}
