using FISCA.Presentation.Controls;
using FISCA;
using iCampusManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace iCampusManagerPlugin
{
    public partial class MessageAllUsage : BaseForm
    {
        BackgroundWorker BGW = new BackgroundWorker();

        public MessageAllUsage()
        {
            InitializeComponent();
        }

        private void MessageAllUsage_Load(object sender, EventArgs e)
        {
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
            BGW.DoWork += BGW_DoWork;
            dataGridViewX1.AutoGenerateColumns = false;

            BGW.RunWorkerAsync();
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<string, MessageAllCountRow> MessageDic = new Dictionary<string, MessageAllCountRow>();

            //取得發送數
            StringBuilder sql = new StringBuilder();
            sql.Append("select sum(total_target.發送數) as 發送數 from ( ");
            sql.Append("select notice.uid as target_uid,COUNT(notice_target.uid) as 發送數 from $ischool.1campus.notice notice ");
            sql.Append("join $ischool.1campus.notice_target notice_target on notice_target.ref_notice_id=notice.uid ");
            sql.Append("group by notice.uid ) total_target ");


            List<XElement> rsps = tool.MultiTasking_NowRun(sql.ToString(), SchoolPanel.SetSchoolPanel.SelectedSource);

            foreach (XElement rsp in rsps)
            {
                DataTable dt = tool.GetDateTable(rsp);
                string uid = rsp.AttributeText("UID");

                foreach (DataRow row in dt.Rows)
                {
                    MessageAllCountRow mcR = new MessageAllCountRow();
                    mcR.SchoolName = SchoolPanel.GlobalSchoolCache[uid].Title;
                    mcR.SchoolDSNS = "" + SchoolPanel.GlobalSchoolCache[uid].DSNS;

                    if (!MessageDic.ContainsKey(uid))
                    {
                        MessageDic.Add(uid, mcR);
                    }

                    int count = 0;
                    if (int.TryParse("" + row["發送數"], out count))
                    {
                        MessageDic[uid].Count = count;
                    }
                }
            }

            //取得已讀數
            StringBuilder sql_2 = new StringBuilder();
            sql_2.Append("select sum(notice_log.已讀數) as 已讀數 from ( ");
            sql_2.Append("select COUNT(notice_log.uid) as 已讀數 from $ischool.1campus.notice notice ");
            sql_2.Append("join $ischool.1campus.notice_log notice_log on notice_log.ref_notice_id=notice.uid ");
            sql_2.Append("group by notice_log.ref_parent_id,notice_log.ref_student_id,");
            sql_2.Append("notice_log.ref_teacher_id ) notice_log ");


            List<XElement> rsps_2 = tool.MultiTasking_NowRun(sql_2.ToString(), SchoolPanel.SetSchoolPanel.SelectedSource);

            foreach (XElement rsp in rsps_2)
            {
                DataTable dt = tool.GetDateTable(rsp);
                string uid = rsp.AttributeText("UID");

                foreach (DataRow row in dt.Rows)
                {
                    MessageAllCountRow mcR = new MessageAllCountRow();
                    mcR.SchoolName = SchoolPanel.GlobalSchoolCache[uid].Title;
                    mcR.SchoolDSNS = "" + SchoolPanel.GlobalSchoolCache[uid].DSNS;

                    if (!MessageDic.ContainsKey(uid))
                    {
                        MessageDic.Add(uid, mcR);
                    }

                    int count = 0;
                    if (int.TryParse("" + row["已讀數"], out count))
                    {
                        MessageDic[uid].Read = count;
                    }
                }
            }
            e.Result = MessageDic.Values.ToList();

        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    List<MessageAllCountRow> list = (List<MessageAllCountRow>)e.Result;

                    SortableBindingList<MessageAllCountRow> pk = new SortableBindingList<MessageAllCountRow>(list);
                    dataGridViewX1.DataSource = pk;
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

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    class MessageAllCountRow
    {
        /// <summary>
        /// 學校名稱
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 學校DSNS  
        /// </summary>
        public string SchoolDSNS { get; set; }


        public int Count { get; set; }

        public int Read { get; set; }
    }
}
