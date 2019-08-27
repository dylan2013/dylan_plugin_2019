using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;

namespace ClubPlugIn
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            RibbonBarItem kkj = FISCA.Presentation.MotherForm.RibbonBarItems["學生", "其它"];
            kkj["學生志願指定"].Click += delegate
            {
                志願輸入內容 v = new 志願輸入內容();
                v.ShowDialog();
            };

            RibbonBarItem kkt = FISCA.Presentation.MotherForm.RibbonBarItems["志願序社團", "其它"];
            kkt["志願清除與亂數"].Click += delegate
            {
                批次志願功能 v = new 批次志願功能();
                v.ShowDialog();
            };
        }
    }
}
