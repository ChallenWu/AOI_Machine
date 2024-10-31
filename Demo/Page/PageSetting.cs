using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;
using HB_IWatch;
using Demo.UserControls;

namespace Demo.Page
{
    public partial class PageSetting : UserControlBase
    {
        private static PageSetting instance;
        public static PageSetting Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageSetting();
                return instance;
            }
        }

        public PageSetting()
        {
            InitializeComponent();
            UpDateCompensationSettings(Globals.IsDOEPCB);
        }

        private void PageSetting_Load(object sender, EventArgs e)
        {
            this.xSettingGrid_SettingOption.SaveOkEventHandler += xSettingGrid_SettingOption_SaveOkEventHandler;
            //this.xSettingGrid_SettingOption.SaveOkEventHandler += Globals.OnCarrierChanged;
            this.xSettingGrid_SettingOption.SaveOkEventHandler += Form1.OnparameterChanged;

            //this.xSettingGrid_SettingCalibration.SaveOkEventHandler += UpDateGlobalsSettings;
            //this.xSettingGrid_SettingCalibration.SaveOkEventHandler += Globals.OnSaveMotionPara; //(kvp.Value as ETask).OnSaveMotionPara;
            //this.xSettingGrid_SettingCalibration.SaveOkEventHandler += Form1.OnparameterChanged;  //(kvp.Value as ETask).OnSaveMotionPara;

            this.xSettingGrid_ICT.SaveOkEventHandler += UpDateGlobalsSettings;
            this.xSettingGrid_ICT.SaveOkEventHandler += Form1.OnparameterChanged;

            //this.xSettingGrid_SettingCalibration.SaveOkEventHandler += (XTaskManager.Instance.FindTaskById((int)TaskId.Task30_LeftAssemblyMaterial) as
            //    Task30_LeftAssemblyMaterial).OnSaveForcePara;
            //this.xSettingGrid_SettingCalibration.SaveOkEventHandler += (XTaskManager.Instance.FindTaskById((int)TaskId.Task31_RightAssemblyMaterial) as
            //    Task31_RightAssemblyMaterial).OnSaveForcePara;

            //this.xSettingGrid_SettingNozzlesCompensation.SaveOkEventHandler += UpDateGlobalsSettings;
            //this.xSettingGrid_SettingNozzlesCompensation.SaveOkEventHandler += Form1.OnparameterChanged;

            //this.xSettingGrid_SettingCellsLRCompensation.SaveOkEventHandler += UpDateGlobalsSettings;
            //this.xSettingGrid_SettingCellsLXCompensation.SaveOkEventHandler += UpDateGlobalsSettings;
            //this.xSettingGrid_SettingCellsLYCompensation.SaveOkEventHandler += UpDateGlobalsSettings;

            //this.xSettingGrid_SettingCellsLRCompensation.SaveOkEventHandler += Form1.OnparameterChanged;
            //this.xSettingGrid_SettingCellsLXCompensation.SaveOkEventHandler += Form1.OnparameterChanged;
            //this.xSettingGrid_SettingCellsLYCompensation.SaveOkEventHandler += Form1.OnparameterChanged;

            //this.xSettingGrid_SettingCellsRRCompensation.SaveOkEventHandler += UpDateGlobalsSettings;
            //this.xSettingGrid_SettingCellsRXCompensation.SaveOkEventHandler += UpDateGlobalsSettings;
            //this.xSettingGrid_SettingCellsRYCompensation.SaveOkEventHandler += UpDateGlobalsSettings;

            //this.xSettingGrid_SettingCellsRRCompensation.SaveOkEventHandler += Form1.OnparameterChanged;
            //this.xSettingGrid_SettingCellsRXCompensation.SaveOkEventHandler += Form1.OnparameterChanged;
            //this.xSettingGrid_SettingCellsRYCompensation.SaveOkEventHandler += Form1.OnparameterChanged;
            Globals.AutoRunChangeSettingHandle += UpDateGlobalsSettings;
            this.xSettingGrid_SettingOption.SaveOkEventHandler += UpDateGlobalsSettings;
            Globals.AutoRunChangeSettingHandle += Form1.OnparameterChanged;
        }

        private void xSettingGrid_SettingOption_SaveOkEventHandler(object sender, EventArgs e)
        {
            XMachine.Instance.DoorEnabled = Globals.SettingOption.IsOpensafeDoor();
            XMachine.Instance.SafeDoorEStop = Globals.SettingOption.是否开启安全门急停复位;

            //UpDateCompensationSettings(Globals.SettingOption.GetSettingDOEPCB());
            UpDateCompensationSettings(Globals.IsDOEPCB);
            //if (Globals.SettingOption.GetSettingDOEPCB() != Globals.IsDOEPCB)
            //{

            //    if (!Globals.Keyence_Change_Setting(KeyenceService.Instance, Globals.SettingOption.GetSettingDOEPCB()))
            //        MessageBox.Show("左CCD控制器切换程序失败");
            //    if (!Globals.Keyence_Change_Setting(KeyenceService.Instance2, Globals.SettingOption.GetSettingDOEPCB()))
            //        MessageBox.Show("右CCD控制器切换程序失败");
            //    Globals.IsDOEPCB = Globals.SettingOption.GetSettingDOEPCB();
            //}

        }

        //每个补偿参数保存后，Globals里面的Setting要更新
        private void UpDateGlobalsSettings(object sender, EventArgs e)
        {
            //UpDateCompensationSettings(Globals.SettingOption.GetSettingDOEPCB());
            UpDateCompensationSettings(Globals.IsDOEPCB);
            //TempReader.Instance.EnableTempContorl = Globals.EnableTempContorl;
        }

        private void UpDateCompensationSettings(bool IsDOE)
        {
            if (IsDOE)
            {
                //this.xSettingGrid_SettingNozzlesCompensation.Id = (int)SettingId.吸头补偿参数_DOE;
                //this.xSettingGrid_SettingCellsLRCompensation.Id = (int)SettingId.左机穴位补偿参数R_DOE;
                //this.xSettingGrid_SettingCellsLXCompensation.Id = (int)SettingId.左机穴位补偿参数X_DOE;
                //this.xSettingGrid_SettingCellsLYCompensation.Id = (int)SettingId.左机穴位补偿参数Y_DOE;
                //this.xSettingGrid_SettingCellsRRCompensation.Id = (int)SettingId.右机穴位补偿参数R_DOE;
                //this.xSettingGrid_SettingCellsRXCompensation.Id = (int)SettingId.右机穴位补偿参数X_DOE;
                //this.xSettingGrid_SettingCellsRYCompensation.Id = (int)SettingId.右机穴位补偿参数Y_DOE;
                //this.xSettingGrid_SettingCalibration.Id = (int)SettingId.标定参数_DOE;
                this.xSettingGrid_SettingOption.Id = (int)SettingId.选项_DOE;
                this.xSettingGrid_ICT.Id=(int)SettingId.ICT;
                Globals.AddDOECompensationXYR();
            }
            else
            {
                //this.xSettingGrid_SettingNozzlesCompensation.Id = (int)SettingId.吸头补偿参数_PD;
                //this.xSettingGrid_SettingCellsLRCompensation.Id = (int)SettingId.左机穴位补偿参数R_PD;
                //this.xSettingGrid_SettingCellsLXCompensation.Id = (int)SettingId.左机穴位补偿参数X_PD;
                //this.xSettingGrid_SettingCellsLYCompensation.Id = (int)SettingId.左机穴位补偿参数Y_PD;
                //this.xSettingGrid_SettingCellsRRCompensation.Id = (int)SettingId.右机穴位补偿参数R_PD;
                //this.xSettingGrid_SettingCellsRXCompensation.Id = (int)SettingId.右机穴位补偿参数X_PD;
                //this.xSettingGrid_SettingCellsRYCompensation.Id = (int)SettingId.右机穴位补偿参数Y_PD;
                //this.xSettingGrid_SettingCalibration.Id = (int)SettingId.标定参数_PD;
                this.xSettingGrid_SettingOption.Id = (int)SettingId.选项_PD;
                this.xSettingGrid_ICT.Id = (int)SettingId.ICT;
                Globals.AddPDCompensationXYR();
            }
        }
    }
}
