using FISCA;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMessageTestModule
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            RibbonBarItem InClass = FISCA.Presentation.MotherForm.RibbonBarItems["學生", "其它"];
            InClass["訊息ADD範例"].Click += delegate
            {
                Form1 f = new Form1();
                f.ShowDialog();

            };


        }
    }
}
