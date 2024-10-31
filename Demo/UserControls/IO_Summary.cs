using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class IO_Summary : UserControl
    {
        public IO_Summary()
        {
            InitializeComponent();

        }

        private string _sn;
        private bool _unitStatus;
        private string _iO;
        private string _yield;
        private string _pF;
        private string _uPH;
        private string _cT;
        private Color _oKColor = Color.FromArgb(0, 249, 0);
        private Color _nGColor = Color.FromArgb(236, 93, 87);

        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Category("GUI属性"), Description("SN")]
        public string SN { get { return _sn; } set { _sn = value; this.Lbl_Unit.Text = _sn; } }
        [Category("GUI属性"), Description("Input/Output")]
        public string Input_Output { get { return _iO; } set { _iO = value; this.Lbl_IO.Text = _iO; } }
        [Category("GUI属性"), Description("Yield")]
        public string Yield { get { return _yield; } set { _yield = value; this.Lbl_Yield.Text = _yield; } }
        [Category("GUI属性"), Description("Pass/Fail")]
        public string Pass_Fail { get { return _pF; } set { _pF = value; this.Lbl_PF.Text = _pF; } }
        [Category("GUI属性"), Description("UPH")]
        public string UPH { get { return _uPH; } set { _uPH = value; this.Lbl_UPH.Text = _uPH; } }
        [Category("GUI属性"), Description("CT")]
        public string CT { get { return _cT; } set { _cT = value; this.LBL_CT.Text = _cT; } }
        [Category("GUI属性"), Description("OKColor")]
        public Color OKColor { get { return _oKColor; } set { _oKColor = value; } }
        [Category("GUI属性"), Description("NGColor")]
        public Color NGColor { get { return _nGColor; } set { _nGColor = value; } }
        [Category("GUI属性"), Description("UnitStatus 当前物料状态")]
        public bool UnitStatus { get { return _unitStatus; } set { _unitStatus = value; ChangeUnitBackColor(_unitStatus); } }

        private void ChangeUnitBackColor(bool b)
        {
            if (b)
                this.Pa_Unit.BackColor = _oKColor;
            else
                this.Pa_Unit.BackColor = _nGColor;
        }


        private void panel1_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);
        }

    }
}
