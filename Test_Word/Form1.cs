using Aspose.Words;
using Aspose.Words.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_Word
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            //2019/12/23 - 測試為何欄位的寬,無法調整
            //造成確認表跑版

            //PS:Aspose.Words.dll是2014版
            //因為ischool指認識這個檔名
            Document _doc = new Document(new MemoryStream(Properties.Resources.資料));

            DocumentBuilder docBuilder = new DocumentBuilder(_doc);
            docBuilder.MoveToMergeField("資料");
            Cell _cell = docBuilder.CurrentParagraph.ParentNode as Cell;

            #region Cell切割器

            double MAXwidth = _cell.CellFormat.Width;
            double Cellwidth = MAXwidth / 3;

            CellFormat cf = _cell.CellFormat;
            cf.Width = Cellwidth;

            List<Cell> list = new List<Cell>();
            list.Add(_cell);

            Row _row = _cell.ParentNode as Row;

            for (int xx = 0; xx < 3 - 1; xx++)
            {
                list.Add((_row.InsertAfter(new Cell(_cell.Document), _cell)) as Cell);
            }

            foreach (Cell each in list)
            {
                Console.WriteLine(each.CellFormat.Width);
            }

            foreach (Cell each in list)
            {
                each.CellFormat.PreferredWidth = PreferredWidth.FromPoints(Cellwidth);
            }

            #endregion


            SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
            sd.Title = "另存新檔";
            sd.FileName = "資料(確認表).docx";
            sd.Filter = "Word檔案 (*.Docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _doc.Save(sd.FileName);
                    System.Diagnostics.Process.Start(sd.FileName);

                }
                catch
                {
                    MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
