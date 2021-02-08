using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();

            //合併欄位
            wb.Worksheets[0].Cells.Merge(0, 0, 1, 30);

            labelX1.Text = StringSplit(labelX1.Text, 12);

            //SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
            //sd.Title = "另存新檔";
            //sd.FileName = "日常生活表現總表.xls";
            //sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            //if (sd.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        wb.Save(sd.FileName, FileFormatType.Excel2003);
            //        System.Diagnostics.Process.Start(sd.FileName);

            //    }
            //    catch
            //    {
            //        MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    }
            //}
        }

        /// <summary>
        /// 字串斷行器
        /// </summary>
        /// <param name="message">文字內容</param>
        /// <param name="SplitLength">分割長度</param>
        /// <returns></returns>
        public string StringSplit(string message, int SplitLength)
        {
            int 總長度 = message.Length;
            int 每一份幾個文字 = SplitLength;
            int 共幾份 = 總長度 / 每一份幾個文字;
            int 餘數 = 總長度 % 每一份幾個文字;

            bool check = false;
            if (總長度 % 每一份幾個文字 != 0)
            {
                check = true;
            }

            //結果文字
            string 結果文字 = "";

            int 節點 = 0;
            for (int x = 0; x <= 共幾份; x++)
            {
                if (x == 共幾份)
                {
                    if (check)
                    {
                        結果文字 += labelX1.Text.Substring(節點, 餘數);
                    }
                }
                else
                {
                    結果文字 += labelX1.Text.Substring(節點, 每一份幾個文字);
                    結果文字 += "\r\n"; //加入分行符號
                    節點 += 每一份幾個文字;
                }
            }

            return 結果文字;
        }
    }
}
