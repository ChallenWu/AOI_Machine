using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.UserControls
{
        public partial class DataEachUnit : UserControlBase
    {
            public DataEachUnit()
            {
                InitializeComponent();
            }

            /// <summary>
            /// 控件更新显示后，此事件触发
            /// </summary>
            public event Action<DataTable> Refreshed_event;

            private string _errormessage = string.Empty;
            public string ErrorMessage { get { return _errormessage; } }//错误/异常信息   只读属性

            [Category("GUI属性"), Description("图表显示的信息内容（列数组）")]
            public int[] ShowColumn { get; set; }//图表显示的信息内容（列数组）

            //public DataTable SuorceData { get; set; }//图标数据源

            private void 数据表格_Load(object sender, EventArgs e)
            {
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(210, 212, 213);
                dataGridView1.EnableHeadersVisualStyles = false;
            }

            public bool ClearData()
            {
                this.dataGridView1.DataSource = null;
                return true;
            }

            public bool UpdateData(DataTable dt)
            {
                try
                {
                    _errormessage = string.Empty;
                    if (dt == null)
                    {
                        _errormessage = "Dữ liệu đầu vào trống(null)";
                        return false;
                    }
                    //Array.Reverse(ShowColumn);
                    DataTable mydt = dt;
                    int mindex = mydt.Columns.Count;
                    bool[] showbool = new bool[mindex];//用于判断是否显示列
                    if (ShowColumn != null && ShowColumn.Length > 0) //显示列数组为空，则默认显示全部列
                    {
                        for (int i = 0; i < ShowColumn.Length; i++)
                        {
                            showbool[ShowColumn[i]] = true;
                        }

                        for (int i = mindex - 1; i >= 0; i--)
                        {
                            if (showbool[i] == false)
                                mydt.Columns.RemoveAt(i);
                        }
                        this.dataGridView1.DataSource = mydt;
                        DataTable dtresult = (DataTable)this.dataGridView1.DataSource;
                        //Edit: Xếp thời gian gần nhất lên đầu để truy xuất
                        dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Descending);
                        if (Refreshed_event != null)
                            Refreshed_event(dtresult);

                        return true;
                    }
                    else
                    {

                        this.dataGridView1.DataSource = mydt;
                        DataTable dtresult = (DataTable)this.dataGridView1.DataSource;
                        //Edit: Xếp thời gian gần nhất lên đầu để truy xuất
                        dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Descending);
                        this.dataGridView1.ClearSelection();
                        if (Refreshed_event != null)
                            Refreshed_event(dtresult);
                        //_errormessage = "图表显示的信息内容（列数组）ShowColumn为空";
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    _errormessage = ex.Message;
                    return false;
                }
            }

            public DataTable GetData()
            {
                try
                {
                    return (DataTable)this.dataGridView1.DataSource;
                }
                catch (Exception ex)
                {
                    _errormessage = ex.Message;
                    return null;
                }

            }

        }
    }
