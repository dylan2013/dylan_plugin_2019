using FISCA.DSAUtil;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using K12.Club.Volunteer;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClubPlugIn
{
    public partial class 批次志願功能 : BaseForm
    {
        public AccessHelper _A = new AccessHelper();

        List<CLUBRecord> ClubList { get; set; }

        int 學生選填志願數 = 0;

        public 批次志願功能()
        {
            InitializeComponent();


            List<ConfigRecord> list1 = _A.Select<ConfigRecord>("config_name='學生選填志願數'");
            學生選填志願數 = 1;
            if (list1.Count > 0)
            {
                int a = 1;
                int.TryParse(list1[0].Content, out a);
                學生選填志願數 = a;
            }

            integerInput1.Value = int.Parse(School.DefaultSchoolYear);
            integerInput2.Value = int.Parse(School.DefaultSemester);

            ClubList = _A.Select<CLUBRecord>(string.Format("school_year={0} and semester={1}", integerInput1.Value, integerInput2.Value));

        }

        /// <summary>
        /// 全校亂數選社
        /// </summary>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            //學生
            List<StudentRecord> StudentList = new List<StudentRecord>();
            foreach (StudentRecord each in Student.SelectAll())
            {
                if (each.Status == StudentRecord.StudentStatus.一般 || each.Status == StudentRecord.StudentStatus.延修)
                {
                    StudentList.Add(each);
                }
            }

            //亂數
            RandomStudent<StudentRecord>(StudentList);
            List<VolunteerRecord> VolList = new List<VolunteerRecord>();
            foreach (StudentRecord each in StudentList)
            {
                //亂數
                RandomStudent<CLUBRecord>(ClubList);

                VolunteerRecord vr = new VolunteerRecord();
                vr.RefStudentID = each.ID;
                vr.SchoolYear = integerInput1.Value;
                vr.Semester = integerInput2.Value;

                DSXmlHelper dsx = new DSXmlHelper("xml");
                int x = 1;
                foreach (CLUBRecord club in ClubList)
                {
                    if (學生選填志願數 >= x)
                    {
                        dsx.AddElement("Club");
                        dsx.SetAttribute("Club", "Index", x.ToString());
                        dsx.SetAttribute("Club", "Ref_Club_ID", club.UID);
                        x++;
                    }
                    else
                        break;
                }
                vr.Content = dsx.BaseElement.OuterXml;
                VolList.Add(vr);
            }

            _A.InsertValues(VolList);

        }

        /// <summary>
        /// 亂數儀
        /// </summary>
        private void RandomStudent<T>(List<T> sList)
        {
            List<T> temp = new List<T>(sList);
            Random r = new Random();

            for (int i = 0; i < sList.Count; i++)
            {
                if (i + 1 == sList.Count)
                    break;

                int rnd = r.Next(i + 1, sList.Count - 1);
                T tmp = sList[rnd];
                sList[rnd] = sList[i];
                sList[i] = tmp;

            }
        }

        /// <summary>
        /// 清除社團參與記錄
        /// </summary>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            List<string> list2 = new List<string>();
            List<CLUBRecord> list1 = _A.Select<CLUBRecord>(string.Format("school_year={0} and semester={1}", integerInput1.Value.ToString(), integerInput2.Value.ToString()));

            foreach (CLUBRecord each in list1)
            {
                list2.Add(each.UID);
            }
            List<SCJoin> list3 = _A.Select<SCJoin>("ref_club_id in ('" + string.Join("','", list2) + "')");

            _A.DeletedValues(list3);
        }

        /// <summary>
        /// 清除志願序記錄
        /// </summary>
        private void buttonX3_Click(object sender, EventArgs e)
        {
            List<VolunteerRecord> list = _A.Select<VolunteerRecord>(string.Format("school_year={0} and semester={1}", integerInput1.Value.ToString(), integerInput2.Value.ToString()));
            _A.DeletedValues(list);
        }
    }
}
