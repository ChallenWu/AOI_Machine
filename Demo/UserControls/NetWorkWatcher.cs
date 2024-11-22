using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using XCore;

namespace Demo
{
    public partial class NetWorkWatcher : UserControl
    {
        private Timer timer;

        public NetWorkWatcher()
        {
            InitializeComponent();
            InitialDgv();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //Doc trang thai ethernet
            try
            {
                dataGridView1.Rows.Clear();
                foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.Cells.Add(new DataGridViewImageCell());
                        dr.Cells.Add(new DataGridViewTextBoxCell());
                        dr.Cells.Add(new DataGridViewTextBoxCell());
                        dr.Cells[0].Value = (item.OperationalStatus == OperationalStatus.Up) ? Properties.Resources._lampGreen20 : Properties.Resources._lampRed20;
                       
                        foreach (var ipInfo in item.GetIPProperties().UnicastAddresses.ToArray())
                        {
                            if (ipInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                dr.Cells[1].Value = ipInfo.Address.ToString();
                            }
                        }
                        dr.Cells[2].Value = item.Name;
                        dataGridView1.Rows.Add(dr);
                    }
                }
            }
            catch
            {

            }
        }

        private string[] colHead = new string[] { "", "IP", "Name" };
        private void InitialDgv()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns.Add(new DataGridViewImageColumn());
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Columns[i].HeaderText = MultiLanguage.GetMessage(colHead[i]);
                dataGridView1.Columns[i].HeaderText = colHead[i];
            }
            dataGridView1.Columns[0].Width = 40;
        }
    }
}
