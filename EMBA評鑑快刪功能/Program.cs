using FISCA;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMBA評鑑快刪功能
{
    public class Program
    {
        [MainMethod("TeachingEvaluation")]
        public static void Main()
        {
            string name = "評鑑快刪(For APP Testing)";
            RibbonBarItem templateManager = MotherForm.RibbonBarItems["學生", "其它"];
            templateManager[name].Enable = false;
            templateManager["評鑑快刪(For APP Testing)"].Click += delegate
            {
                DeleteForm del = new DeleteForm();
                del.ShowDialog();
            };

            K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count == 1)
                {
                    templateManager[name].Enable = true;
                }
                else
                {
                    templateManager[name].Enable = false;
                }
            };
        }
    }
}
