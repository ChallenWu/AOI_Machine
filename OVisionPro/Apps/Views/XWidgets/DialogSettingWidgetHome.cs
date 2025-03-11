using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OVisionPro.Apps.Views.Class;
using OVisionPro.Services._01._Core.ShapesPics;
using OVisionPro.Services.ImageProcessing;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace OVisionPro.Apps.Views.XWidgets
{
    public partial class DialogSettingWidgetHome : Form
    {
        public string _imageFilePath = "";
        public int selectedTaskIDRun = 0;
        private readonly object lockObjectUpdateImg = new object();
        public Dictionary<string, ButtonToolBase> DicButton = new Dictionary<string, ButtonToolBase>() { };
        public Mat frameShow;
        public string imageFilePath {
            get { return _imageFilePath; }
            set {  _imageFilePath = value;  XVisionManager.Instance.ImgUrlTest = value; }
        }

        public void onShowLog(string content)
        {
            if (rictxtBxLog.InvokeRequired) // Kiểm tra xem có phải thread chính không
            {
                rictxtBxLog.Invoke(new Action(() =>
                {

                    rictxtBxLog.AppendText(content + "\n");
                }));
            }
            else
            {
                rictxtBxLog.AppendText(content);
            }
            
        }
        public void onListenRunState(string runID)
        {
            if (DicButton.ContainsKey(runID)) {
                foreach (var btn in DicButton.Keys) { DicButton[btn].BackColor = System.Drawing.Color.WhiteSmoke; }
                DicButton[runID].BackColor = System.Drawing.Color.DarkGray;
            }
        }
        public void OnUpdateImagePath(string path) { imageFilePath = path; }
        
        public DialogSettingWidgetHome()
        {
            InitializeComponent();
            LoadCBBModelName();
            LoadCBBTasksID();
            LoadFuncButtonAction();
            StartPosition = FormStartPosition.CenterParent;
            logW.Ins.onShowLog += onShowLog;
            XVisionManager.Instance.XcountBlocksAction += LoadFuncButtonAction;
            DragDropPictureBox.ActionUpdateimgFilePath += OnUpdateImagePath;
            cvX.onUpdateOutFrameToUIAction += UpdatePicturePropertyGrid;
            cvX.onListenRunStateAction += onListenRunState;
        }

        public void AddButton(string nameBtn)
        {
            if (!DicButton.ContainsKey(nameBtn)) {
                ButtonToolBase _button6 = new ButtonToolBase(cbbModelTasks);
                _button6.Text = nameBtn;
                _button6.Name = $"btn_{nameBtn}";
                DicButton.Add(nameBtn, _button6);
                this.panel5.Controls.Add(_button6);
            }
        }
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    cbbModelName?.Dispose();
                    cbbModelTasks?.Dispose();
                    XParameterManager.Instance.propertyGridData.PropertyChanged -= LoadFuncButtonAction;
                    XVisionManager.Instance.VisionRunMode = visionGlob.RunMode.production;
                    XVisionManager.Instance.XcountBlocksAction -= LoadFuncButtonAction;
                    DragDropPictureBox.ActionUpdateimgFilePath -= OnUpdateImagePath;
                    cvX.onUpdateOutFrameToUIAction -= UpdatePicturePropertyGrid;
                    cvX.onListenRunStateAction -= onListenRunState;
                    logW.Ins.onShowLog -= onShowLog;
                    // dispose shapes.
                    Shapes.Instance.shapes.Clear();
                    // dispose picturebox1 main.
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Dispose();
                    pictureBox1.Image = null;
                    pictureBox1 = null;
                    picBoxLoadPicture.Image?.Dispose();
                    picBoxLoadPicture?.Dispose();
                    picBoxLoadPicture.Image = null;
                    picBoxLoadPicture = null;
                    frameShow?.Dispose();
                    frameShow = null;
                    // Giải phóng tài nguyên hình ảnh của PictureBox nếu có
                    toolStrip2.Dispose();
                    toolStrip1.Dispose();
                    toolStripSeparator1.Dispose();
                    toolStripSeparator1 = null;
                    toolStrip2 = null;
                    toolStrip1 = null;
                    // Giải phóng tài nguyên của các điều khiển hoặc tài nguyên quản lý khác
                    if (components != null) { components.Dispose(); }
                    // Lặp qua tất cả các controls trong form và dispose từng cái
                    foreach (Control ctrl in this.Controls) { ctrl.Dispose(); }
                }
                GC.Collect();  // Yêu cầu Garbage Collector thu hồi bộ nhớ ngay lập tức
                //GC.WaitForFullGCComplete();
                //this.FormClosed -= DialogSettingWidget_FormClosed;
            }
            catch (Exception ex) { logW.Ins.error($"[HOME][0] ex: {ex}"); }
            base.Dispose(disposing);
        }
        public void clearButtons()
        {
            foreach (var btn in DicButton.Keys) { this.panel5.Controls.RemoveByKey($"btn_{btn}"); }
            foreach (var btn in DicButton.Keys) { DicButton[btn]?.Dispose(); }
            DicButton.Clear();
        }
        public void SortButtons()
        {
            DicButton = DicButton.OrderByDescending(kv => kv.Key).ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        public void RemoveAtButtons()
        {
            foreach (var btn in DicButton.Keys) { this.panel5.Controls.RemoveByKey($"btn_{btn}"); }
            displayCCDDebug(XVisionManager.Instance.taskDegging);
        }
        public void AddControlButtons()
        {
            foreach (var btn in DicButton.Keys) { this.panel5.Controls.Add(DicButton[btn]); }
            displayCCDDebug(XVisionManager.Instance.taskDegging);
        }

        public void displayCCDDebug(int id)
        {
            id += 1;
            string idtext = $"CCD{id}";
            foreach (var btn in DicButton.Keys) { 
                if (btn.StartsWith(idtext))
                { DicButton[btn].Show(); } 
                else 
                { DicButton[btn].Hide(); }; }
        }


        public void UpdatePicturePropertyGrid(int taskID, OpenCvSharp.Mat frame)
        {
            cvX.onUpdateOutFrameToUIAction -= UpdatePicturePropertyGrid;
            // Lock để ngăn chặn các luồng khác đồng thời truy cập
            lock (lockObjectUpdateImg)
            {
                if (OBasicAlgorithm.IsMat(frame)) {
                    try
                    {
                        frameShow?.Dispose();
                        frameShow = frame.Clone();
                        // Xóa hình ảnh cũ nếu có
                        pictureBox1.Image?.Dispose();
                        pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frameShow);
                    }
                    catch (Exception ex) { logW.Ins.Except($"[HOME][3] Error updating PictureBox: {ex.Message}"); }
                }
            }
            cvX.onUpdateOutFrameToUIAction += UpdatePicturePropertyGrid;
        }

        public void LoadCBBTasksID()
        {
            cbbModelTasks.Items.Clear();
            string selectModel = XModels.Instance.modelRunning;
            foreach (var key in XModels.Instance.TaskRuns[selectModel].Keys) { cbbModelTasks.Items.Add(key); }
            cbbModelTasks.SelectedIndex = 0;
            int.TryParse(cbbModelTasks.Text.ToString(), out selectedTaskIDRun);
        }
        public void LoadCBBModelName()
        {
            if (cbbModelName == null)  return;
            cbbModelName.Items.Clear();
            foreach (var key in XModels.Instance.TaskRuns.Keys)
            {
                cbbModelName.Items.Add(key);
            }
            cbbModelName.SelectedIndex = 0;
        }
        public void LoadFuncButtonAction()
        {
            try
            {
                if (panel5.InvokeRequired) {
                    panel5.Invoke(new Action(() =>
                    {
                        foreach (var key in XParameterManager.Instance.ucBlockBasicControllers.Keys)
                        { if (!key.StartsWith("ROI1") && !key.StartsWith("ROI2") && key != "") { AddButton(key); } }
                        SortButtons();
                        RemoveAtButtons();
                        AddControlButtons();
                    }));
                }
                else {
                    foreach (var key in XParameterManager.Instance.ucBlockBasicControllers.Keys)
                    { if (!key.StartsWith("ROI1") && !key.StartsWith("ROI2") && key != "") { AddButton(key); } }
                    SortButtons();
                    RemoveAtButtons();
                    AddControlButtons();
                }
                
            }
            catch (Exception ex)
            {
                logW.Ins.Except("[HOME][1] " + ex.ToString());
            }
        }
        private void btnLiveCamera_Click(object sender, EventArgs e)
        {
            try
            {
                using (var liveCCDInstance = new DialogSettingWidgetLiveCCD())
                {
                    liveCCDInstance.ShowDialog();
                };
            }
            catch { }
        }

        private void btnSaveCVConfig_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.SaveConfig();
            XParameterManager.Instance.LoadConfig();
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.LoadConfig();
        }

        private void btnReadImg_Click(object sender, EventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Select an Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    // Hiển thị ảnh trong PictureBox
                    picBoxLoadPicture.Image?.Dispose();
                    picBoxLoadPicture.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                    imageFilePath = openFileDialog.FileName;
                }
                catch (Exception ex) { logW.Ins.Except("[HOME][2]Error loading image: " + ex.Message); }
            }

        }

        private void btnStartDebug_Click(object sender, EventArgs e)
        {
            if (btnSetDebugging.Text == "DEBUG") logW.Ins.warning("DANG CHAY PRODUCTION. > CHON DEBUGGING.");
            else { XVisionManager.Instance.StartWorkInTaskDebug(int.Parse(cbbModelTasks.Text)); }
        }

        private void DialogSettingWidgetHome_FormClosed(object sender, FormClosedEventArgs e) {
            clearButtons();
        }

        private bool debuggMode = false;
        private void button8_Click(object sender, EventArgs e)
        {
            debuggMode = !debuggMode;
            if (debuggMode) { btnSetDebugging.Text = "DEBUGGING"; XVisionManager.Instance.VisionRunMode = visionGlob.RunMode.debug; }
            else { btnSetDebugging.Text = "DEBUG"; XVisionManager.Instance.VisionRunMode = visionGlob.RunMode.production; }
            
        }

        private void cbbModelTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            XVisionManager.Instance.taskDegging = int.Parse(cbbModelTasks.Text);
            displayCCDDebug(int.Parse(cbbModelTasks.Text));
        }
    }
}
