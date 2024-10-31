using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;

namespace Demo
{
    public partial class UserAccountManager : UserControl
    {

        private int m_SelectRowIndex = -1;
        private string m_SelectAccount;

        public delegate void OnDeleteAccountDelegate();
        public OnDeleteAccountDelegate OnDeleteAccount;

        public UserAccountManager()
        {
            InitializeComponent();
            //btn_AccountDelete.Text = XCore.MultiLanguage.GetMessage("删除账号");
            InitDataGridView();
            UpDataGridView();
        }

        private void InitDataGridView()
        {
            dataGridView_AccountInfo.AllowUserToAddRows = false;
            dataGridView_AccountInfo.AllowUserToOrderColumns = false;
            dataGridView_AccountInfo.AllowUserToDeleteRows = false;
            dataGridView_AccountInfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_AccountInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private string[] colHead = new string[] { "Account", "Name", "Password", "Permission" };

        public void UpDataGridView()
        {
            UserAccountControl.ReadFromXml();
            dataGridView_AccountInfo.Rows.Clear();
            dataGridView_AccountInfo.Columns.Clear();

            dataGridView_AccountInfo.Columns.Add(new DataGridViewImageColumn());
            dataGridView_AccountInfo.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_AccountInfo.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_AccountInfo.Columns.Add(new DataGridViewTextBoxColumn());

            for (int i = 0; i < 4; i++)
            {
                //dataGridView_AccountInfo.Columns[i].HeaderText = XCore.MultiLanguage.GetMessage(colHead[i]);
                dataGridView_AccountInfo.Columns[i].HeaderText = colHead[i];
                dataGridView_AccountInfo.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (KeyValuePair<string, UserAccount> kvp in UserAccountControl.AllUserAccounts)
            {
                DataGridViewRow dr = new DataGridViewRow();
                foreach (KeyValuePair<string, string> kvpAccount in kvp.Value.UserAccount_DIc)
                {
                    DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                    cell1.Value = kvpAccount.Value;
                    dr.Cells.Add(cell1);
                }
                dataGridView_AccountInfo.Rows.Add(dr);
            }
        }


        private void dataGridView_AccountInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_SelectRowIndex = e.RowIndex;
            if (m_SelectRowIndex < 0)
            {
                return;
            }
            m_SelectAccount = dataGridView_AccountInfo.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btn_AccountDelete_Click_1(object sender, EventArgs e)
        {
            if (m_SelectRowIndex < 0)
                return;
            if (BzMessagebox.Show(XCore.MultiLanguage.GetMessage("确定删除选中账号", m_SelectAccount) + "？", XCore.MultiLanguage.GetMessage("提示"),
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                UserAccountControl.RemoveCount(m_SelectAccount);
                UpDataGridView();
                if (OnDeleteAccount != null)
                    OnDeleteAccount();
            }
        }
    }
}
