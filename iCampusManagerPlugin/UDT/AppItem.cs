using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iCampusManagerPlugin
{
    [TableName("icampusmanager.app_ver_item")]
    class AppItem : ActiveRecord
    {
        /// <summary>
        /// 學校名稱
        /// </summary>
        [Field(Field = "school_name", Indexed = true)]
        public string SchoolName { get; set; }

        /// <summary>
        /// DSNS
        /// </summary>
        [Field(Field = "app_name", Indexed = true)]
        public string AppName { get; set; }

        /// <summary>
        /// DSNS
        /// </summary>
        [Field(Field = "school_dsns", Indexed = true)]
        public string SchoolDsns { get; set; }

        /// <summary>
        /// APP版本編號 by Android
        /// </summary>
        [Field(Field = "school_app_ver", Indexed = true)]
        public string SchoolAppVer { get; set; }

        /// <summary>
        /// APP版本編號 by IOS
        /// </summary>
        [Field(Field = "school_app_iosver", Indexed = true)]
        public string SchoolAppIOSVer { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Field(Field = "remark", Indexed = true)]
        public string Remark { get; set; }

        /// <summary>
        /// Bundle ID
        /// </summary>
        [Field(Field = "bundle_id", Indexed = true)]
        public string BundleID { get; set; }

        /// <summary>
        /// PackageName
        /// </summary>
        [Field(Field = "package_name", Indexed = true)]
        public string PackageName { get; set; }
    }
}
