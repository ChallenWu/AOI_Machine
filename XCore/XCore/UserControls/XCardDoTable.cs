using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XCore
{
    public partial class XCardDoTable : UserControl
    {
        private Timer m_Timer;
        private string cardname = "cardname";
        private int cardid = 0;
        private string[] colHead = new string[] { "", "", "通道位置", "通道号", "实际ID", "输出点名称" };
        private Dictionary<int , DOSTSTYPE> stsMap = new Dictionary<int  , DOSTSTYPE>();
        private Dictionary<int  , DOSTSTYPE> lastStsMap = new Dictionary<int  , DOSTSTYPE>();
        private Dictionary<int, int> SetId = new Dictionary<int, int>();
        public event EventHandler RestEventHandler;
        private int showIdx, readIdx;

        public XCardDoTable()
        {
            InitializeComponent();

            colHead[2] = MultiLanguage.GetMessage("通道位置");
            colHead[3] = MultiLanguage.GetMessage("通道号");
            colHead[4] = MultiLanguage.GetMessage("实际") + "ID";
            colHead[5] = MultiLanguage.GetMessage("输出点名称");
            
            InitialDgv();
            InitialTimer();
        }
        public string CardName
        {
            get { return cardname; }
            set
            {
                cardname = value;
                try
                {
                    if (XDevice.Instance.CardMap[cardid] != null)
                    {
                        if (XDevice.Instance.CardMap[cardid].Name == cardname)
                        {
                            this.groupBox1.Text = MultiLanguage.GetMessage(cardname);
                            this.toolStripStatusLabel1.Text = MultiLanguage.GetMessage(cardname);
                            dataGridView1.Rows.Clear();
                            stsMap.Clear();
                            lastStsMap.Clear();
                            foreach (KeyValuePair<int  , XDo> kvp in XDevice.Instance.DoMap)
                            {
                                if (kvp.Value.CardName == cardname)
                                {
                                    DataGridViewRow dr = new DataGridViewRow();
                                    dr.Cells.Add(new DataGridViewImageCell());
                                    dr.Cells.Add(new DataGridViewButtonCell());
                                    dr.Cells.Add(new DataGridViewTextBoxCell());
                                    dr.Cells.Add(new DataGridViewTextBoxCell());
                                    dr.Cells.Add(new DataGridViewTextBoxCell());
                                    dr.Cells.Add(new DataGridViewTextBoxCell());


                                    dr.Cells[0].Value = Properties.Resources._lampGray20;

                                    if (kvp.Value.CardName == "主设备1")
                                    {
                                        dr.Cells[2].Value = MultiLanguage.GetMessage("扩展通道") + "_EDI";
                                        showIdx = kvp.Value.Channel * 8 + kvp.Value.ActId;
                                    }
                                    else
                                    {
                                        if (kvp.Value.Channel == 0)
                                            dr.Cells[2].Value = MultiLanguage.GetMessage("常规通道") + "_DI";
                                        else
                                            dr.Cells[2].Value = MultiLanguage.GetMessage("扩展通道") + "_EDI";
                                        showIdx = kvp.Value.Channel * 16 + kvp.Value.ActId;
                                    }

                                    dr.Cells[3].Value = kvp.Value.Channel + 1;
                                    dr.Cells[4].Value = kvp.Value.ActId + 1;

                                    if (SetId.ContainsKey(showIdx) == false)
                                    {
                                        SetId.Add(showIdx, kvp.Value.SetId);
                                    }
                                    dr.Cells[5].Value = MultiLanguage.GetMessage(kvp.Value.Name);

                                    dataGridView1.Rows.Add(dr);
                                    stsMap.Add(showIdx, DOSTSTYPE.LOW);
                                    lastStsMap.Add(showIdx, DOSTSTYPE.LOW);
                                }
                            }
                        }
                        else
                        {
                            this.toolStripStatusLabel1.Text = MultiLanguage.GetMessage("卡号和卡名称不对应");
                        }
                    }
                }
                catch
                {
                }
          


            }
        }
        public int CardID
        {
            get { return cardid; }
            set { cardid = value; }
        }
        private void InitialTimer()
        {
            m_Timer = new Timer();
            m_Timer.Interval = 100;
            m_Timer.Tick += new EventHandler(m_Timer_Tick);
            m_Timer.Start();
        }
        private void InitialDgv()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns.Add(new DataGridViewImageColumn());
            dataGridView1.Columns.Add(new DataGridViewButtonColumn());
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());

            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Columns[i].HeaderText = colHead[i];
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.Columns[0].Width = 20;
            dataGridView1.Columns[1].Width = 20;
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].Width = 35;
            dataGridView1.Columns[4].Width = 35;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int clickidx;
            if (e.ColumnIndex == 1)
            {
                if (RestEventHandler != null)
                    RestEventHandler(null, null);
                if (this.groupBox1.Text ==MultiLanguage.GetMessage( "主设备1"))
                {
                    clickidx = (int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) - 1) * 8 + int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) - 1;
                    int doId = SetId[clickidx];
                    XDo currentDo = XDevice.Instance.FindDoById(doId);
                    if (currentDo.STS == DOSTSTYPE.HIGH)
                    {
                        XDevice.Instance.FindDoById(doId).SetDo(DOSTSTYPE.LOW);
                    }
                    else
                    {
                        XDevice.Instance.FindDoById(doId).SetDo(DOSTSTYPE.HIGH);
                    }
                }
                else
                {
                    clickidx = (int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) - 1) * 16 + int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) - 1;
                    int doId = SetId[clickidx];
                    XDo currentDo = XDevice.Instance.FindDoById(doId);
                    if (currentDo.STS == DOSTSTYPE.HIGH)
                    {
                        XDevice.Instance.FindDoById(doId).SetDo(DOSTSTYPE.LOW);
                    }
                    else
                    {
                        XDevice.Instance.FindDoById(doId).SetDo(DOSTSTYPE.HIGH);
                    }
                }
                //int doId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

            }
        }

        private void m_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {

                    if (this.groupBox1.Text ==MultiLanguage.GetMessage( "主设备1"))
                    {
                        readIdx = (int.Parse(dr.Cells[3].Value.ToString()) - 1) * 8 + int.Parse(dr.Cells[4].Value.ToString()) - 1;
                    }
                    else
                    {
                        readIdx = (int.Parse(dr.Cells[3].Value.ToString()) - 1) * 16 + int.Parse(dr.Cells[4].Value.ToString()) - 1;
                    }

                    int doId = SetId[readIdx];
                    stsMap[readIdx] = XDevice.Instance.FindDoById(doId).STS;
                    if (stsMap[readIdx] == DOSTSTYPE.HIGH && lastStsMap[readIdx] == DOSTSTYPE.LOW)
                    {
                        dr.Cells[0].Value = Properties.Resources._lampBlue20;
                    }
                    else if (stsMap[readIdx] == DOSTSTYPE.LOW && lastStsMap[readIdx] == DOSTSTYPE.HIGH)
                    {
                        dr.Cells[0].Value = Properties.Resources._lampGray20;
                    }
                    DOSTSTYPE temp = stsMap[readIdx];
                    lastStsMap[readIdx] = temp;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("bug");
            }

        }
    }
}
