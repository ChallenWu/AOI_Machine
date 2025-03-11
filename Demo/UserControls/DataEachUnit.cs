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
    public partial class DataEachUnit : UserControl
    {
        public DataEachUnit()
        {
            InitializeComponent();
        }

    /// <summary>
    ///This event is triggered after the control updates the display
    /// </summary>
        public event Action<DataTable> Refreshed_event;

        private string _errormessage = string.Empty;
        public string ErrorMessage { get { return _errormessage; } }//Error/Exception information Read-only attribute

        [Category("GUI Properties"), Description("The information displayed in the chart (column array)")]
        public int[] ShowColumn { get; set; }//The information content displayed in the chart (column array)

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
                    _errormessage = "Data input is empty(null)";
                    return false;
                }
                //Array.Reverse(ShowColumn);
                DataTable mydt = dt;
                int mindex = mydt.Columns.Count;
                bool[] showbool = new bool[mindex];//Used to determine whether to display the column

            if (ShowColumn != null && ShowColumn.Length > 0) //If the display column array is empty, all columns will be displayed by default.
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
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(() =>
                    {
                        this.dataGridView1.DataSource = mydt;
                    }));
                }
                else
                    this.dataGridView1.DataSource = mydt;
                DataTable dtresult = (DataTable)this.dataGridView1.DataSource;
                    //Xếp thời gian gần nhất lên đầu để truy xuất
                    //dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                    if (Refreshed_event != null)
                        Refreshed_event(dtresult);

                    return true;
                }
                else
                {
                    if(dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(() =>
                    {
                        this.dataGridView1.DataSource = mydt;
                    }));
                }
                    else
                    this.dataGridView1.DataSource = mydt;


                DataTable dtresult = (DataTable)this.dataGridView1.DataSource;
                    //Edit: Xếp thời gian gần nhất lên đầu để truy xuất
                    //dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                    //this.dataGridView1.ClearSelection();
                    if (Refreshed_event != null)
                        Refreshed_event(dtresult);
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
