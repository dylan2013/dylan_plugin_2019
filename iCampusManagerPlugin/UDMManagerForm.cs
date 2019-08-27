using FISCA;
using FISCA.DSA;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using iCampusManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace iCampusManagerPlugin
{
    public partial class UDMManagerForm : BaseForm
    {
        private List<ConnectionHelper> Connections;

        Dictionary<string, BitTask> RunnerDic;

        private Dictionary<string, UDMsOfSchool> ModuleConfigs = new Dictionary<string, UDMsOfSchool>();

        public UDMManagerForm()
        {
            InitializeComponent();
        }

        private void DesktopModuleManagerForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitConnections();
                LoadAllModuleConfig();
                GroupByToDataGrid();
            }
            catch { Close(); } //爆了就關吧。
        }

        private void GroupByToDataGrid()
        {
            List<ModuleRow> modules = GroupByModules();
            modules.Sort((x, y) =>
            {
                return x.Name.CompareTo(y.Name);
            });
            SortableBindingList<ModuleRow> bl = new SortableBindingList<ModuleRow>(modules);

            dgvModules.DataSource = bl;
        }

        private List<ModuleRow> GroupByModules()
        {
            Dictionary<string, ModuleRow> result = new Dictionary<string, ModuleRow>();

            foreach (UDMsOfSchool each in ModuleConfigs.Values)
            {
                foreach (UDMInfo data in each.Datas)
                {
                    if (!result.ContainsKey(data.Name))
                        result.Add(data.Name, new ModuleRow(data));

                    result[data.Name].IncreaseRefCount();
                }
            }

            return result.Values.ToList();
        }

        private void LoadAllModuleConfig()
        {
            MultiTaskingRunner_up runner = new MultiTaskingRunner_up();

            ModuleConfigs = new Dictionary<string, UDMsOfSchool>();
            foreach (ConnectionHelper conn in Connections)
            {
                string name = SchoolPanel.GlobalSchoolCache[conn.UID].Title;
                runner.AddTask(string.Format("{0}({1})", name, conn.UID), (x) =>
                {
                    LoadModuleConfig(x as ConnectionHelper);
                }, conn, new System.Threading.CancellationTokenSource());
            }
            runner.ExecuteTasks();
        }

        private void LoadModuleConfig(ConnectionHelper conn)
        {
            Envelope rsp = conn.CallService("UDMService.ListModules", new Envelope());
            XElement conf = XElement.Parse(rsp.BodyContent.XmlString);

            lock (ModuleConfigs)
            {
                ModuleConfigs[conn.UID] = new UDMsOfSchool(conf, conn.UID);
            }
        }

        private void InitConnections()
        {
            List<string> selected = SchoolPanel.SetSchoolPanel.SelectedSource;
            List<ConnectionHelper> conns = new List<ConnectionHelper>();

            foreach (string uid in selected)
            {
                conns.Add(ConnectionHelper.GetConnection(uid));
            }

            Connections = conns;
        }

        class ModuleRow
        {
            public ModuleRow(UDMInfo udm)
            {
                Name = udm.Name;
                Url = udm.Url;
                UDTContains = udm.UDTContains;
            }

            public string Name { get; private set; }

            public string Url { get; private set; }

            public bool UDTContains { get; private set; }

            public int ReferenceCount { get; private set; }

            public void IncreaseRefCount()
            {
                ReferenceCount++;
            }

            public void DecreaseRefCount()
            {
                ReferenceCount--;
            }
        }

        class UDMsOfSchool
        {
            private XElement Modules;

            public UDMsOfSchool(XElement modData, string uid)
            {
                Modules = modData;
                UID = uid;
            }

            public string UID { get; private set; }

            public HashSet<string> Urls
            {
                get
                {
                    HashSet<string> hs = new HashSet<string>();

                    foreach (XElement each in Modules.Elements("Module"))
                    {
                        string murl = each.Element("URL").Value;
                        hs.Add(murl);
                    }

                    return hs;
                }
            }

            public HashSet<string> Names
            {
                get
                {
                    HashSet<string> hs = new HashSet<string>();

                    foreach (XElement each in Modules.Elements("Module"))
                    {
                        string murl = each.Element("Name").Value;
                        hs.Add(murl);
                    }

                    return hs;
                }
            }

            /// <summary>
            /// 資料依序是：Name、Url
            /// </summary>
            public HashSet<UDMInfo> Datas
            {
                get
                {
                    HashSet<UDMInfo> hs = new HashSet<UDMInfo>();

                    foreach (XElement each in Modules.Elements("Module"))
                    {
                        hs.Add(new UDMInfo(each));
                    }

                    return hs;
                }
            }
        }

        class UDMInfo
        {
            public UDMInfo(XElement data)
            {
                Name = data.Element("Name").Value;
                Url = data.Element("URL").Value;

                UDTContains = data.Element("Property").Elements("UDT").Count() > 0;
            }

            public string Name { get; private set; }

            public string Url { get; private set; }

            public bool UDTContains { get; private set; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MultiTaskingRunner_up runner = new MultiTaskingRunner_up();

            UDMSelect selectUDM = new UDMSelect();
            if (selectUDM.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(selectUDM.Add_Url))
                {
                    string modUrl = selectUDM.Add_Url;
                    foreach (ConnectionHelper conn in Connections)
                    {
                        string name = SchoolPanel.GlobalSchoolCache[conn.UID].Title;
                        runner.AddTask(string.Format("{0}({1})", name, conn.UID), (x) =>
                        {
                            object[] obj = x as object[];
                            ConnectionHelper c = obj[0] as ConnectionHelper;
                            string xurl = (string)obj[1];
                            AddUDM(c, xurl);
                        }, new object[] { conn, modUrl }, new System.Threading.CancellationTokenSource());
                    }
                    runner.ExecuteTasks();

                    try
                    {
                        InitConnections();
                        LoadAllModuleConfig();
                        GroupByToDataGrid();
                    }
                    catch { Close(); } //爆了就關吧。
                }
                else
                {
                    MsgBox.Show("未選擇UDM!!");
                }
            }
            else
            {
                MsgBox.Show("已取消!!");
            }
        }

        private void AddUDM(ConnectionHelper conn, string url)
        {
            XElement req = new XElement("Request",
                new XElement("URL", url));

            conn.CallService("UDMService.ForceRegistry", new Envelope(new FISCA.XStringHolder(req)));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvModules.SelectedRows.Count <= 0)
                return;

            DialogResult dr = MessageBox.Show("確定要刪除選擇的模組？", "ischool", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.No)
                return;

            MultiTaskingRunner_up runner = new MultiTaskingRunner_up();
            ModuleRow mr = dgvModules.SelectedRows[0].DataBoundItem as ModuleRow;
            string udmName = mr.Name;

            foreach (ConnectionHelper conn in Connections)
            {
                string name = SchoolPanel.GlobalSchoolCache[conn.UID].Title;
                runner.AddTask(string.Format("{0}({1})", name, conn.UID), (x) =>
                {
                    object[] obj = x as object[];
                    ConnectionHelper c = obj[0] as ConnectionHelper;
                    string xurl = (string)obj[1];
                    RemoveUDM(c, xurl);
                }, new object[] { conn, udmName }, new System.Threading.CancellationTokenSource());
            }
            runner.ExecuteTasks();

            //try
            //{
            //    InitConnections();
            //    LoadAllModuleConfig();
            //    GroupByToDataGrid();
            //}
            //catch { Close(); } //爆了就關吧。
        }

        private void RemoveUDM(ConnectionHelper conn, string name)
        {
            XElement req = new XElement("Request",
                new XElement("ModuleName", name));

            conn.CallService("UDMService.RemoveModule", new Envelope(new XHelper(req)));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvModules.SelectedRows.Count <= 0)
                return;

            DialogResult dr = MessageBox.Show("確定要更新選擇的模組？", "ischool", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.No)
                return;

            MultiTaskingRunner_up runner = new MultiTaskingRunner_up();
            ModuleRow mr = dgvModules.SelectedRows[0].DataBoundItem as ModuleRow;
            string udmName = mr.Name;

            RunnerDic = new Dictionary<string, BitTask>();
            foreach (ConnectionHelper conn in Connections)
            {
                TestAddTask(runner, conn, udmName);
            }
            runner.ExecuteTasks();


            for (int x = 0; x < 1; x--)
            {
                if (RunnerDic.Count > 0)
                {
                    bool Succeed = ReNewTask();
                    if (Succeed)
                    {
                        break;
                    }
                }
            }
            //try
            //{
            //    InitConnections();
            //    LoadAllModuleConfig();
            //    GroupByToDataGrid();
            //}
            //catch { Close(); } //爆了就關吧。
        }

        //檢查更新後內容
        //是否都是成功
        private bool ReNewTask()
        {
            bool Succeed = false;
            Dictionary<string, BitTask> dic = new Dictionary<string, BitTask>();
            foreach (string each in RunnerDic.Keys)
            {
                BitTask bt = RunnerDic[each];
                if (!bt.envelope.BodyContent.XmlString.Contains("Succeed"))
                {
                    dic.Add(each, bt);

                }
                else
                {
                    Succeed = true;
                }
            }
            RunnerDic = dic;
            return Succeed;

        }

        //執行更新作業
        private void TestAddTask(MultiTaskingRunner_up runner, ConnectionHelper conn, string udmName)
        {
            string name = SchoolPanel.GlobalSchoolCache[conn.UID].Title;

            BitTask bt = new BitTask();
            bt.conn = conn;
            bt.udmName = udmName;

            runner.AddTask(string.Format("{0}({1})", name, conn.UID), (x) =>
            {
                object[] obj = x as object[];
                ConnectionHelper c = obj[0] as ConnectionHelper;
                string xurl = (string)obj[1];
                Envelope el = UpdateUDM(c, xurl);
                bt.envelope = el;

                if (!RunnerDic.ContainsKey(c.TargetDSNS))
                {
                    RunnerDic.Add(c.TargetDSNS, bt);
                }
            }, new object[] { conn, udmName }, new System.Threading.CancellationTokenSource());

        }

        private Envelope UpdateUDM(ConnectionHelper conn, string name)
        {
            XElement req = new XElement("Request",
                new XElement("ModuleName", name));

            Envelope el = conn.CallService("UDMService.UpdateModule", new Envelope(new XHelper(req)));
            return el;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    class BitTask
    {
        public ConnectionHelper conn;
        public string udmName;
        public Envelope envelope;
    }
}
