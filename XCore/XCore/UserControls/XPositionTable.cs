using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices; //for COMException class
using System.Threading;

namespace XCore
{
    public delegate void OnMoveToTraySupplyPos(int taskId, string posName, double vel);
    public partial class XPositionTable : UserControl
    {
        public ACS.SPiiPlusNET.Api Acs;

        public event Action<string, XPosition> OnAdded;
        public event Action<string, XPosition> OnDeleted;
        public event Action<string, XPosition> OnTeached;
        //public event Action<string, XPosition> OnMoving;
        public event Action<string> OnMoving;
        public event Action OnSaved;
        public event EventHandler RestEventHandler;

        public event OnMoveToTraySupplyPos OnTrayFeedSysMoveToPos;

        private int m_SelectRowIndex = -1;
        private int m_SelectCellColIndex = -1;
        private int m_MoveMode = 0;
        private string m_CurrentPointName;
        private int[] m_AxisIdGroup;
        private double m_Vel;
        private int taskId = 1;

        public XPositionTable()
        {
            InitializeComponent();
            InitialDgv();
            InitialComboBox();          
        }

        private void InitialDgv()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void InitialComboBox()
        {
            Combo_Vel.Items.Add(1);
            Combo_Vel.Items.Add(2);
            Combo_Vel.Items.Add(5);
            Combo_Vel.Items.Add(10);
            Combo_Vel.Items.Add(20);
            Combo_Vel.Items.Add(30);
            Combo_Vel.Items.Add(40);
            Combo_Vel.Items.Add(50);
            Combo_Vel.Items.Add(80);
            Combo_Vel.Items.Add(100);
            Combo_Vel.SelectedIndex = 3;

        }

        public int TaskId
        {
            get { return this.taskId; }
            set
            {
                this.taskId = value;
                if (XTaskManager.Instance.FindTaskById(taskId) != null)
                {
                    this.toolStripStatusLabel1.Text = XTaskManager.Instance.FindTaskById(taskId).Name;
                    m_AxisIdGroup = XTaskManager.Instance.FindTaskById(taskId).PositionTableAxisMap.Keys.ToArray();
                    UpdateDataGridView();
                }
            }
        }

        public void UpdateDataGridView()
        {
            try
            {
                int colNum = m_AxisIdGroup.Length + 2;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                DataGridViewTextBoxColumn dcol0 = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(dcol0);
                dataGridView1.Columns[0].HeaderText = "Name";
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (int i = 1; i < colNum - 1; i++)
                {
                    DataGridViewTextBoxColumn dcol = new DataGridViewTextBoxColumn();
                    dcol.HeaderText = MultiLanguage.GetMessage(XDevice.Instance.FindAxisById(m_AxisIdGroup[i - 1]).Name);
                    dcol.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView1.Columns.Add(dcol);
                }
                DataGridViewTextBoxColumn dcolEnd = new DataGridViewTextBoxColumn();
                dcolEnd.HeaderText = MultiLanguage.GetMessage("注释");
                dcolEnd.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(dcolEnd);
                if (XPositionManager.Instance.PositionSet.ContainsKey(taskId))
                {
                    foreach (KeyValuePair<string, XPosition> kvp in XPositionManager.Instance.PositionSet[taskId].PositionMap)
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
                        cell0.Value = kvp.Key;
                        dr.Cells.Add(cell0);
                        foreach (double dd in kvp.Value.Positions)
                        {
                            DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                            cell1.Value = dd;
                            dr.Cells.Add(cell1);
                        }
                        DataGridViewTextBoxCell cellEnd = new DataGridViewTextBoxCell();
                        cellEnd.Value = XPositionManager.Instance.PositionSet[taskId].NameMap[kvp.Key];
                        dr.Cells.Add(cellEnd);
                        dataGridView1.Rows.Add(dr);
                    }
                }

                for (int i = 1; i < colNum - 1; i++)
                {
                    dataGridView1.Columns[i].Width = 70;
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex, this.GetType().ToString());
            }

        }

        
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                m_SelectRowIndex = e.RowIndex;
                if (m_SelectRowIndex < 0)
                {
                    return;
                }
                m_MoveMode = 0;
                m_CurrentPointName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch(Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                m_SelectRowIndex = e.RowIndex;
                if (m_SelectRowIndex < 0)
                {
                    return;
                }
                m_SelectCellColIndex = e.ColumnIndex;
                m_MoveMode = 1;
                m_CurrentPointName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch(Exception ex) 
            {
                BzMessagebox.Show(ex.Message);
            }
           
        }

        private void Combo_Vel_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Vel = double.Parse(Combo_Vel.SelectedItem.ToString());
        }
  

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                XTipDlg xTip = new XTipDlg(MultiLanguage.GetMessage("请输入点名称")+","+MultiLanguage.GetMessage("注释"));
                if (xTip.ShowDialog() == DialogResult.OK)
                {
                    if (XPositionManager.Instance.PositionSet[taskId].ContainsKey(xTip.output))
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("已存在点") + xTip.output);
                        return;
                    }
                    string ptId = "";
                    string ptName = "";
                    try
                    {
                        string[] ss = xTip.output.Split(',');
                        ptId = ss[0];
                        ptName = ss[1];
                    }
                    catch
                    { 
                        
                    }
                    int colNum = dataGridView1.Columns.Count;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = ptId;
                    for (int i = 1; i < colNum - 1; i++)
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i].Value = 0;
                    }
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[colNum - 1].Value = ptName;

                    double[] pos = new double[m_AxisIdGroup.Length];
                    XPosition position = new XPosition(m_AxisIdGroup, pos, m_AxisIdGroup.Length);
                    position.Name = ptName;
                    XPositionManager.Instance.PositionSet[taskId].Add(ptId, position);
                    XXml.NewElement(XPositionManager.Instance.PositionXml_Path,
                        XPositionManager.Instance.PositionXml_Root + "//" + XPositionManager.Instance.PositionSet[taskId].Node,
                        ptId, XConvert.DoubleG2Str(pos, ","));
                    XXml.NewElement(XPositionManager.Instance.PositionNameXml_Path,
                       XPositionManager.Instance.PositionXml_Root + "//" + XPositionManager.Instance.PositionSet[taskId].Node,
                       ptId, ptName);

                    if (OnAdded != null)
                    {
                        OnAdded(m_CurrentPointName, XPositionManager.Instance.PositionSet[taskId].FindPositionById(m_CurrentPointName));
                    }
                }
            }
            catch(Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("确定删除选中点",m_CurrentPointName,"？"), MultiLanguage.GetMessage("提示"), 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    dataGridView1.Rows.RemoveAt(m_SelectRowIndex);
                    XPositionManager.Instance.PositionSet[taskId].Remove(m_CurrentPointName);
                    XXml.DeleteElement(XPositionManager.Instance.PositionXml_Path,
                        XPositionManager.Instance.PositionXml_Root + "//" + XPositionManager.Instance.PositionSet[taskId].Node,
                        m_CurrentPointName);
                    XXml.DeleteElement(XPositionManager.Instance.PositionNameXml_Path,
                       XPositionManager.Instance.PositionXml_Root + "//" + XPositionManager.Instance.PositionSet[taskId].Node,
                       m_CurrentPointName);

                    if (OnDeleted != null)
                    {
                        OnDeleted(m_CurrentPointName, XPositionManager.Instance.PositionSet[taskId].FindPositionById(m_CurrentPointName));
                    }
                }
                catch (Exception ex)
                {
                    BzMessagebox.Show(ex.Message);
                }

            }
        }

        private void Btn_Teach_Click(object sender, EventArgs e)
        {
            if (m_SelectRowIndex < 0)
            {
                return;
            }
            try
            {
                if (BzMessagebox.Show(this, MultiLanguage.GetMessage("确认示教点",m_CurrentPointName,"？"), MultiLanguage.GetMessage("询问"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int colNum = dataGridView1.Columns.Count;
                    double[] pos = new double[colNum - 2];
                    for (int i = 1; i < colNum - 1; i++)
                    {
                        double p = 0;
                        if (XDevice.Instance.AxisMap[m_AxisIdGroup[i - 1]].IsFeedback)
                        {
                            p = XDevice.Instance.AxisMap[m_AxisIdGroup[i - 1]].POS;
                        }
                        else
                        {
                            p = XDevice.Instance.AxisMap[m_AxisIdGroup[i - 1]].CommandPOS;
                        }
                        dataGridView1.Rows[m_SelectRowIndex].Cells[i].Value = p.ToString("f3");
                        pos[i - 1] = double.Parse(p.ToString("f3"));
                    }

                    //XPositionManager.Instance.PositionSet[taskId].Map[m_CurrentPointName] = new XPosition(m_AxisIdGroup, pos, m_AxisIdGroup.Length);
                    //XXml.UpdateInnerText(XPositionManager.Instance.Path_PositionXml,
                    //                     XPositionManager.Instance.Root_PositionXml + "//" + XPositionManager.Instance.PositionSet[taskId].Node + "//" + m_CurrentPointName,
                    //                     XConvert.DoubleG2Str(pos, ","));

                    if (OnTeached != null)
                    {
                        OnTeached(m_CurrentPointName, XPositionManager.Instance.PositionSet[taskId].FindPositionById(m_CurrentPointName));
                    }
                }
            }
            catch(Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void btn_CancelTeach_Click(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(this, MultiLanguage.GetMessage("撤销示教？"), MultiLanguage.GetMessage("询问"),
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                XPositionManager.Instance.PositionSet[taskId].LoadPositions();
                XPositionManager.Instance.PositionSet[taskId].LoadPositionNames();
                UpdateDataGridView();
            }
        }

        private void Btn_Go_Click(object sender, EventArgs e)
        {
            if (RestEventHandler != null)
                RestEventHandler(null, null);
            try
            {
                if (m_SelectRowIndex < 0)
                {
                    return;
                }

                //XPosition position = XPositionManager.Instance.PositionSet[taskId].PositionMap[m_CurrentPointName];

                //for (int i = 0; i < position.AxisId.Length; i++)
                //{
                //    if (XDevice.Instance.FindAxisById(position.AxisId[i]).IsHomeOk == false)
                //    {
                //        BzMessageBox.Show("未初始化轴：" + XDevice.Instance.FindAxisById(position.AxisId[i]).Name);
                //        return;
                //    }
                //}

                //int zIndex = XTaskManager.Instance.FindTaskById(taskId).Z_PositionAxisIdIndex;
                //double zSafe = XTaskManager.Instance.FindTaskById(taskId).Z_Safe;
                //int zAxis = 0;
                //double zTarget = 0;
                //double[] posTarget_zSafe = new double[position.AxisId.Length];
                //if (zIndex > 0)
                //{
                //    zAxis = position.AxisId[zIndex];
                //    zTarget = position.Positions[zIndex];
                //    for (int i = 0; i < position.AxisId.Length; i++)
                //    {
                //        if (i == zIndex)
                //        {
                //            posTarget_zSafe[i] = zSafe;
                //        }
                //        else
                //        {
                //            posTarget_zSafe[i] = position.Positions[i];
                //        }
                //    }
                //}

              

                //if (m_MoveMode == 0)
                //{
                //    if (BzMessageBox.Show(this, "确认再现点 " + m_CurrentPointName + "？", "询问",
                //   MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                //    {
                //        Task.Factory.StartNew(new Action(() =>
                //        {
                //            if (taskId == 1)
                //            {
                                   
                //            }
                //            //switch ((int)taskId)
                //            //{
                //            //    case 1:
                //            //    //case 2:
                //            //    //case 5:
                //            //    //case 6:
                //            //        //LEFT PICK
                //            //        XPosition position1 = XPositionManager.Instance.PositionSet[1].PositionMap["pWaitL"];
                //            //        //double[] posTarget_zSafe1 = new double[position1.AxisId.Length];
                //            //        int zAxis1 = 0;
                //            //        double zTarget1 = 0;
                //            //        double zSafe1= 0;
                //            //        zSafe1 = XTaskManager.Instance.FindTaskById(1).Z_Safe;
                //            //        zAxis1 = position1.AxisId[2];
                //            //        XController.Instance._MoveAbs(zAxis1, zSafe1, m_Vel, false);//Z AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;
                                   

                //            //        //RIGHT PICK
                //            //        position1 = XPositionManager.Instance.PositionSet[2].PositionMap["pWaitR"];
                //            //        zSafe1 = XTaskManager.Instance.FindTaskById(2).Z_Safe;
                //            //        zAxis1 = position1.AxisId[2];
                                    
                //            //        XController.Instance._MoveAbs(zAxis1, zSafe1, m_Vel, false);//Z AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;
                                   

                //            //        //LEFT PICK
                //            //        position1 = XPositionManager.Instance.PositionSet[1].PositionMap["pWaitL"];
                //            //        zSafe1 = XTaskManager.Instance.FindTaskById(1).Z_Safe;
                //            //        zAxis1 = position1.AxisId[2];
                //            //        XController.Instance._MoveAbs(position1.AxisId[1], position1.Positions[1], m_Vel, false);//Y AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;

                //            //        //RIGHT PICK
                //            //        position1 = XPositionManager.Instance.PositionSet[2].PositionMap["pWaitR"];
                //            //        zSafe1 = XTaskManager.Instance.FindTaskById(2).Z_Safe;
                //            //        zAxis1 = position1.AxisId[2];
                //            //        XController.Instance._MoveAbs(position1.AxisId[1], position1.Positions[1], m_Vel, false);//Y AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;


                //            //        //LEFT VOVER
                //            //        position1 = XPositionManager.Instance.PositionSet[5].PositionMap["pWaitMaskL"];
                //            //        zSafe1 = XTaskManager.Instance.FindTaskById(5).Z_Safe;
                //            //        zAxis1 = position1.AxisId[1];
                                    
                //            //        XController.Instance._MoveAbs(zAxis1, zSafe1, m_Vel, false);//Z AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;
                //            //        XController.Instance._MoveAbs(position1.AxisId[0], position1.Positions[0], m_Vel, false);//Y AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;

                //            //        //RIGHT VOVER
                //            //        position1 = XPositionManager.Instance.PositionSet[6].PositionMap["pWaitMaskR"];
                //            //        zSafe1 = XTaskManager.Instance.FindTaskById(6).Z_Safe;
                //            //        zAxis1 = position1.AxisId[1];
                                    
                //            //        XController.Instance._MoveAbs(zAxis1, zSafe1, m_Vel, false);//Z AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;
                //            //        XController.Instance._MoveAbs(position1.AxisId[0], position1.Positions[0], m_Vel, false);//Y AXIS
                //            //        if (!XController.Instance._WaitMoveDone())
                //            //            return;
                //            //        break;

                //            //}
                            
                //            //if (zIndex > 0)
                //            //{
                //            //    XController.Instance._MoveAbs(zAxis, zSafe, m_Vel, false);
                //            //    if (!XController.Instance._WaitMoveDone())
                //            //        return;
                //            //    XController.Instance._MoveAbs(position.AxisId, posTarget_zSafe, m_Vel, false);
                //            //    if (!XController.Instance._WaitMoveDone())
                //            //        return;
                //            //    XController.Instance._MoveAbs(zAxis, zTarget, m_Vel, false);
                //            //    if (!XController.Instance._WaitMoveDone())
                //            //        return;
                //            //}
                //            //else
                //            //{
                //            //    XController.Instance._MovePosition(position, m_Vel);
                //            //    if (!XController.Instance._WaitMoveDone())
                //            //        return;
                //            //}
                //        }));
                     
                //    }
                //}
                //else if (m_MoveMode == 1)
                //{
                //    if (BzMessageBox.Show(this, "确认再现点 " + m_CurrentPointName + "[轴:" + dataGridView1.Columns[m_SelectCellColIndex].HeaderText + "]？", "询问",
                //   MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                //    {
                //        Task.Factory.StartNew(new Action(() => {
                //            if (zIndex > 0)
                //            {
                //                XController.Instance._MoveAbs(zAxis, zSafe, m_Vel, false);
                //                if (!XController.Instance._WaitMoveDone())
                //                    return;
                //            }
                //            XController.Instance._MoveAbs(position.AxisId[m_SelectCellColIndex - 1],
                //            position.Positions[m_SelectCellColIndex - 1], m_Vel);
                //            if (!XController.Instance._WaitMoveDone())
                //                return;
                //        }));
                //    }
                //}

                if (OnMoving != null)
                {
                    double vel = 30;
                    //m_Vel = double.Parse(Combo_Vel.SelectedItem.ToString());
                    //vel = m_Vel;

                    
                    try
                    {
                        vel = Convert.ToDouble(Combo_Vel.SelectedItem.ToString());
                    }
                    catch
                    {

                    }
                    OnMoving(m_CurrentPointName);
                }

                if (OnTrayFeedSysMoveToPos != null)
                {
                    double vel = 30;

                    try
                    {
                        vel = Convert.ToDouble(Combo_Vel.SelectedItem.ToString());
                    }
                    catch
                    {

                    }
                    OnTrayFeedSysMoveToPos(taskId, m_CurrentPointName, vel);
                }

            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //add by tang
            int currentRow = this.dataGridView1.CurrentCell.RowIndex;
            int currentCol = this.dataGridView1.CurrentCell.ColumnIndex + 1;
            if (currentCol >= this.dataGridView1.ColumnCount)
            {
                currentCol = 0;
            }
            this.dataGridView1.CurrentCell = dataGridView1[currentCol, currentRow];
            //add by tang
            if (BzMessagebox.Show(this, MultiLanguage.GetMessage("确认保存？"), MultiLanguage.GetMessage("询问"),
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SavePoints();
                XPositionManager.Instance.PositionSet[taskId].LoadPositions();
                XPositionManager.Instance.PositionSet[taskId].LoadPositionNames();
                if (OnSaved != null)
                {
                    OnSaved();
                }
            }
        }

        private void SavePoints()
        {
            Acs = new ACS.SPiiPlusNET.Api();

            try
            {
                int rowNum = dataGridView1.Rows.Count;
                int colNum = dataGridView1.Columns.Count;
                SaveBackUpPoints();
                XXml.DeleteInnerText(XPositionManager.Instance.PositionXml_Path, XPositionManager.Instance.PositionXml_Root, XPositionManager.Instance.PositionSet[taskId].Node);
                XXml.NewElement(XPositionManager.Instance.PositionXml_Path,
                    XPositionManager.Instance.PositionXml_Root + "//" + XPositionManager.Instance.PositionSet[taskId].Node,
                    XPositionManager.AXISID, XConvert.IntG2Str(m_AxisIdGroup, ","));
                XXml.DeleteInnerText(XPositionManager.Instance.PositionNameXml_Path, XPositionManager.Instance.PositionXml_Root, XPositionManager.Instance.PositionSet[taskId].Node);
                string[] ptId = new string[rowNum];
                string[] ptName = new string[rowNum];
                string[] posStr = new string[rowNum];
                for (int i = 0; i < rowNum; i++)
                {
                    string[] pos = new string[colNum - 2];
                    ptId[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    for (int j = 1; j < colNum - 1; j++)
                    {
                        pos[j - 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    posStr[i] = XConvert.StrG2Str(pos, ",");
                    ptName[i] = dataGridView1.Rows[i].Cells[colNum - 1].Value.ToString();
                }
                XXml.NewElement(XPositionManager.Instance.PositionXml_Path,
                    XPositionManager.Instance.PositionXml_Root + "//" + XPositionManager.Instance.PositionSet[taskId].Node,
                    ptId, posStr);
                XXml.NewElement(XPositionManager.Instance.PositionNameXml_Path,
                    XPositionManager.Instance.PositionXml_Root + "//" + XPositionManager.Instance.PositionSet[taskId].Node,
                    ptId, ptName);
                //double[] array=new double [posStr.Count()];
                //int h = 0;
                //foreach ( string dt in posStr)
                //{
                //    array[h ] = double.Parse(dt);
                //    h++;
                //}
                double[,] pos1 = new double[rowNum,colNum - 2];
                for (int i = 0; i < rowNum; i++)
                {
                    
                    ptId[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    for (int j = 1; j < colNum - 1; j++)
                    {
                        pos1[i,j - 1] = double.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString());
                    }
                }

                double[] array = new double[colNum - 2];
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < colNum - 2; j++)
                    {
                        array[j] = pos1[i,j];
                    }
                    try  // @sjx add
                    {
                      //  XDevice.Instance.FindCardById(0).WritePos(array, ptId[i]);
                    }
                    catch
                    {
 
                    }
                    Thread.Sleep(100);
                }
                
                

            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void SaveBackUpPoints()
        {
            string DirPath = DateTime.Now.ToString("yyyy-MM-dd");
            DirPath = XPositionManager.Instance.BackUpPosition_Dir + DirPath + "\\";
            if (Directory.Exists(DirPath) == false)
            {
                Directory.CreateDirectory(DirPath);
            }
            string BackUpFile = "";
            BackUpFile = DirPath + XPositionManager.Instance.PositionXml;
            BackUpFile = BackUpFile.Replace(".xml", DateTime.Now.ToString(" HH-mm-ss") + ".xml");
            File.Copy(XPositionManager.Instance.PositionXml_Path, BackUpFile, true);

            BackUpFile = DirPath + XPositionManager.Instance.PositionNameXml;
            BackUpFile = BackUpFile.Replace(".xml", DateTime.Now.ToString(" HH-mm-ss") + ".xml");
            File.Copy(XPositionManager.Instance.PositionNameXml_Path, BackUpFile, true);
        }
    }
}
