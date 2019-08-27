using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iCampusManagerPlugin
{
    //撰寫一個可以編輯版本編號的空間
    public partial class AppVerForm : BaseForm
    {

        FISCA.UDT.AccessHelper _A = new FISCA.UDT.AccessHelper();

        public AppVerForm()
        {
            InitializeComponent();


            //List<SchoolRecord> schoolList = _A.Select<SchoolRecord>(SchoolPanel.SetSchoolPanel.SelectedSource);
            //foreach (SchoolRecord school in schoolList)
            //{
            //    DataGridViewRow row = new DataGridViewRow();
            //    row.CreateCells(dataGridViewX1);

            //}


            List<AppItem> appList = _A.Select<AppItem>();

            foreach (AppItem item in appList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);

                row.Cells[colSchoolName.Index].Value = item.SchoolName; //學校名稱
                row.Cells[colAppName.Index].Value = item.AppName; //APP名稱

                row.Cells[colPackageName.Index].Value = item.PackageName; //PackageName
                row.Cells[colBundleID.Index].Value = item.BundleID; //BundleID

                row.Cells[colDSNS.Index].Value = item.SchoolDsns; //位置
                row.Cells[colAndroidVer.Index].Value = item.SchoolAppVer; //Android版本編號
                row.Cells[colIOSVer.Index].Value = item.SchoolAppIOSVer; //IOS版本編號
                row.Cells[colRemake.Index].Value = item.Remark; //備注
                dataGridViewX1.Rows.Add(row);
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<AppItem> appList = _A.Select<AppItem>();
            _A.DeletedValues(appList); //全部刪除

            List<AppItem> itemlist = new List<AppItem>();
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                    continue;

                AppItem item = new AppItem();

                item.SchoolName = "" + row.Cells[colSchoolName.Index].Value; //學校名稱
                item.AppName = "" + row.Cells[colAppName.Index].Value; //APP名稱
                item.PackageName = "" + row.Cells[colPackageName.Index].Value; //PackageName
                item.BundleID = "" + row.Cells[colBundleID.Index].Value; //BundleID
                item.SchoolDsns = "" + row.Cells[colDSNS.Index].Value; //位置
                item.SchoolAppVer = "" + row.Cells[colAndroidVer.Index].Value; //Android版本編號
                item.SchoolAppIOSVer = "" + row.Cells[colIOSVer.Index].Value; //IOS版本編號
                item.Remark = "" + row.Cells[colRemake.Index].Value; //備注

                itemlist.Add(item);
            }

            //新增
            _A.SaveAll(itemlist);

            MsgBox.Show("儲存成功!");

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
