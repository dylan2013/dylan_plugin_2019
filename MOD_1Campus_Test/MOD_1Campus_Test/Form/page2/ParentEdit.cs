using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOD_1Campus_Test
{
    public partial class ParentEdit : BaseForm
    {
        string _ref_student_id;
        string _parent_uid;

        Stud _stud { get; set; }

        public ParentEdit(Stud stud)
        {
            InitializeComponent();

            _stud = stud;
        }

        private void ParentChange_Load(object sender, EventArgs e)
        {



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //INSERT INTO "表格1" ("欄位1", "欄位2", ...)
            //SELECT "欄位3", "欄位4", ...
            //FROM "表格2";

            StringBuilder sb_sql = new StringBuilder();



        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }
    }
}
