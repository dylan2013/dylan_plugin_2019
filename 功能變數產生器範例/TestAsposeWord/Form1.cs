using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Words;
using System.IO;
using Aspose.Cells;
using FISCA.Presentation.Controls;

namespace TestAsposeWord
{
    public partial class Form1 : BaseForm
    {
        Dictionary<string, mapObj> mapDict = new Dictionary<string, mapObj>();

        Dictionary<string, int> IndexDic = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();

            Aspose.Words.License li = new Aspose.Words.License();
            li.SetLicense(new MemoryStream(Properties.Resources.Aspose_Total));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //依據設定檔(Book1.xls)
            //建立畫面預設資料
            Aspose.Cells.Workbook book = new Workbook();
            book.Open(Application.StartupPath + "\\Book1.xls");

            for (int x = 0; x <= book.Worksheets[0].Cells.MaxDataRow; x++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);

                for (int y = 0; y <= book.Worksheets[0].Cells.MaxDataColumn; y++)
                {
                    row.Cells[y].Value = book.Worksheets[0].Cells[x, y].Value;
                }
                dataGridViewX1.Rows.Add(row);
            }

            建立畫面預設資料();
        }

        private void 建立畫面預設資料()
        {
            RunAddView();
        }

        /// <summary>
        /// 產生文件檔
        /// </summary>
        private void StartRun_Click(object sender, EventArgs e)
        {
            列印前回存範例檔案();

            建立文件();
        }

        private void 列印前回存範例檔案()
        {
            Aspose.Cells.Workbook book = new Workbook();
            book.Open(Application.StartupPath + "\\Book1.xls");
            book.Worksheets.Clear(); //清除
            book.Worksheets.Add(); //新增

            int x = 0;
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                    continue;

                book.Worksheets[0].Cells[x, 0].PutValue(row.Cells[0].Value);
                book.Worksheets[0].Cells[x, 1].PutValue(row.Cells[1].Value);
                book.Worksheets[0].Cells[x, 2].PutValue(row.Cells[2].Value);
                x++;
            }

            book.Save(Application.StartupPath + "\\Book1.xls");
        }

        private void 建立文件()
        {
            建立參考資料物件();

            Aspose.Words.Document doc = new Document(new MemoryStream(Properties.Resources.SA1));

            Aspose.Words.DocumentBuilder bu = new DocumentBuilder(doc);

            if (!cbLateral.Checked)
            {
                #region 橫向產生

                bu.StartTable();
                foreach (KeyValuePair<string, mapObj> data in mapDict)
                {
                    bu.InsertCell();
                    bu.Write(data.Key);
                }

                bu.EndRow();

                for (int x = 1; x <= integerInput1.Value; x++)
                {
                    foreach (KeyValuePair<string, mapObj> data in mapDict)
                    {
                        string str = string.Format("MERGEFIELD {0}" + @"\* MERGEFORMAT", data.Value.內容 + textBoxX2.Text + x);

                        string str1 = "";
                        if (cbEasy.Checked)
                        {
                            str1 = string.Format("{0}", data.Value.外觀 + textBoxX2.Text + x);
                        }
                        else
                        {
                            str1 = string.Format("«{0}»", data.Value.外觀 + textBoxX2.Text + x);
                        }
                        bu.InsertCell();
                        bu.InsertField(str, str1);
                        //}
                    }
                    bu.EndRow();
                }

                #endregion
            }
            else
            {
                #region 直向產生

                if (integerInput1.Value > 20)
                {
                    MsgBox.Show("橫向產生資料請勿超過20列!!");
                    return;
                }


                bu.StartTable();
                foreach (KeyValuePair<string, mapObj> data in mapDict)
                {
                    bu.InsertCell();
                    bu.Write(data.Key);

                    for (int x = 1; x <= integerInput1.Value; x++)
                    {
                        string str = string.Format("MERGEFIELD {0}" + @"\* MERGEFORMAT", data.Value.內容 + textBoxX2.Text + x);
                        string str1 = "";
                        if (cbEasy.Checked)
                        {
                            str1 = string.Format("{0}", data.Value.外觀 + textBoxX2.Text + x);
                        }
                        else
                        {
                            str1 = string.Format("«{0}»", data.Value.外觀 + textBoxX2.Text + x);
                        }
                        bu.InsertCell();
                        bu.InsertField(str, str1);
                    }
                    bu.EndRow();
                }

                #endregion
            }

            //儲存檔案與開啟檔案
            try
            {
                int FileName = 1;
                string path = "";
                for (int forX = 1; forX < 99; forX++)
                {
                    path = string.Format(Application.StartupPath + "\\akbbb{0}.doc", FileName);
                    if (!File.Exists(path))
                    {
                        break;
                    }
                    FileName++;
                }

                doc.Save(path);
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MsgBox.Show("檔案開啟中,請關閉檔案再試一次!!\n" + ex.Message);
            }

        }

        void RunAddView()
        {
            建立參考資料物件();

            dataGridViewX2.Rows.Clear();
            dataGridViewX2.Columns.Clear();

            if (!cbLateral.Checked)//橫向產生
            {
                #region 處理Column

                //這裡ColumnName 就是Title名稱
                int columnIndex = 1;
                foreach (DataGridViewRow defRow in dataGridViewX1.Rows)
                {
                    if (defRow.IsNewRow)
                        continue;

                    string 標題 = "" + defRow.Cells[0].Value;

                    //處理標題
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.Name = 標題;
                    col.HeaderText = 標題;
                    dataGridViewX2.Columns.AddRange(col);
                    columnIndex++;
                }

                #endregion

                #region 處理內文

                int columnX = 1;

                //共幾行

                //每一個範例行
                for (int j = 1; j <= integerInput1.Value; j++)
                {
                    int cell_Column_Index = 0;

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridViewX2);

                    foreach (DataGridViewRow defRow in dataGridViewX1.Rows)
                    {
                        if (defRow.IsNewRow)
                            continue;

                        string 顯示 = "";

                        //取得內文名稱
                        if (!cbViewX.Checked)
                        {
                            顯示 = string.Format("{0}", "" + defRow.Cells[1].Value);
                            if (!cbEasy.Checked)
                                row.Cells[cell_Column_Index].Value = "«" + 顯示 + textBoxX2.Text + columnX + "»";
                            else
                                row.Cells[cell_Column_Index].Value = 顯示 + textBoxX2.Text + columnX;
                        }
                        else
                        {
                            顯示 = string.Format("{0}", "" + defRow.Cells[2].Value);
                            row.Cells[cell_Column_Index].Value = 顯示 + textBoxX2.Text + columnX;
                        }

                        cell_Column_Index++;
                    }

                    dataGridViewX2.Rows.Add(row);

                    columnX++;
                }

                #endregion
            }
            else //直向產生
            {
                #region 處理column數量

                for (int columnX = 1; columnX <= integerInput1.Value + 1; columnX++)
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.Name = "A" + columnX;
                    col.HeaderText = "A" + columnX;
                    dataGridViewX2.Columns.AddRange(col);
                }

                #endregion

                #region 處理內文

                foreach (DataGridViewRow defRow in dataGridViewX1.Rows)
                {
                    if (defRow.IsNewRow)
                        continue;

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridViewX2);
                    row.Cells[0].Value = defRow.Cells[0].Value;

                    //取得內文名稱

                    int columnIndex = 1;
                    for (int columnX = 1; columnX <= integerInput1.Value; columnX++)
                    {

                        if (!cbViewX.Checked)
                        {
                            string 顯示 = string.Format("{0}", "" + defRow.Cells[1].Value);
                            if (!cbEasy.Checked)
                                row.Cells[columnIndex].Value = "«" + 顯示 + textBoxX2.Text + columnX + "»";
                            else
                                row.Cells[columnIndex].Value = 顯示 + textBoxX2.Text + columnX;
                        }
                        else
                        {
                            string 顯示 = string.Format("{0}", "" + defRow.Cells[2].Value);
                            row.Cells[columnIndex].Value = 顯示 + textBoxX2.Text + columnX;
                        }
                        columnIndex++;
                    }

                    dataGridViewX2.Rows.Add(row);
                }

                #endregion
            }
        }

        private void 建立參考資料物件()
        {
            mapDict.Clear();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                    continue;

                mapObj obj = new mapObj();
                obj.標題 = "" + row.Cells[0].Value;
                obj.外觀 = "" + row.Cells[1].Value;
                obj.內容 = "" + row.Cells[2].Value;

                if (!mapDict.ContainsKey("" + row.Cells[0].Value))
                {
                    mapDict.Add("" + row.Cells[0].Value, obj);
                }
                else
                {
                    MsgBox.Show("標題列重覆!!");
                }
            }
        }

        #region 畫面更新

        private void dataGridViewX1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            RunAddView();
        }

        private void dataGridViewX1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RunAddView();
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            RunAddView();
        }

        private void integerInput1_ValueChanged(object sender, EventArgs e)
        {
            RunAddView();
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLateral.Checked)
            {
                lbHelp3.Text = "產生列數：";
                cbLateral.Text = "橫向產生變數(上限20列)";
                integerInput1.MaxValue = 20;
                integerInput1.Value = 20;
            }
            else
            {
                lbHelp3.Text = "產生行數：";
                cbLateral.Text = "橫向產生變數";
                integerInput1.MaxValue = 999;
            }
            RunAddView();
        }

        private void cbViewX_CheckedChanged(object sender, EventArgs e)
        {
            RunAddView();
        }

        private void cbEasy_CheckedChanged(object sender, EventArgs e)
        {
            RunAddView();
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "開啟設定檔";
            ofd.Filter = "Word 檔案 (*.xls)|*.xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataGridViewX1.Rows.Clear();

                    //依據設定檔(Book1.xls)
                    //建立畫面預設資料
                    Aspose.Cells.Workbook book = new Workbook();
                    book.Open(ofd.FileName);

                    for (int x = 0; x <= book.Worksheets[0].Cells.MaxDataRow; x++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridViewX1);

                        for (int y = 0; y <= book.Worksheets[0].Cells.MaxDataColumn; y++)
                        {
                            row.Cells[y].Value = book.Worksheets[0].Cells[x, y].Value;
                        }
                        dataGridViewX1.Rows.Add(row);
                    }

                    建立畫面預設資料();
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 資料模型
    /// </summary>
    class mapObj
    {
        public string 標題 { get; set; }
        public string 外觀 { get; set; }
        public string 內容 { get; set; }
    }
}
