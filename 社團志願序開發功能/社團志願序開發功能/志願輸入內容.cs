using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using K12.Data;
using System.Xml;
using FISCA.DSAUtil;
using DevComponents.DotNetBar.Controls;
using K12.Club.Volunteer;
using FISCA.UDT;

namespace ClubPlugIn
{
    public partial class 志願輸入內容 : BaseForm
    {
        /// <summary>
        /// 社團名稱:社團
        /// </summary>
        Dictionary<string, CLUBRecord> ClubDic = new Dictionary<string, CLUBRecord>();

        public AccessHelper _A = new AccessHelper();

        List<CLUBRecord> ClubList { get; set; }

        int 學生選填志願數 = 0;

        public 志願輸入內容()
        {
            InitializeComponent();

            integerInput1.Value = int.Parse(School.DefaultSchoolYear);
            integerInput2.Value = int.Parse(School.DefaultSemester);
        }

        private void 志願輸入內容_Load(object sender, EventArgs e)
        {
            List<ConfigRecord> list1 = _A.Select<ConfigRecord>("config_name='學生選填志願數'");
            學生選填志願數 = 1;
            if (list1.Count > 0)
            {
                int a = 1;
                int.TryParse(list1[0].Content, out a);
                學生選填志願數 = a;
            }

            for (int x = 1; x <= 學生選填志願數; x++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);
                row.Cells[0].Value = "" + x;
                dataGridViewX1.Rows.Add(row);
            }

            ClubList = _A.Select<CLUBRecord>(string.Format("school_year={0} and semester={1}", integerInput1.Value, integerInput2.Value));

            foreach (CLUBRecord cr in ClubList)
            {
                if (!ClubDic.ContainsKey(cr.ClubName))
                {
                    ClubDic.Add(cr.ClubName, cr);
                }
            }

            Column1.DataSource = ClubList;
            Column1.DisplayMember = "ClubName";
        }

        /// <summary>
        /// 儲存
        /// </summary>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            int x = 1;
            DSXmlHelper dsx = new DSXmlHelper("xml");

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                    continue;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell is DataGridViewComboBoxExCell)
                    {
                        string name = "" + cell.Value;

                        if (ClubDic.ContainsKey(name))
                        {
                            CLUBRecord cr = ClubDic[name];

                            dsx.AddElement("Club");
                            dsx.SetAttribute("Club", "Index", x.ToString());
                            dsx.SetAttribute("Club", "Ref_Club_ID", cr.UID);
                            x++;
                        }
                    }
                }
            }

            List<string> list = K12.Presentation.NLDPanels.Student.SelectedSource;

            List<StudentRecord> StudentList = Student.SelectByIDs(list);

            List<VolunteerRecord> VolList = new List<VolunteerRecord>();

            List<VolunteerRecord> DelList = _A.Select<VolunteerRecord>(string.Format("ref_student_id in ('{0}')", string.Join("','", list)));

            foreach (StudentRecord stud in StudentList)
            {
                VolunteerRecord Vol = new VolunteerRecord();
                Vol.SchoolYear = integerInput1.Value;
                Vol.Semester = integerInput2.Value;
                Vol.RefStudentID = stud.ID;
                Vol.Content = dsx.BaseElement.OuterXml;

                VolList.Add(Vol);
            }

            _A.DeletedValues(DelList); //清掉選擇學生的社團記錄
            _A.InsertValues(VolList); //新增

            MsgBox.Show("儲存完成!!");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
