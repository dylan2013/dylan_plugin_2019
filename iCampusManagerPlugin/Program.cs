using FISCA;
using FISCA.Presentation;
using iCampusManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iCampusManagerPlugin
{
    public class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [MainMethod]
        public static void Main()
        {
            RibbonBarItem item = FISCA.Presentation.MotherForm.RibbonBarItems["學校", "外掛"];
            item["快速加入待處理"].Click += delegate
            {

                AddTempForm from = new AddTempForm();
                from.ShowDialog();
            };

            item["批次更新UDM"].Click += delegate
            {
                UDMManagerForm from = new UDMManagerForm();
                from.ShowDialog();
            };

            item["APP版本設定器"].Click += delegate
            {
                AppVerForm from = new AppVerForm();
                from.ShowDialog();
            };

            item["教師訊息發送統計"].Click += delegate
            {
                MessageTeacherUsage from = new MessageTeacherUsage();
                from.ShowDialog();
            };


            item["校園訊息發送統計"].Click += delegate
            {
                MessageAllUsage from = new MessageAllUsage();
                from.ShowDialog();
            };

        }
    }
}
