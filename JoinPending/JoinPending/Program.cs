using FISCA;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinPending
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            string 批次加入待處理 = "學生/編號加入待處理";

            FISCA.Features.Register(批次加入待處理, arg =>
            {
                AddPending a = new AddPending();
                a.ShowDialog();
            });

            RibbonBarItem rbItem1 = MotherForm.RibbonBarItems["學生", "其它"];
            rbItem1["編號加入待處理"].Image = Properties.Resources.group_add_64;
            rbItem1["編號加入待處理"].Click += delegate
            {
                Features.Invoke(批次加入待處理);
            };
        }

    }
}
