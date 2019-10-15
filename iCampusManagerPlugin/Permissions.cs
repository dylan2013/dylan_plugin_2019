using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iCampusManagerPlugin
{
    class Permissions
    {

        public static string 批次更新UDM { get { return "iCampusManagerPlugin.RoleManager01"; } }
        public static bool 批次更新UDM權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[批次更新UDM].Executable;
            }
        }

        public static string APP版本設定器 { get { return "iCampusManagerPlugin.RoleManager02"; } }
        public static bool APP版本設定器權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[APP版本設定器].Executable;
            }
        }

        public static string 教師訊息發送統計 { get { return "iCampusManagerPlugin.RoleManager03"; } }
        public static bool 教師訊息發送統計權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[教師訊息發送統計].Executable;
            }
        }

        public static string 校園訊息發送統計 { get { return "iCampusManagerPlugin.RoleManager04"; } }
        public static bool 校園訊息發送統計權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[校園訊息發送統計].Executable;
            }
        }
    }
}
