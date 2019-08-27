using FISCA;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOD_1Campus_Test
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            //測試如何將1Campus的家長資料取得

            MenuButton menuButton =MotherForm.StartMenu["行動應用"];
            menuButton.Image = Properties.Resources.agronomy_64;
            menuButton["家長使用查詢"].Click += delegate
            {
                ParentForm a = new ParentForm();
                a.ShowDialog();
            };

            menuButton["行動使用統計"].Click += delegate
            {
                ParentByStudent a = new ParentByStudent();
                a.ShowDialog();
            };
        }
    }
}
