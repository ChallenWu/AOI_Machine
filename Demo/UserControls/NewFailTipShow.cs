using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoTech;

namespace Demo.UserControls
{
    public class NewFailTipShow
    {
        private static NewFailTipShow instance;
        public static NewFailTipShow Instance
        {
            get { return instance == null ? instance = new NewFailTipShow() : instance; }
        }
        public NewFailTipShow()
        {
            lastAlarmEndTime = DateTime.Now;
        }
        private DateTime lastAlarmEndTime = DateTime.Now;
        private ConcurrentQueue<AlarmMessage> TemAlarm = new ConcurrentQueue<AlarmMessage>();
        //private List<NewFailTip> NFTArray = new List<NewFailTip>();


        private NewFailTip temnft;


        public void ShowPages(AlarmMessage am)
        {
            bool isAlarm = false;
            AsyncMainPageChanges();
            if (temnft == null || temnft.IsDisposed)
            {
                temnft = new NewFailTip(am, true);
            }

            for (int i = 0; i < NewFailTip.ll.Count; i++)
            {
                if (NewFailTip.ll[i].NowAlarm.ErrorCode == am.NowAlarm.ErrorCode)
                {
                    isAlarm = true;
                }
            }

            if (isAlarm == false)
            {
                NewFailTip.ll.Add(am);
                temnft.UpdateComBoxData();
                temnft.Show();

                am.EndTime = am.HappenTime.AddSeconds(1);
                DataServerManager.Instance.InsertAlarmON(am);

                //HiveManager.Instance.UploadMachineStateMessage(HiveState.Downtime, new HiveStateAddMessage { badge = AudioLoginManager.Instance.LoggedInAccount.UserID == null ? "" : AudioLoginManager.Instance.LoggedInAccount.UserID, error_message = am.NowAlarm.ErrorCode, error_detail = am.NowAlarm.MessageEn });
                //HiveManager.Instance.UploadMachineStateMessage(HiveState.Downtime, new HiveStateAddMessage { badge = AudioLoginManager.Instance.LoggedInAccount.UserID == null ? "" : AudioLoginManager.Instance.LoggedInAccount.UserID, error_message = am.NowAlarm.MessageEn, error_detail = am.NowAlarm.ErrorDetail });
            }
        }


        private void AsyncMainPageChanges()
        {
            //Frm_ICT_Main.Instance.BeginInvoke(new Action(() => {
            //    Frm_ICT_Main.Instance.Btn_Click(Frm_ICT_Main.Instance.Btn_Alarm, new EventArgs());
            //    Frm_ICT_Main.Instance.ChangeBtn_Hive(HiveState.Downtime);
            //    Frm_ICT_Main.Instance.Btn_Alarm.ButtonStateChange(NewButtonState.Alarm);
            //    //Frm_ICT_Main.Instance.Btn_Hive.ButtonStateChange(NewButtonState.)
            //}));
        }

        public void CloseAll()
        {
            try
            {
                if (NewFailTip.ll.Count >= 1)
                {
                    AlarmMessage temAm = NewFailTip.ll[0];
                    for (int i = 0; i < NewFailTip.ll.Count; i++)
                    {
                        temAm = temAm.HappenTime < NewFailTip.ll[i].HappenTime ? temAm : NewFailTip.ll[0];
                        temAm.EndTime = DateTime.Now;
                        //HiveManager.Instance.UploadErrorMessage(temAm);

                        temAm.EffectiveHappenTime = temAm.EffectiveHappenTime >= lastAlarmEndTime ? temAm.EffectiveHappenTime : lastAlarmEndTime;
                        lastAlarmEndTime = temAm.EndTime;
                        DataServerManager.Instance.UpdateAlarm(temAm);
                    }

                    NewFailTip.ll.Clear();
                }


            }
            catch (Exception ex)
            {

            }

        }


        public void CloseOne(NewFailTip nft)
        {
            AlarmMessage temAm = nft.CurrentAlarm;
            DateTime now = DateTime.Now;
            temAm.EffectiveHappenTime = temAm.EffectiveHappenTime >= lastAlarmEndTime ? temAm.EffectiveHappenTime : lastAlarmEndTime;
            lastAlarmEndTime = now;
            temAm.EndTime = now;

            if (temAm.EndTime.ToString("yyyy").Contains("0001"))
            {
                temAm.EndTime = DateTime.Now;
            }
            DataServerManager.Instance.UpdateAlarm(temAm);

            //HiveManager.Instance.UploadErrorMessage(temAm);
            //遍历报警 缓存，若所有关闭，则转换状态
        }
    }
}
