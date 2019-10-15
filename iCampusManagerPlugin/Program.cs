using FISCA;
using FISCA.Permission;
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

            //不須權限
            item["快速加入待處理"].Click += delegate
            {

                AddTempForm from = new AddTempForm();
                from.ShowDialog();
            };

            item["批次更新UDM"].Enable = Permissions.批次更新UDM權限;
            item["批次更新UDM"].Click += delegate
            {
                UDMManagerForm from = new UDMManagerForm();
                from.ShowDialog();
            };

            item["APP版本設定器"].Enable = Permissions.APP版本設定器權限;
            item["APP版本設定器"].Click += delegate
            {
                AppVerForm from = new AppVerForm();
                from.ShowDialog();
            };

            item["教師訊息發送統計"].Enable = Permissions.教師訊息發送統計權限;
            item["教師訊息發送統計"].Click += delegate
            {
                MessageTeacherUsage from = new MessageTeacherUsage();
                from.ShowDialog();
            };

            item["校園訊息發送統計"].Enable = Permissions.校園訊息發送統計權限;
            item["校園訊息發送統計"].Click += delegate
            {
                MessageAllUsage from = new MessageAllUsage();
                from.ShowDialog();
            };


            Catalog iCampusManager01 = RoleAclSource.Instance["學校"]["外掛"];
            iCampusManager01.Add(new RibbonFeature(Permissions.APP版本設定器, "APP版本設定器"));
            iCampusManager01.Add(new RibbonFeature(Permissions.批次更新UDM, "批次更新UDM"));
            iCampusManager01.Add(new RibbonFeature(Permissions.教師訊息發送統計, "教師訊息發送統計"));
            iCampusManager01.Add(new RibbonFeature(Permissions.校園訊息發送統計, "校園訊息發送統計"));
        }
    }
}
