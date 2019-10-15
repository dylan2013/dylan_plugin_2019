using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Z_EightDomainEdit
{
    class Permissions
    {

        public static string 領域資料管理 { get { return "Z_EightDomainEdit.RoleManager01"; } }
        public static bool 領域資料管理權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[領域資料管理].Executable;
            }
        }
    }
}
