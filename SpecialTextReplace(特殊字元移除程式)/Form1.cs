using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using System.IO;
using System.Text.RegularExpressions;

namespace SpecialTextReplace
{
    public partial class Form1 : Form
    {
        BackgroundWorker BGW = new BackgroundWorker();


        Workbook book = new Workbook();
        OpenFileDialog ofd;

        int countError = 0;

        public Form1()
        {
            InitializeComponent();

            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);
        }

        #region 檢查錯誤內容

        private void button1_Click(object sender, EventArgs e)
        {
            countError = 0;
            dataGridViewX1.Rows.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            ofd = new OpenFileDialog();
            ofd.Filter = "Excel(*.xls)|*.xls"; //只能選Excel

            if (ofd.ShowDialog() == DialogResult.Cancel) return;

            book.Open(ofd.FileName);

            foreach (Worksheet sheet in book.Worksheets)
            {
                comboBox1.Items.Add(sheet.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                foreach (Worksheet sheet in book.Worksheets)
                {
                    if (comboBox1.Text == sheet.Name)
                    {
                        foreach (Cell each in sheet.Cells)
                        {
                            if (each.Row == 0)
                            {
                                comboBox2.Items.Add("" + each.Name[0]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 檢查所有內容
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (ofd.FileName != "")
            {
                if (!string.IsNullOrEmpty(comboBox2.Text))
                {
                    dataGridViewX1.Rows.Clear();

                    foreach (Worksheet sheet in book.Worksheets)
                    {
                        if (sheet.Name == comboBox1.Text)
                        {
                            foreach (Cell each in sheet.Cells)
                            {
                                if (each.Name[0].ToString() == comboBox2.Text)
                                {
                                    string a = "" + each.Value;
                                    foreach (char ct in a)
                                    {
                                        string r = Regex.Replace(ct.ToString(), @"[\W_]+", "");
                                        if (r != ct.ToString())
                                        {
                                            Change(ct.ToString(), each, sheet.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    if (dataGridViewX1.Rows.Count < 1)
                    {
                        MessageBox.Show("未發現特殊字元!!");
                    }
                    else
                    {
                        label2.Text = "錯誤數量：" + countError;
                    }
                }
            }
            else
            {
                MessageBox.Show("請選擇檔案!");
            }
        }

        /// <summary>
        /// 檢查所有內容
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();

            if (ofd.FileName != "")
            {
                foreach (Worksheet sheet in book.Worksheets)
                {
                    if (sheet.Name == comboBox1.Text)
                    {
                        foreach (Cell each in sheet.Cells)
                        {
                            string a = "" + each.Value;
                            foreach (char ct in a)
                            {
                                string r = Regex.Replace(ct.ToString(), @"[\W_]+", "");
                                if (r != ct.ToString())
                                {
                                    Change(ct.ToString(), each, sheet.Name);
                                }
                            }
                        }
                    }

                    if (dataGridViewX1.Rows.Count < 1)
                    {
                        MessageBox.Show("未發現特殊字元!!");
                    }
                    else
                    {
                        label2.Text = "錯誤數量：" + countError;
                    }
                }
            }
            else
            {
                MessageBox.Show("請選擇檔案!");
            }
        }

        private void Change(string _char, Cell cell, string sheetname)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);

            if (_char.Contains("\b"))
            {
                row.Cells[0].Value = "\\b"; //錯誤字元
            }
            else if (_char.Contains("\n"))
            {
                row.Cells[0].Value = "\\n"; //錯誤字元
            }
            else if (_char.Contains("$"))
            {
                row.Cells[0].Value = "$"; //錯誤字元
            }
            else if (_char.Contains("\0"))
            {
                row.Cells[0].Value = "\\0"; //錯誤字元
            }
            else if (_char.Contains("&amp"))
            {
                row.Cells[0].Value = "&amp"; //錯誤字元
            }
            else if (_char.Contains("\u0002"))
            {
                row.Cells[0].Value = "\\u0002"; //錯誤字元
            }
            else
            {
                row.Cells[0].Value = _char; //錯誤字元
            }

            row.Cells[1].Value = sheetname; //工作表名稱
            row.Cells[2].Value = cell.Row + 1; //Row index
            row.Cells[3].Value = cell.Name; //Column index
            row.Cells[4].Value = cell.Value; //原有欄位內容

            dataGridViewX1.Rows.Add(row);
            countError++;
        }

        #endregion

        #region 開始清除錯誤

        private void button2_Click(object sender, EventArgs e)
        {
            countError = 0;
            dataGridViewX1.Rows.Clear();

            if (!BGW.IsBusy)
            {
                this.Text = "特殊字元移除程式(開始執行清除任務...)";
                BGW.RunWorkerAsync();
            }
        }

        #endregion

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> LIST = new List<DataGridViewRow>();
            foreach (Worksheet sheet in book.Worksheets)
            {
                foreach (Cell each in sheet.Cells)
                {
                    string a = "" + each.Value;
                    if (a.Contains("\b"))
                    {
                        DataGridViewRow row = ChangeNow("\b", "\\b", each, sheet.Name);
                        if (row != null)
                            LIST.Add(row);
                    }
                    else if (a.Contains("\n"))
                    {
                        DataGridViewRow row = ChangeNow("\n", "\\n", each, sheet.Name);
                        if (row != null)
                            LIST.Add(row);
                    }
                    else if (a.Contains("$"))
                    {
                        DataGridViewRow row = ChangeNow("$", "$", each, sheet.Name);
                        if (row != null)
                            LIST.Add(row);
                    }
                    else if (a.Contains("\0"))
                    {
                        DataGridViewRow row = ChangeNow("\0", "\\0", each, sheet.Name);
                        if (row != null)
                            LIST.Add(row);
                    }
                    else if (a.Contains("&amp"))
                    {
                        DataGridViewRow row = ChangeNow("&amp", "&amp", each, sheet.Name);
                        if (row != null)
                            LIST.Add(row);
                    }
                    else if (a.Contains("\u0002")) //特殊案例
                    {
                        DataGridViewRow row = ChangeNow("\u0002", "\\u0002", each, sheet.Name);
                        if (row != null)
                            LIST.Add(row);
                    }

                }
            }

            e.Result = LIST;
        }

        private DataGridViewRow ChangeNow(string char1, string Char2, Cell cell, string sheetname)
        {
            string a = "" + cell.Value;
            if (a.Contains(char1))
            {
                cell.PutValue(a.Replace(char1, ""));

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);
                row.Cells[0].Value = Char2; //錯誤字元
                row.Cells[1].Value = sheetname; //工作表名稱
                row.Cells[2].Value = cell.Row; //Row index
                row.Cells[3].Value = cell.Column; //Column index
                row.Cells[4].Value = cell.Value; //現在欄位內容
                countError++;

                return row;
            }
            else
            {
                return null;
            }
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.Text = "特殊字元移除程式(已清除完成)";
                List<DataGridViewRow> LIST = (List<DataGridViewRow>)e.Result;
                if (LIST.Count != 0)
                {
                    dataGridViewX1.Rows.AddRange(LIST.ToArray());
                }

                string filename = ofd.FileName.Replace(".xls", "");
                this.Text = "特殊字元移除程式(儲存資料中...)";
                book.Save(filename + "(特殊字元已處理)" + ".xls");
                MessageBox.Show("特殊字元已處理!!");
                label2.Text = "修正錯誤數量：" + countError;
                this.Text = "特殊字元移除程式(特殊字元已處理!!)";
            }
            else
            {
                this.Text = "特殊字元移除程式(發生錯誤...)";
                MessageBox.Show("特殊字元處理發生錯誤!!\n" + e.Error.Message);
                label2.Text = "修正錯誤數量：null";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();
        }
    }
}
