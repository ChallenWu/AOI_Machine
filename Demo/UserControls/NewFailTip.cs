using BoTech;
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
    public partial class NewFailTip : Form
    {
        public AlarmMessage CurrentAlarm;
        public static List<AlarmMessage> ll = new List<AlarmMessage>();//用于存报警数据；
        private List<string> cList = new List<string>();


        public NewFailTip(AlarmMessage am, bool isOnlyOne = true)
        {
            InitializeComponent();


            /* DFM2.5 */
            this.CurrentAlarm = am;
            this.txt_StartTime.Text = am.HappenTime.ToString("yyyy/MM/dd HH:mm:ss");
            this.txt_Code.Text = am.NowAlarm.ErrorCode;
            this.txt_ErrorMsg.Text = am.NowAlarm.MessageCn;//
            this.txt_ErrorDetail.Text = am.NowAlarm.ErrorDetail;
            this.cbox_ErrorMsgList.Items.Add(am.NowAlarm.MessageCn);
            this.rBox_ErrorDetail.Text = am.NowAlarm.ErrorDetail;
            this.cbox_ErrorMsgList.SelectedIndex = 0;

            if (isOnlyOne)
            {
                btnRetry.Visible = false;
            }

            this.StartPosition = FormStartPosition.CenterParent;
            this.TopMost = true;

        }


        /*更新combox数据下拉表；*/
        public void UpdateComBoxData()
        {


            cList = GetStringsList();
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    comboxData();
                }));
            }
            else
            {
                comboxData();
            }
        }


        private List<string> GetStringsList()
        {
            List<string> cList = new List<string>();
            if (ll.Count > 0)
            {
                string ss;
                for (int i = 0; i < ll.Count; i++)
                {
                    ss = ll[i].NowAlarm.MessageCn;
                    cList.Add(ss);
                }
            }

            return cList;
        }


        private void btnAffirm_Click(object sender, EventArgs e)
        {
            //if (AudioLoginManager.Instance.LoggedInAccount.UserLevel < LoginLevel.Level2)
            //{
            //    MessageBox.Show("Quyền hiện tại thấp hơn Level 2");
            //    return;
            //}
            this.DialogResult = DialogResult.OK;
            nClose();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            nClose();
        }


        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            nClose();
        }


        private void nClose()
        {

            if (ll.Count == 0)
            {
                this.Close();
                this.Dispose();
            }
            else
            {
                try
                {
                    cList = GetStringsList();
                    int index = cbox_ErrorMsgList.SelectedIndex;
                    CurrentAlarm = ll[index];
                    NewFailTipShow.Instance.CloseOne(this);
                    ll.RemoveAt(index);
                    cList = GetStringsList();

                    DispFirstAmData();//默认显示第一条数据；
                    comboxData();


                    if (ll.Count == 0)
                    {
                        this.Close();
                    }


                }
                catch (Exception ex)
                {

                }

            }
        }


        private void comboxData()
        {
            cbox_ErrorMsgList.Items.Clear();
            for (int i = 0; i < cList.Count; i++)
            {
                cbox_ErrorMsgList.Items.Add(cList[i]);
            }
        }

        //public void UserLevelChange(AccountInfo AI)
        //{

        //    try
        //    {
        //        if (AI.UserLevel <= LoginLevel.Level1)
        //        {
        //            btnAffirm.Enabled = false;


        //        }
        //        else
        //        {
        //            btnAffirm.Enabled = true;

        //        }
        //    }
        //    catch (Exception ex)
        //    { }

        //}





        private void NewFailTip_FormClosing(object sender, FormClosingEventArgs e)
        { }


        private void cbox_ErrorMsgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ll.Count > 0)
            {
                int index = cbox_ErrorMsgList.SelectedIndex;
                AlarmMessage am = new AlarmMessage();
                if (ll.Count >= index)
                {
                    am = ll[index];
                    this.CurrentAlarm = am;
                    this.txt_StartTime.Text = am.HappenTime.ToString("yyyy/MM/dd HH:mm:ss");
                    this.txt_Code.Text = am.NowAlarm.ErrorCode;
                    this.txt_ErrorMsg.Text = am.NowAlarm.MessageCn;//
                    this.txt_ErrorDetail.Text = am.NowAlarm.ErrorDetail;
                    this.rBox_ErrorDetail.Text = am.NowAlarm.ErrorDetail;
                }

            }

        }



        /*界面默认显示：第一条ALARM数据*/
        private void DispFirstAmData()
        {

            if (ll.Count > 0)
            {
                AlarmMessage am = new AlarmMessage();
                am = ll[0];
                this.CurrentAlarm = am;
                this.txt_StartTime.Text = am.HappenTime.ToString("yyyy/MM/dd HH:mm:ss");
                this.txt_Code.Text = am.NowAlarm.ErrorCode;
                this.txt_ErrorMsg.Text = am.NowAlarm.MessageCn;//
                this.txt_ErrorDetail.Text = am.NowAlarm.ErrorDetail;
                this.rBox_ErrorDetail.Text = am.NowAlarm.ErrorDetail;
            }

        }


        /*这里用定时器去刷登录权限的变化*/
        private void timer1_Tick(object sender, EventArgs e)
        {
        }


    }
}
