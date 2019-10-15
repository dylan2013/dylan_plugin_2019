using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using iCampusManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_EightDomainEdit
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            RibbonBarItem item = FISCA.Presentation.MotherForm.RibbonBarItems["學校", "外掛"];
            item["領域資料管理"].Enable = false;
            item["領域資料管理"].Click += delegate
            {
                DomainForm from = new DomainForm();
                from.ShowDialog();
            };

            SchoolPanel.SetSchoolPanel.SelectedSourceChanged += delegate
            {
                if (SchoolPanel.SetSchoolPanel.SelectedSource.Count > 0 && Permissions.領域資料管理權限)
                {
                    item["領域資料管理"].Enable = true;
                }
                else
                {
                    item["領域資料管理"].Enable = false;
                }
            };

            Catalog iCampusManager01 = RoleAclSource.Instance["學校"]["外掛"];
            iCampusManager01.Add(new RibbonFeature(Permissions.領域資料管理, "領域資料管理"));
        }
    }
}
