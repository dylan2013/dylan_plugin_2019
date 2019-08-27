using DesktopLib;
using FISCA;
using FISCA.Presentation.Controls;
using iCampusManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace iCampusManagerPlugin
{
    public partial class MessageTeacherUsage : BaseForm
    {
        BackgroundWorker BGW = new BackgroundWorker();


        public MessageTeacherUsage()
        {
            InitializeComponent();
        }

        private void MessageUsage_Load(object sender, EventArgs e)
        {
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
            BGW.DoWork += BGW_DoWork;

            BGW.RunWorkerAsync();
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            List<MessageTeacherCountRow> list = new List<MessageTeacherCountRow>();

            StringBuilder sql = new StringBuilder();
            sql.Append("select notice.uid as 訊息編號,notice.post_time as 發送日期,notice.title as 標題,");
            sql.Append("teacher.teacher_name as 發送老師,count(notice_target.uid) as 發送數,notice.message ");
            sql.Append("from $ischool.1campus.notice notice ");
            sql.Append("join $ischool.1campus.notice_target notice_target on notice_target.ref_notice_id=notice.uid ");
            sql.Append("join teacher on teacher.id=notice.ref_teacher_id ");
            sql.Append("where notice.global_notice='false' and notice.ref_teacher_id is not null ");
            sql.Append("group by notice.uid,notice.title,teacher.teacher_name,notice.post_time ");
            sql.Append("order by notice.post_time DESC ");

            List<XElement> rsps = tool.MultiTasking_NowRun(sql.ToString(), SchoolPanel.SetSchoolPanel.SelectedSource);

            foreach (XElement rsp in rsps)
            {
                DataTable dt = tool.GetDateTable(rsp);
                string uid = rsp.AttributeText("UID");

                foreach (DataRow row in dt.Rows)
                {
                    MessageTeacherCountRow mcR = new MessageTeacherCountRow();
                    mcR.SchoolName = SchoolPanel.GlobalSchoolCache[uid].Title;
                    mcR.SchoolDSNS = "" + SchoolPanel.GlobalSchoolCache[uid].DSNS;

                    mcR.TeacherName = "" + row["發送老師"];

                    int count = 0;
                    if (int.TryParse("" + row["發送數"], out count))
                    {
                        mcR.Count = count;
                    }

                    mcR.Date = "" + row["發送日期"];
                    mcR.Title = "" + row["標題"];
                    mcR.message = "" + row["message"];
                    list.Add(mcR);
                }
            }

            e.Result = list;
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    List<MessageTeacherCountRow> list = (List<MessageTeacherCountRow>)e.Result;
                    foreach (MessageTeacherCountRow teacher in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridViewX1);

                        row.Cells[colDSNS.Index].Value = teacher.SchoolDSNS;
                        row.Cells[colSchoolName.Index].Value = teacher.SchoolName;
                        row.Cells[colDate.Index].Value = teacher.Date;
                        row.Cells[colTitle.Index].Value = teacher.Title;
                        row.Cells[colContent.Index].Value = teacher.message;
                        row.Cells[colTeacherName.Index].Value = teacher.TeacherName;
                        row.Cells[colCount.Index].Value = teacher.Count;

                        dataGridViewX1.Rows.Add(row);

                    }

                }
                else
                {
                    MsgBox.Show("取得資料發生錯誤!!\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("背景模式已取消!!");
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    class MessageTeacherCountRow
    {
        /// <summary>
        /// 學校名稱
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 學校DSNS  
        /// </summary>
        public string SchoolDSNS { get; set; }

        public string Date { get; set; }
        public string TeacherName { get; set; }

        public string Title { get; set; }

        public string message { get; set; }

        public int Count { get; set; }


    }
}
