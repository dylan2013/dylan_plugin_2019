using FISCA.Presentation.Controls;
using iCampusManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Z_EightDomainEdit
{
    public partial class DomainForm : BaseForm
    {

        public DomainForm()
        {
            InitializeComponent();
        }

        private void DomainForm_Load(object sender, EventArgs e)
        {
            if (SchoolPanel.SetSchoolPanel.SelectedSource.Count > 0)
            {
                StringBuilder sql_2 = new StringBuilder();
                sql_2.Append("select * from list  where name='JHEvaluation_Subject_Ordinal'");
                List<XElement> rsps_2 = tool.MultiTasking_NowRun(sql_2.ToString(), SchoolPanel.SetSchoolPanel.SelectedSource);

                foreach (XElement each in rsps_2)
                {
                    //取得Record -> Column
                    if (each.Element("Record") != null)
                    {
                        List<XElement> a = each.Element("Record").Elements("Column").ToList();
                        if (each.Element("Record").Elements("Column") != null)
                        {
                            string UID = each.Attribute("UID").Value;

                            XElement xml = a[2];

                            string SchoolName = SchoolPanel.GlobalSchoolCache[UID].Title;
                            string SchoolDSNS = "" + SchoolPanel.GlobalSchoolCache[UID].DSNS;

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridViewX1);
                            row.Cells[0].Value = UID;
                            row.Cells[1].Value = SchoolDSNS;
                            row.Cells[2].Value = SchoolName;
                            row.Cells[3].Value = xml.Value;
                            row.Cells[4].Value = "";

                            dataGridViewX1.Rows.Add(row);
                        }
                    }
                }
            }
        }

        //開始資料更新
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dr = MsgBox.Show("您確定要進行領域更新?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                //儲存更新前內容
                foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
                {
                    string x = "" + row.Cells[3].Value;
                    File.WriteAllText("" + row.Cells[2].Value + "_原檔.xml", x);

                    string y = "" + row.Cells[4].Value;
                    File.WriteAllText("" + row.Cells[2].Value + "_新檔.xml", y);
                }

                //更新動作
                foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
                {
                    try
                    {
                        StringBuilder sql_3 = new StringBuilder();
                        sql_3.Append(string.Format("UPDATE list  SET content='{0}' WHERE name='JHEvaluation_Subject_Ordinal' returning list_id", "" + row.Cells[4].Value));
                        List<XElement> rsps_3 = tool.MultiTasking_NowRun(sql_3.ToString(), new List<string>() { "" + row.Cells[0].Value });
                        row.Cells[5].Value = "成功";
                    }
                    catch (Exception ex)
                    {
                        row.Cells[5].Value = "錯誤:" + ex.Message;
                    }
                }
            }
        }

        //離開
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveInFile_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                //資料
                try
                {
                    string x = "" + row.Cells[3].Value;
                    File.WriteAllText("" + row.Cells[2].Value + "_原檔.xml", x);

                    string y = "" + row.Cells[4].Value;
                    File.WriteAllText("" + row.Cells[2].Value + "_新檔.xml", y);

                }
                catch
                {
                    MsgBox.Show("檔案儲存錯誤!!");
                }
            }
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if ("" + dataGridViewX1.Rows[e.RowIndex].Cells[4].Value != "")
                {
                    SchoolXmlParse scForm = new SchoolXmlParse(dataGridViewX1.Rows[e.RowIndex]);
                    scForm.ShowDialog();
                }
                else
                {
                    MsgBox.Show("請先指定領域類型(國中/國小)");
                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();

            if (SchoolPanel.SetSchoolPanel.SelectedSource.Count > 0)
            {
                StringBuilder sql_2 = new StringBuilder();
                sql_2.Append("select * from list  where name='JHEvaluation_Subject_Ordinal'");
                List<XElement> rsps_2 = tool.MultiTasking_NowRun(sql_2.ToString(), SchoolPanel.SetSchoolPanel.SelectedSource);

                foreach (XElement each in rsps_2)
                {
                    //取得Record -> Column
                    if (each.Element("Record") != null)
                    {
                        if (each.Element("Record").Elements("Column") != null)
                        {
                            List<XElement> a = each.Element("Record").Elements("Column").ToList();
                            string UID = each.Attribute("UID").Value;

                            XElement xml = a[2];


                            //先將String 轉 Xml
                            XElement Configuration = XElement.Parse(xml.Value);

                            //取得DomainOrdinal

                            XElement DomainOrdinal = Configuration.Elements("Configuration").ToList()[1];
                            //先將String 轉 Xml
                            XElement xml2 = XElement.Parse(DomainOrdinal.Value);

                            /*
                           * 條件:
                           * 1.確認是否有自然科學
                           * 2.社會之後,增加自然科學

                           * 1.確認有沒有藝術
                           * 2.自然與生活科技之後,增加藝術
                            */

                            bool checkPoint1 = false;
                            bool checkPoint2 = false;
                            foreach (XElement domain in xml2.Elements("Domain").ToList())
                            {
                                var name = domain.Attribute("Name");
                                if (name.Value == "自然科學")
                                {
                                    checkPoint1 = true;
                                }

                                if (name.Value == "藝術")
                                {
                                    checkPoint2 = true;
                                }
                            }

                            //Add自然科學
                            if (!checkPoint1)
                            {
                                foreach (XElement domain in xml2.Elements("Domain").ToList())
                                {
                                    var name = domain.Attribute("Name");
                                    if (name.Value == "社會")
                                    {
                                        var new1 = new XElement("Domain");
                                        new1.SetAttributeValue("Group", "");
                                        new1.SetAttributeValue("Name", "自然科學");
                                        new1.SetAttributeValue("EnglishName", "Natural Science");

                                        domain.AddAfterSelf(new1);
                                    }
                                }
                            }

                            //Add藝術
                            if (!checkPoint2)
                            {
                                foreach (XElement domain in xml2.Elements("Domain").ToList())
                                {
                                    var name = domain.Attribute("Name");
                                    if (name.Value == "自然與生活科技")
                                    {
                                        var new1 = new XElement("Domain");
                                        new1.SetAttributeValue("Group", "");
                                        new1.SetAttributeValue("Name", "藝術");
                                        new1.SetAttributeValue("EnglishName", "Arts");

                                        domain.AddAfterSelf(new1);
                                    }
                                }
                            }

                            //儲存回去
                            DomainOrdinal.SetValue(xml2.ToString(SaveOptions.DisableFormatting));

                            string SchoolName = SchoolPanel.GlobalSchoolCache[UID].Title;
                            string SchoolDSNS = "" + SchoolPanel.GlobalSchoolCache[UID].DSNS;

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridViewX1);
                            row.Cells[0].Value = UID;
                            row.Cells[1].Value = SchoolDSNS;
                            row.Cells[2].Value = SchoolName;
                            row.Cells[3].Value = xml.Value;
                            row.Cells[4].Value = Configuration.ToString(SaveOptions.DisableFormatting);

                            dataGridViewX1.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private void btnChange2_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();

            if (SchoolPanel.SetSchoolPanel.SelectedSource.Count > 0)
            {
                StringBuilder sql_2 = new StringBuilder();
                sql_2.Append("select * from list  where name='JHEvaluation_Subject_Ordinal'");
                List<XElement> rsps_2 = tool.MultiTasking_NowRun(sql_2.ToString(), SchoolPanel.SetSchoolPanel.SelectedSource);


                /* XML回傳結構 - Dylan
    <Response UID="1683">
        <Metadata>
            <Column Type="Integer" Index="1" Field="list_id"/>
            <Column Type="String" Index="2" Field="name"/>
            <Column Type="String" Index="3" Field="content"/>
        </Metadata>
        <Record>
            <Column Index="1">141</Column>
            <Column Index="2">JHEvaluation_Subject_Ordinal</Column>
            <Column Index="3">
                <Configurations>
                    <Configuration Name="SubjectOrdinal">&lt;Subjects&gt;&lt;Subject Name="國語文"....</Configuration>
                    <Configuration Name="DomainOrdinal">&lt;Domains&gt;&lt;Dom....</Configuration>
                </Configurations>
            </Column>
        </Record>
    </Response>
                 */

                foreach (XElement each in rsps_2)
                {
                    if (each.Element("Record") != null)
                    {
                        if (each.Element("Record").Elements("Column") != null)
                        {
                            //取得Record -> Column
                            List<XElement> a = each.Element("Record").Elements("Column").ToList();
                            string UID = each.Attribute("UID").Value;

                            XElement xml = a[2];

                            //先將String 轉 Xml
                            XElement Configuration = XElement.Parse(xml.Value);

                            //取得DomainOrdinal

                            XElement DomainOrdinal = Configuration.Elements("Configuration").ToList()[1];
                            //先將String 轉 Xml
                            XElement xml2 = XElement.Parse(DomainOrdinal.Value);

                            /*
            <Domains>
                <Domain Group="語文" Name="國語文" EnglishName="ChineseMM"/>
                <Domain Group="語文" Name="英語" EnglishName="English"/>
                <Domain Group="" Name="數學" EnglishName="Mathematics"/>
                <Domain Group="" Name="社會" EnglishName="Social Studies"/>
                new  <Domain Group="" Name="自然科學" EnglishName="Natural Science"/>
                <Domain Group="" Name="自然與生活科技" EnglishName="Science And Technology"/>
                new  <Domain Group="" Name="藝術" EnglishName="Art"/>
                <Domain Group="" Name="藝術與人文" EnglishName="Arts And Humanities"/>
                <Domain Group="" Name="健康與體育" EnglishName="Health And Physical Education"/>
                <Domain Group="" Name="綜合活動" EnglishName="Integrative Activities"/>
                new   <Domain Group="" Name="科技" EnglishName="Social Studies"/>
                <Domain Group="" Name="實用語文" EnglishName="P Test"/>
                <Domain Group="" Name="實用數學" EnglishName="M Test"/>
                <Domain Group="" Name="社會適應" EnglishName=""/>
                <Domain Group="" Name="生活教育" EnglishName=""/>
                <Domain Group="" Name="休閒教育" EnglishName=""/>
                <Domain Group="" Name="職業教育" EnglishName=""/>
                <Domain Group="" Name="特殊需求" EnglishName=""/>
            </Domains>
                            */

                            /*
                           * 條件:
                           * 1.確認是否有自然科學
                           * 2.社會之後,增加自然科學

                           * 1.確認有沒有藝術
                           * 2.自然與生活科技之後,增加藝術

                           * 1.確認是否有科技
                           * 2.將原本科技保留移出
                           * 3.並於綜合活動之後增加科技
                            */

                            bool checkPoint1 = false;
                            bool checkPoint2 = false;
                            bool checkPoint3 = false;
                            foreach (XElement domain in xml2.Elements("Domain").ToList())
                            {
                                var name = domain.Attribute("Name");
                                if (name.Value == "自然科學")
                                {
                                    checkPoint1 = true;
                                }

                                if (name.Value == "藝術")
                                {
                                    checkPoint2 = true;
                                }

                                if (name.Value == "科技")
                                {
                                    checkPoint3 = true;
                                }
                            }

                            //Add自然科學
                            if (!checkPoint1)
                            {
                                foreach (XElement domain in xml2.Elements("Domain").ToList())
                                {
                                    var name = domain.Attribute("Name");
                                    if (name.Value == "社會")
                                    {
                                        var new1 = new XElement("Domain");
                                        new1.SetAttributeValue("Group", "");
                                        new1.SetAttributeValue("Name", "自然科學");
                                        new1.SetAttributeValue("EnglishName", "Natural Science");

                                        domain.AddAfterSelf(new1);
                                    }
                                }
                            }

                            //Add藝術
                            if (!checkPoint2)
                            {
                                foreach (XElement domain in xml2.Elements("Domain").ToList())
                                {
                                    var name = domain.Attribute("Name");
                                    if (name.Value == "自然與生活科技")
                                    {
                                        var new1 = new XElement("Domain");
                                        new1.SetAttributeValue("Group", "");
                                        new1.SetAttributeValue("Name", "藝術");
                                        new1.SetAttributeValue("EnglishName", "Arts");

                                        domain.AddAfterSelf(new1);
                                    }
                                }
                            }

                            //如果沒有科技
                            if (!checkPoint3)
                            {
                                foreach (XElement domain in xml2.Elements("Domain").ToList())
                                {
                                    var name = domain.Attribute("Name");
                                    if (name.Value == "綜合活動")
                                    {
                                        var new1 = new XElement("Domain");
                                        new1.SetAttributeValue("Group", "");
                                        new1.SetAttributeValue("Name", "科技");
                                        new1.SetAttributeValue("EnglishName", "Technology");

                                        domain.AddAfterSelf(new1);
                                    }
                                }
                            }
                            else
                            {
                                //有科技的處理
                                //1.將原本科技予以移除,並保存
                                //2.將科技放置於綜合活動之下
                                foreach (XElement domain in xml2.Elements("Domain").ToList())
                                {
                                    var name = domain.Attribute("Name");
                                    if (name.Value == "科技")
                                    {
                                        domain.Remove();
                                    }
                                }

                                //加入科技
                                foreach (XElement domain in xml2.Elements("Domain").ToList())
                                {
                                    var name = domain.Attribute("Name");
                                    if (name.Value == "綜合活動")
                                    {
                                        var new1 = new XElement("Domain");
                                        new1.SetAttributeValue("Group", "");
                                        new1.SetAttributeValue("Name", "科技");
                                        new1.SetAttributeValue("EnglishName", "Technology");

                                        domain.AddAfterSelf(new1);
                                    }
                                }
                            }

                            //儲存回去
                            DomainOrdinal.SetValue(xml2.ToString(SaveOptions.DisableFormatting));


                            string SchoolName = SchoolPanel.GlobalSchoolCache[UID].Title;
                            string SchoolDSNS = "" + SchoolPanel.GlobalSchoolCache[UID].DSNS;

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridViewX1);
                            row.Cells[0].Value = UID;
                            row.Cells[1].Value = SchoolDSNS;
                            row.Cells[2].Value = SchoolName;
                            row.Cells[3].Value = xml.Value;
                            row.Cells[4].Value = Configuration.ToString(SaveOptions.DisableFormatting);

                            dataGridViewX1.Rows.Add(row);
                        }
                    }
                }
            }
        }
    }
}
