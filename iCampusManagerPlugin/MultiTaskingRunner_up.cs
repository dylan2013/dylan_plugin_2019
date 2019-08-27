using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Threading.Tasks;
using System.Threading;
using FISCA;
using iCampusManager;

namespace iCampusManagerPlugin
{
    public partial class MultiTaskingRunner_up : Form
    {
        private Dictionary<string, Task> Tasks = new Dictionary<string, Task>();
        private Dictionary<Task, DataGridViewRow> TaskRows = new Dictionary<Task, DataGridViewRow>();
        private Dictionary<Task, CancellationTokenSource> TaskCancellations = new Dictionary<Task, CancellationTokenSource>();
        private int CompleteCount { get; set; }

        List<TaskState> tsList = new List<TaskState>();
        int countError = 0;
        public event EventHandler AllTaskCompleted;

        public MultiTaskingRunner_up()
        {
            InitializeComponent();
        }

        public void AddTask(string name, Action<object> action, object aState, CancellationTokenSource source)
        {
            CancellationToken token = source.Token;
            TaskState state = new TaskState(aState, token);

            tsList.Add(state);

            Task task = new Task(x =>
            {
                RunTask(action, x);
            }, state, token);

            state.task = task;

            state.task.ContinueWith(x => CompleteTask(x));

            Tasks.Add(name, state.task);
            TaskCancellations.Add(state.task, source);
        }

        //執行此內容
        public void RunTask(Action<object> action, object x)
        {
            TaskState t = (TaskState)x;
            action(t.State);
            if (t.Token.IsCancellationRequested)
                t.Token.ThrowIfCancellationRequested();
        }

        //完成時執行此內容
        public void CompleteTask(Task task)
        {
            if (TaskRows.ContainsKey(task))
            {
                DataGridViewRow row = TaskRows[task];

                if (InvokeRequired)
                    Invoke(new Action(() => FinalMessage(task, row)));
                else
                    FinalMessage(task, row);
            }
            else
                throw new Exception("爆了!");
        }

        private void FinalMessage(Task task, DataGridViewRow row)
        {
            if (task.IsCanceled)
                row.Cells[0].Value = "Cancelled";
            else
            {
                if (task.IsFaulted)
                {
                    row.Cells[0].Value = "Error";
                    row.Cells[2].Value = task.Exception.InnerException.Message;

                    countError++;
                    btnAddTemp.Text = "將(" + countError + ")加入待處理";
                }
                else
                    row.Cells[0].Value = "Complete";
            }

            DataGridViewButtonCell cell = row.Cells[3] as DataGridViewButtonCell;
            cell.Value = "完成";

            CompleteCount++;

            if (CompleteCount >= Tasks.Count)
            {

                //進入表示全部完成
                if (AllTaskCompleted != null)
                    AllTaskCompleted(this, EventArgs.Empty);

                bool CheckError = false;

                foreach (Task each in Tasks.Values)
                {
                    if (each.Exception != null)
                    {
                        //cbIsErrors
                        if (cbIsError.Checked)
                        {
                            CheckError = true;
                            break;
                        }
                        else
                        {
                            return; //如有錯誤,則畫面暫止
                        }
                    }
                }

                if (CheckError)
                {
                    //如果 cbIsError 被勾選
                    //則錯誤的Task重新排入執行階段
                    CompleteCount = 0;
                    List<DataGridViewRow> RowList = new List<DataGridViewRow>();

                    foreach (DataGridViewRow ErrorRow in dgvTasks.Rows)
                    {
                        if ("" + ErrorRow.Cells[0].Value == "Error")
                        {


                            DataGridViewRow copyRow = new DataGridViewRow();
                            copyRow.CreateCells(dgvTasks);

                            copyRow.Cells[0].Value = "" + ErrorRow.Cells[0].Value;
                            copyRow.Cells[1].Value = "" + ErrorRow.Cells[1].Value;
                            copyRow.Cells[2].Value = "" + ErrorRow.Cells[2].Value;
                            copyRow.Cells[3].Value = "" + ErrorRow.Cells[3].Value;
                            copyRow.Tag = ErrorRow.Tag;

                            RowList.Add(copyRow);
                        }
                    }
                    dgvTasks.Rows.Clear();
                    dgvTasks.Rows.AddRange(RowList.ToArray());

                    foreach (DataGridViewRow reRunRow in dgvTasks.Rows)
                    {
                        Task t = reRunRow.Tag as Task;
                        row.Cells[0].Value = "Running";
                        t.Start();
                    }
                }
                else
                {
                    this.Close(); //沒有錯誤則關閉畫面
                }
            }
        }

        public void ExecuteTasks()
        {
            CompleteCount = 0;
            ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MultiTaskingRunner_Load(object sender, EventArgs e)
        {
            PrepareTasks();
        }

        private void PrepareTasks()
        {
            dgvTasks.Rows.Clear();

            foreach (KeyValuePair<string, Task> each in Tasks)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvTasks, "Preapre", each.Key, "", "取消");
                row.Tag = each.Value;
                dgvTasks.Rows.Add(row);
                TaskRows.Add(each.Value, row);

                if (TaskCancellations.ContainsKey(each.Value))
                {
                    TaskCancellations[each.Value].Token.Register(state =>
                    {
                        DataGridViewRow r = state as DataGridViewRow;
                        if (r.Cells[0].Value.ToString() != "Running") return;

                        if (InvokeRequired)
                            Invoke(new Action(() => r.Cells[0].Value = "Cancelling"));
                        else
                            r.Cells[0].Value = "Cancelling";
                    }, row);
                }
            }

            foreach (DataGridViewRow row in dgvTasks.Rows)
            {
                Task t = row.Tag as Task;
                row.Cells[0].Value = "Running";
                t.Start();
            }
        }

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3) return;

            DataGridViewRow row = dgvTasks.Rows[e.RowIndex];
            Task task = row.Tag as Task;

            if (TaskCancellations.ContainsKey(task))
                TaskCancellations[task].Cancel();
            else
                throw new Exception("爆了!");
        }

        private class TaskState
        {
            public TaskState(object aState, CancellationToken token)
            {
                if (aState is object[])
                {
                    object[] obj = aState as object[];
                    ch = obj[0] as ConnectionHelper;
                }
                else if (aState is ConnectionHelper)
                {
                    ch = aState as ConnectionHelper;
                }

                State = aState;
                Token = token;
            }

            public ConnectionHelper ch { get; set; }

            public object State { get; set; }

            public CancellationToken Token { get; set; }

            public Task task { get; set; }

        }

        private void dgvTasks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 | e.ColumnIndex != 2) return;

            DataGridViewRow row = dgvTasks.Rows[e.RowIndex];

            if (row.Cells[0].Value.ToString() == "Error")
            {
                Task t = row.Tag as Task;
                Exception ex = t.Exception.InnerException;

                ErrorBox.Show(ex.Message, ex);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddTemp_Click(object sender, EventArgs e)
        {
            //把錯誤的部份加入待處理

            //SchoolPanel.SetSchoolPanel.AddToTemp
            List<string> idList = new List<string>();
            foreach (TaskState ts in tsList)
            {
                if (ts.task.Exception != null)
                    //如有錯誤,則把這個學校加入待處理
                    idList.Add(ts.ch.UID);
            }

            if (idList.Count != 0)
            {
                SchoolPanel.SetSchoolPanel.AddToTemp(idList);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            SchoolPanel.SetSchoolPanel.RemoveFromTemp(SchoolPanel.SetSchoolPanel.TempSource);
        }
    }
}
