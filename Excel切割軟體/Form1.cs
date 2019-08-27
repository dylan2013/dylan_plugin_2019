using Aspose.Cells;
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

namespace SuperParse
{
    public partial class Form1 : BaseForm
    {
        Workbook work { get; set; }
        OpenFileDialog openfile { get; set; }
        public Form1()
        {
            InitializeComponent();

            System.IO.Stream stream = new System.IO.MemoryStream(Properties.Resources.Aspose_Total);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            new Aspose.Cells.License().SetLicense(stream);

            work = new Workbook();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            openfile = new OpenFileDialog();
            openfile.Multiselect = false;
            openfile.Filter = "xls files (*.xls)|*.xls";
            openfile.ShowDialog();

            work = new Workbook();
            work.Open(openfile.FileName);

            foreach (Cell cell in work.Worksheets[0].Cells)
            {
                if (cell.Row == 0)
                {
                    comboBoxEx1.Items.Add(cell.Column);
                }
            }

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Range prototypeHeader = work.Worksheets[0].Cells.CreateRange(0, 1, false);


            Workbook run = new Workbook();
            Worksheet run_sheer = run.Worksheets[0];
            //建立抬頭
            run_sheer.Cells.CreateRange(0, 1, false).Copy(prototypeHeader);

            Byte ColumnIndex = Byte.Parse(comboBoxEx1.SelectedItem.ToString());

            //資料定位
            int rowIndex = 1;

            //資料來源
            int sheetRowIndex = 1;

            foreach (Cell cell in work.Worksheets[0].Cells)
            {
                if (cell.Row == 0)
                    continue;

                if (cell.Row == sheetRowIndex)
                {
                    //切割單位
                    string[] SplitList = work.Worksheets[0].Cells[sheetRowIndex, ColumnIndex].Value.ToString().Split(textBoxX1.Text[0]);

                    //取得要複製的欄位
                    Range r = work.Worksheets[0].Cells.CreateRange(sheetRowIndex, 1, false);

                    foreach (string each in SplitList)
                    {
                        //在新的Sheet建立欄位
                        run_sheer.Cells.CreateRange(rowIndex, 1, false).Copy(r);
                        run_sheer.Cells[rowIndex, ColumnIndex].PutValue(each);
                        rowIndex++;
                    }
                    sheetRowIndex++;
                }
            }


            run.Save(openfile.FileName + "Copy.xls");

            MsgBox.Show("轉檔完成\n確認後開啟檔案!!");

            System.Diagnostics.Process.Start(openfile.FileName + "Copy.xls");


        }
    }
}
