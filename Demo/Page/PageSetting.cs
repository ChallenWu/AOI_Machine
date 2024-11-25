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
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;
using BoTech;
using Models;
using NPOI.SS.Formula.Functions;
using Demo.Setting;

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
            this.xSettingGrid_SettingOption.Id = (int)SettingId.Option_model1 + Globals.indexModel;
            this.xSettingGrid_ICT.Id = (int)SettingId.Paramter_Model  + Globals.indexModel;
            //UpDateCompensationSettings(LoadModel.currentModel.modelName);
           // propertyGrid1.SelectedObject = LoadModel.currentModel;
        }



        private void PageSetting_Load(object sender, EventArgs e)
        {
            this.xSettingGrid_SettingOption.SaveOkEventHandler += xSettingGrid_SettingOption_SaveOkEventHandler;
            this.xSettingGrid_SettingOption.SaveOkEventHandler += UpDateGlobalsSettings;
            //this.xSettingGrid_SettingOption.SaveOkEventHandler += Form1.OnparameterChanged;

            this.xSettingGrid_ICT.SaveOkEventHandler += UpDateGlobalsSettings;
           // this.xSettingGrid_ICT.SaveOkEventHandler += Form1.OnparameterChanged;

            Globals.AutoRunChangeSettingHandle += UpDateGlobalsSettings;
            //Globals.AutoRunChangeSettingHandle += Form1.OnparameterChanged;
        }

        private void xSettingGrid_SettingOption_SaveOkEventHandler(object sender, EventArgs e)
        {
            XMachine.Instance.DoorEnabled = Globals.SettingOption.IsOpensafeDoor();
            XMachine.Instance.SafeDoorEStop = Globals.SettingOption.是否开启安全门急停复位;
            UpDateCompensationSettings();


        }
        private void UpDateGlobalsSettings(object sender, EventArgs e)
        {
            UpDateCompensationSettings();
        }

        /// <summary>
        /// Update lại data setting
        /// </summary>
        /// <param name="IsDOE"></param>
        public void UpDateCompensationSettings()
        {
            DataServerManager.Instance.InsertOperationLog("Thay đổi parameter Config");

            //Globals.CreateModelFolder();

            //Console.WriteLine($"id Option {(int)SettingId.Option_model1 + Globals.indexModel}");
            //Console.WriteLine($"id Parameter {(int)SettingId.Paramter_Model + Globals.indexModel}");

            this.xSettingGrid_SettingOption.Id = (int)SettingId.Option_model1 + Globals.indexModel;
            this.xSettingGrid_ICT.Id = (int)SettingId.Paramter_Model + Globals.indexModel;

            Globals.ChangeParaterForEachModel();
        }

              


            //if (IsDOE)
            //{
            //    this.xSettingGrid_SettingOption.Id = (int)SettingId.选项_DOE;
            //    this.xSettingGrid_ICT.Id=(int)SettingId.ICT;
               
            //}
            //else
            //{
            //    this.xSettingGrid_SettingOption.Id = (int)SettingId.Option_PD;
            //    this.xSettingGrid_ICT.Id = (int)SettingId.ICT;
            //    Globals.AddPDCompensationXYR();
            //}
        //}
    }
}
