using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCore.UserControls
{
    public delegate void OnMoveToTraySupplyPos(int taskId, string posName, double vel);
    public partial class XPositionModelsTable : UserControl
    {
        public ACS.SPiiPlusNET.Api Acs;

        public event Action<string, XPosition> OnAdded;
        public event Action<string, XPosition> OnDeleted;
        public event Action<string, XPosition> OnTeached;
        //public event Action<string, XPosition> OnMoving;
        public event Action<string> OnMoving;
        public event Action OnSaved;
        public event EventHandler RestEventHandler;
        private int m_SelectRowIndex = -1;
        private int m_SelectCellColIndex = -1;
        private int m_MoveMode = 0;
        private string m_CurrentPointName;
        public event OnMoveToTraySupplyPos OnTrayFeedSysMoveToPos;
        private int taskId = 1;
        private int[] m_AxisIdGroup;
        private void InitialDgv()
        {
            dataGridViewModels.AllowUserToAddRows = false;
            dataGridViewModels.AllowUserToOrderColumns = false;
            dataGridViewModels.AllowUserToDeleteRows = false;
            dataGridViewModels.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewModels.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridViewPossitions.AllowUserToAddRows = false;
            dataGridViewPossitions.AllowUserToOrderColumns = false;
            dataGridViewPossitions.AllowUserToDeleteRows = false;
            dataGridViewPossitions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPossitions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        public int TaskId
        {
            get { return this.taskId; }
            set
            {
                this.taskId = value;
                if (XTaskManager.Instance.FindTaskById(taskId) != null)
                {
                    // this.toolStripStatusLabel1.Text = XTaskManager.Instance.FindTaskById(taskId).Name;
                    m_AxisIdGroup = XTaskManager.Instance.FindTaskById(taskId).PositionTableAxisMap.Keys.ToArray();

                    LoadXMLModels();
                }
            }
        }

        public void LoadXMLModels()
        {
            XModelManager.Instance.LoadModelNames();
            UpdateDataModelGridView();
            UpdateDataPositionGridView();
        }

        public void CreateXMLModels()
        {

        }

        public XPositionModelsTable()
        {
            InitializeComponent();
            InitialDgv();
        }

        private void dataGridViewModels_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                m_SelectRowIndex = e.RowIndex;
                if (m_SelectRowIndex < 0)
                {
                    return;
                }
                m_MoveMode = 0;
                m_CurrentPointName = dataGridViewModels.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }

        }
        private void dataGridViewModels_CellClick(object sender, DataGridViewCellEventArgs e)
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
                if (dataGridViewModels.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    m_CurrentPointName = dataGridViewModels.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
                else
                {
                    m_CurrentPointName = "";
                }
                    
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        public void UpdateDataPositionGridView()
        {
            try
            {
                int colNum = m_AxisIdGroup.Length + 2;
                dataGridViewPossitions.Rows.Clear();
                dataGridViewPossitions.Columns.Clear();
                DataGridViewTextBoxColumn dcol0 = new DataGridViewTextBoxColumn();
                dataGridViewPossitions.Columns.Add(dcol0);
                dataGridViewPossitions.Columns[0].HeaderText = "Name";
                dataGridViewPossitions.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (int i = 1; i < colNum - 1; i++)
                {
                    DataGridViewTextBoxColumn dcol = new DataGridViewTextBoxColumn();
                    dcol.HeaderText = MultiLanguage.GetMessage(XDevice.Instance.FindAxisById(m_AxisIdGroup[i - 1]).Name);
                    dcol.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewPossitions.Columns.Add(dcol);
                }
                DataGridViewTextBoxColumn dcolEnd = new DataGridViewTextBoxColumn();
                dcolEnd.HeaderText = MultiLanguage.GetMessage("注释");
                dcolEnd.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewPossitions.Columns.Add(dcolEnd);
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
                        dataGridViewPossitions.Rows.Add(dr);
                    }
                }

                for (int i = 1; i < colNum - 1; i++)
                {
                    dataGridViewPossitions.Columns[i].Width = 50;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, this.GetType().ToString());
            }

        }


        public void UpdateDataModelGridView()
        {
            try
            {
                dataGridViewModels.Rows.Clear();
                dataGridViewModels.Columns.Clear();
                XModelManager.Instance.modelname_realtime.Clear();
                int colNum = XModelManager.Instance.nameMap.Keys.Count;
                DataGridViewTextBoxColumn dcol0 = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn dcol1 = new DataGridViewTextBoxColumn();
                dcol0.HeaderText = ".No";
                dcol0.SortMode = DataGridViewColumnSortMode.NotSortable;

                dcol1.HeaderText = "Model Names";
                dcol1.SortMode = DataGridViewColumnSortMode.NotSortable;

                dcol0.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dcol1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dataGridViewModels.Columns.Add(dcol0);
                dataGridViewModels.Columns.Add(dcol1);

                DataGridViewTextBoxColumn dcolEnd = new DataGridViewTextBoxColumn();
                dcolEnd.HeaderText = MultiLanguage.GetMessage("注释");
                dcolEnd.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewModels.Columns.Add(dcolEnd);
                if (XModelManager.Instance.nameMap.Count>0)
                {
                    int countIdx = 0;
                    cbbPositionModel.Items.Clear();
                    foreach (KeyValuePair<string, string> kvp in XModelManager.Instance.nameMap)
                    {
                        countIdx += 1;
                        DataGridViewRow dr = new DataGridViewRow();
                        DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                        cell0.Value = countIdx;
                        cell1.Value = kvp.Key;
                        cell2.Value = kvp.Value;

                        dr.Cells.Add(cell0);
                        dr.Cells.Add(cell1);
                        dr.Cells.Add(cell2);
                        dataGridViewModels.Rows.Add(dr);
                        cbbPositionModel.Items.Add(kvp.Key);
                        XModelManager.Instance.modelname_realtime.Add(kvp.Key);
                    }
                    cbbPositionModel.SelectedIndex = 0;
                }
                for (int i = 1; i < colNum - 1; i++)
                {
                    dataGridViewModels.Columns[i].Width = 50;
                }
                
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, this.GetType().ToString());
            }

        }

        private void btnAddModel(object sender, EventArgs e)
        {
            try
            {
                XTipDlg xTip = new XTipDlg(MultiLanguage.GetMessage("请输入点名称"));
                int taskId = XModelManager.Instance.taskID;
                if (xTip.ShowDialog() == DialogResult.OK)
                {
                    if (XModelManager.Instance.nameMap.ContainsKey(xTip.output))
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("已存在点") + xTip.output);
                        return;
                    }
                    int colNum = dataGridViewModels.Columns.Count;
                    dataGridViewModels.Rows.Add();
                    string cur_datetime = DateTime.Now.ToString();
                    string cur_modelname = xTip.output.ToString();
                    dataGridViewModels.Rows[dataGridViewModels.Rows.Count - 1].Cells[0].Value = dataGridViewModels.Rows.Count;
                    dataGridViewModels.Rows[dataGridViewModels.Rows.Count - 1].Cells[1].Value = cur_modelname;
                    dataGridViewModels.Rows[dataGridViewModels.Rows.Count - 1].Cells[2].Value = cur_datetime;
                    XModelManager.Instance.nameMap.Add(xTip.output.ToString(), cur_datetime);
                    XModelManager.Instance.modelname_realtime.Add(m_CurrentPointName);
                    cbbPositionModel.Items.Add(cur_modelname);
                    XXml.NewElement(XModelManager.Instance.ModelXml_Path, XModelManager.Instance.ModelXml_Root + "//" + XModelManager.Instance.Node, cur_modelname, cur_datetime);

                    if (OnAdded != null)
                    {
                        OnAdded(m_CurrentPointName, XPositionManager.Instance.PositionSet[taskId].FindPositionById(m_CurrentPointName));
                    }
                }
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void Combo_Vel_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Go_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteModelClick(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("确定删除选中点", m_CurrentPointName, "？"), MultiLanguage.GetMessage("提示"),
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    dataGridViewModels.Rows.RemoveAt(m_SelectRowIndex);
                    XModelManager.Instance.nameMap.Remove(m_CurrentPointName);
                    XModelManager.Instance.modelname_realtime.Remove(m_CurrentPointName);
                    cbbPositionModel.Items.Remove(m_CurrentPointName);
                    XXml.DeleteElement(XModelManager.Instance.ModelXml_Path, XModelManager.Instance.ModelXml_Root + "//" + XModelManager.Instance.Node, m_CurrentPointName);
                    //if (OnDeleted != null)
                    //{
                    //    OnDeleted(m_CurrentPointName, XPositionManager.Instance.PositionSet[taskId].FindPositionById(m_CurrentPointName));
                    //}
                }
                catch (Exception ex)
                {
                    BzMessagebox.Show(ex.Message);
                }

            }
        }

        private void btnRefreshModelClick(object sender, EventArgs e)
        {
            LoadXMLModels();
        }

        private void btnModelSaveClick(object sender, EventArgs e)
        {

        }

        private void btnAddPosModelClick(object sender, EventArgs e)
        {
            try
            {
                XTipDlg xTip = new XTipDlg(MultiLanguage.GetMessage("请输入点名称") + "," + MultiLanguage.GetMessage("注释"));
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
                    int colNum = dataGridViewPossitions.Columns.Count;
                    dataGridViewPossitions.Rows.Add();
                    dataGridViewPossitions.Rows[dataGridViewPossitions.Rows.Count - 1].Cells[0].Value = ptId;
                    for (int i = 1; i < colNum - 1; i++)
                    {
                        dataGridViewPossitions.Rows[dataGridViewPossitions.Rows.Count - 1].Cells[i].Value = 0;
                    }
                    dataGridViewPossitions.Rows[dataGridViewPossitions.Rows.Count - 1].Cells[colNum - 1].Value = ptName;

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
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void btnDeletePosModelClick(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("确定删除选中点", m_CurrentPointName, "？"), MultiLanguage.GetMessage("提示"),
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    dataGridViewPossitions.Rows.RemoveAt(m_SelectRowIndex);
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

        private void btnPosModelRefreshClick(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(this, MultiLanguage.GetMessage("撤销示教？"), MultiLanguage.GetMessage("询问"),
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                XPositionManager.Instance.PositionSet[taskId].LoadPositions();
                XPositionManager.Instance.PositionSet[taskId].LoadPositionNames();
                UpdateDataPositionGridView();
            }
        }

        private void btnPosModelSaveClick(object sender, EventArgs e)
        {
            //add by tang
            int currentRow = this.dataGridViewPossitions.CurrentCell.RowIndex;
            int currentCol = this.dataGridViewPossitions.CurrentCell.ColumnIndex + 1;
            if (currentCol >= this.dataGridViewPossitions.ColumnCount)
            {
                currentCol = 0;
            }
            this.dataGridViewPossitions.CurrentCell = dataGridViewPossitions[currentCol, currentRow];
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

        private void cbbPositionModel_SelectedIndexChanged(object sender, EventArgs e)
        {

            XModelManager.Instance.load_position(cbbPositionModel.Text);

            XModelManager.Instance.load_positionset(cbbPositionModel.Text);


            UpdateDataPositionGridView();
        }

        private void SavePoints()
        {
            Acs = new ACS.SPiiPlusNET.Api();

            try
            {
                int rowNum = dataGridViewPossitions.Rows.Count;
                int colNum = dataGridViewPossitions.Columns.Count;
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
                    ptId[i] = dataGridViewPossitions.Rows[i].Cells[0].Value.ToString();
                    for (int j = 1; j < colNum - 1; j++)
                    {
                        pos[j - 1] = dataGridViewPossitions.Rows[i].Cells[j].Value.ToString();
                    }
                    posStr[i] = XConvert.StrG2Str(pos, ",");
                    ptName[i] = dataGridViewPossitions.Rows[i].Cells[colNum - 1].Value.ToString();
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
                double[,] pos1 = new double[rowNum, colNum - 2];
                for (int i = 0; i < rowNum; i++)
                {

                    ptId[i] = dataGridViewPossitions.Rows[i].Cells[0].Value.ToString();
                    for (int j = 1; j < colNum - 1; j++)
                    {
                        pos1[i, j - 1] = double.Parse(dataGridViewPossitions.Rows[i].Cells[j].Value.ToString());
                    }
                }

                double[] array = new double[colNum - 2];
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < colNum - 2; j++)
                    {
                        array[j] = pos1[i, j];
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

        private void dataGridViewPossitions_CellClick(object sender, DataGridViewCellEventArgs e)
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
                m_CurrentPointName = dataGridViewPossitions.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void dataGridViewPossitions_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                m_SelectRowIndex = e.RowIndex;
                if (m_SelectRowIndex < 0)
                {
                    return;
                }
                m_MoveMode = 0;
                m_CurrentPointName = dataGridViewPossitions.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (m_SelectRowIndex < 0)
            {
                return;
            }
            try
            {
                if (BzMessagebox.Show(this, MultiLanguage.GetMessage("确认示教点", m_CurrentPointName, "？"), MultiLanguage.GetMessage("询问"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int colNum = dataGridViewPossitions.Columns.Count;
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
                        dataGridViewPossitions.Rows[m_SelectRowIndex].Cells[i].Value = p.ToString("f3");
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
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
            }
        }
    }
}
