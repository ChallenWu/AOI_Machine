using Demo.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;

namespace Demo.Id
{
    partial class GlobalsAutoConfig
    {
        private static void BindCard()
        {
            //Khai báo trục điều khiển
            //XDevice.Instance.BindCard((int)CardId.主设备1, 0, new XCommandCardACS(), CardId.主设备1.ToString());
            XDevice.Instance.BindCard((int)CardId.主设备1, 0, new XCommandCardGTS(), CardId.主设备1.ToString());
            XDevice.Instance.BindCard((int)CardId.主设备2, 0, new XCommandCardGTS(), CardId.主设备2.ToString());
            XDevice.Instance.BindCard((int)CardId.供料机1, 1, new XCommandCardGTS(), CardId.供料机1.ToString());
            XDevice.Instance.BindCard((int)CardId.供料机2, 2, new XCommandCardGTS(), CardId.供料机2.ToString());
            XDevice.Instance.BindCard((int)CardId.主设备3, 3, new XCommandCardGTS(), CardId.主设备3.ToString());
        }
        private static void BindAxis()
        {
            //Khai báo trục điều khiển
            //Xác định xem trục nào sẽ do khối motion dll nào kiểm soát 
            //Bộ điều khiển ACS
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.XAxis, 1, 1, AxisId.XAxis.ToString(), XAxisDirection.Left_Right);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.YAxis, 1, 1, AxisId.YAxis.ToString(), XAxisDirection.Left_Right);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.ZAxis, 1, 1, AxisId.ZAxis.ToString(), XAxisDirection.Up_Down,false);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.RAxis, 1, 1, AxisId.RAxis.ToString(), XAxisDirection.Rotate);


            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.左贴装X轴, 1, 1, AxisId.左贴装X轴.ToString(), XAxisDirection.Left_Right);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.右贴装X轴, 5, 1, AxisId.右贴装X轴.ToString(), XAxisDirection.Left_Right);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.左贴装Y轴, 0, 1, AxisId.左贴装Y轴.ToString(), XAxisDirection.Front_Back);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.右贴装Y轴, 4, 1, AxisId.右贴装Y轴.ToString(), XAxisDirection.Front_Back);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.左贴装Z1轴, 8, 1, AxisId.左贴装Z1轴.ToString(), XAxisDirection.Up_Down, false);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.左贴装Z2轴, 9, 1, AxisId.左贴装Z2轴.ToString(), XAxisDirection.Up_Down, false);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.左贴装Z3轴, 10, 1, AxisId.左贴装Z3轴.ToString(), XAxisDirection.Up_Down, false);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.右贴装Z1轴, 11, 1, AxisId.右贴装Z1轴.ToString(), XAxisDirection.Up_Down, false);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.右贴装Z2轴, 12, 1, AxisId.右贴装Z2轴.ToString(), XAxisDirection.Up_Down, false);
            XDevice.Instance.BindAxis((int)CardId.主设备1, (int)AxisId.右贴装Z3轴, 13, 1, AxisId.右贴装Z3轴.ToString(), XAxisDirection.Up_Down, false);
            //Bộ điều khiển GTS
            XDevice.Instance.BindAxis((int)CardId.主设备2, (int)AxisId.左贴装R1轴, 1, 0.01, AxisId.左贴装R1轴.ToString(), XAxisDirection.Rotate);
            XDevice.Instance.BindAxis((int)CardId.主设备2, (int)AxisId.左贴装R2轴, 2, 0.01, AxisId.左贴装R2轴.ToString(), XAxisDirection.Rotate);
            XDevice.Instance.BindAxis((int)CardId.主设备2, (int)AxisId.左贴装R3轴, 3, 0.01, AxisId.左贴装R3轴.ToString(), XAxisDirection.Rotate);
            XDevice.Instance.BindAxis((int)CardId.主设备2, (int)AxisId.右贴装R1轴, 4, 0.01, AxisId.右贴装R1轴.ToString(), XAxisDirection.Rotate);
            XDevice.Instance.BindAxis((int)CardId.主设备2, (int)AxisId.右贴装R2轴, 5, 0.01, AxisId.右贴装R2轴.ToString(), XAxisDirection.Rotate);
            XDevice.Instance.BindAxis((int)CardId.主设备2, (int)AxisId.右贴装R3轴, 6, 0.01, AxisId.右贴装R3轴.ToString(), XAxisDirection.Rotate);
        }
        private static void BindTask()
        {
            //Khai báo task
            Task70_Scanner task70_ScannerInTask = new Task70_Scanner(Globals.Dir_Task70);
            //Gắn task vào trong process
            XTaskManager.Instance.BindTask((int)TaskId.Task70_ScannerBox, task70_ScannerInTask, TaskId.Task70_ScannerBox.ToString());

        }
        private static void BindDi()
        {
            //********************************************************************************************************************************//
            //**********************************************主设备1***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.左贴装R1真空1到位, 0, 0, DiId.左贴装R1真空1到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.左贴装R2真空1到位, 0, 1, DiId.左贴装R2真空1到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.左贴装R3真空1到位, 0, 2, DiId.左贴装R3真空1到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.左贴装中转到位, 0, 3, DiId.左贴装中转到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.右贴装R1真空1到位, 0, 4, DiId.右贴装R1真空1到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.右贴装R2真空1到位, 0, 5, DiId.右贴装R2真空1到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.右贴装R3真空1到位, 0, 6, DiId.右贴装R3真空1到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.右贴装中转到位, 0, 7, DiId.右贴装中转到位.ToString(), CardId.主设备1.ToString());

            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.左贴装R1真空2到位, 1, 0, DiId.左贴装R1真空2到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.左贴装R2真空2到位, 1, 1, DiId.左贴装R2真空2到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.左贴装R3真空2到位, 1, 2, DiId.左贴装R3真空2到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.右贴装R1真空2到位, 1, 3, DiId.右贴装R1真空2到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.右贴装R2真空2到位, 1, 4, DiId.右贴装R2真空2到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.右贴装R3真空2到位, 1, 5, DiId.右贴装R3真空2到位.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.备用DI_8_1_6, 1, 6, DiId.备用DI_8_1_6.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备1, (int)DiId.备用DI_8_1_7, 1, 7, DiId.备用DI_8_1_7.ToString(), CardId.主设备1.ToString());



            //********************************************************************************************************************************//
            //**********************************************主设备2***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.主设备急停, 0, 0, DiId.主设备急停.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.启动按钮, 0, 1, DiId.启动按钮.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.停止按钮, 0, 2, DiId.停止按钮.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.复位按钮, 0, 3, DiId.复位按钮.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.下一台可以接收信号, 0, 4, DiId.下一台可以接收信号.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.上一台有载具信号, 0, 5, DiId.上一台有载具信号.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.上一台已装配失败信号, 0, 6, DiId.上一台已装配失败信号.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.储气罐压力侦测, 0, 7, DiId.储气罐压力侦测.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_0_8, 0, 8, DiId.备用DI_0_0_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装顶升真空吸到位, 0, 9, DiId.左贴装顶升真空吸到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装顶升真空吸到位, 0, 10, DiId.右贴装顶升真空吸到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_0_11, 0, 11, DiId.备用DI_0_0_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_0_12, 0, 12, DiId.备用DI_0_0_12.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_0_13, 0, 13, DiId.备用DI_0_0_13.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_0_14, 0, 14, DiId.备用DI_0_0_14.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_0_15, 0, 15, DiId.备用DI_0_0_15.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.进载对射1, 1, 0, DiId.进载对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.进载对射2, 1, 1, DiId.进载对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.进载对射3, 1, 2, DiId.进载对射3.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.进载阻挡原点, 1, 3, DiId.进载阻挡原点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.进载阻挡动点, 1, 4, DiId.进载阻挡动点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装对射1, 1, 5, DiId.左贴装对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装对射2, 1, 6, DiId.左贴装对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装阻挡原点, 1, 7, DiId.左贴装阻挡原点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装阻挡动点, 1, 8, DiId.左贴装阻挡动点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装顶升下降到位, 1, 9, DiId.左贴装顶升下降到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装顶升上升到位, 1, 10, DiId.左贴装顶升上升到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.过渡对射1, 1, 11, DiId.过渡对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.过渡对射2, 1, 12, DiId.过渡对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.过渡阻挡原点, 1, 13, DiId.过渡阻挡原点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.过渡阻挡动点, 1, 14, DiId.过渡阻挡动点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装对射1, 1, 15, DiId.右贴装对射1.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装对射2, 2, 0, DiId.右贴装对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装阻挡原点, 2, 1, DiId.右贴装阻挡原点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装阻挡动点, 2, 2, DiId.右贴装阻挡动点.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装顶升下降到位, 2, 3, DiId.右贴装顶升下降到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装顶升上升到位, 2, 4, DiId.右贴装顶升上升到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.出载对射1, 2, 5, DiId.出载对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.出载对射2, 2, 6, DiId.出载对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_2_7, 2, 7, DiId.备用DI_0_2_7.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_2_8, 2, 8, DiId.备用DI_0_2_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料1对射1, 2, 9, DiId.左预取料1对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料1对射2, 2, 10, DiId.左预取料1对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_2_11, 2, 11, DiId.备用DI_0_2_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_2_12, 2, 12, DiId.备用DI_0_2_12.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_2_13, 2, 13, DiId.备用DI_0_2_13.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_2_14, 2, 14, DiId.备用DI_0_2_14.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料1送回位置检测, 2, 15, DiId.左预取料1送回位置检测.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_0, 3, 0, DiId.备用DI_0_3_0.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_1, 3, 1, DiId.备用DI_0_3_1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_2, 3, 2, DiId.备用DI_0_3_2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料1真空吸到位, 3, 3, DiId.左预取料1真空吸到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料1顶升上升到位, 3, 4, DiId.左预取料1顶升上升到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料1顶升下降到位, 3, 5, DiId.左预取料1顶升下降到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料2对射1, 3, 6, DiId.左预取料2对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料2对射2, 3, 7, DiId.左预取料2对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_8, 3, 8, DiId.备用DI_0_3_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_9, 3, 9, DiId.备用DI_0_3_9.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_10, 3, 10, DiId.备用DI_0_3_10.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_11, 3, 11, DiId.备用DI_0_3_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料2送回位置检测, 3, 12, DiId.左预取料2送回位置检测.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_3_13, 3, 13, DiId.备用DI_0_3_13.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左贴装温度控制器状态, 3, 14, DiId.左贴装温度控制器状态.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右贴装温度控制器状态, 3, 15, DiId.右贴装温度控制器状态.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料2真空吸到位, 4, 0, DiId.左预取料2真空吸到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料2顶升上升到位, 4, 1, DiId.左预取料2顶升上升到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.左预取料2顶升下降到位, 4, 2, DiId.左预取料2顶升下降到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料1对射1, 4, 3, DiId.右预取料1对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料1对射2, 4, 4, DiId.右预取料1对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_4_5, 4, 5, DiId.备用DI_0_4_5.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_4_6, 4, 6, DiId.备用DI_0_4_6.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_4_7, 4, 7, DiId.备用DI_0_4_7.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_4_8, 4, 8, DiId.备用DI_0_4_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料1送回位置检测, 4, 9, DiId.右预取料1送回位置检测.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_4_10, 4, 10, DiId.备用DI_0_4_10.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_4_11, 4, 11, DiId.备用DI_0_4_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_4_12, 4, 12, DiId.备用DI_0_4_12.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料1真空吸到位, 4, 13, DiId.右预取料1真空吸到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料1顶升上升到位, 4, 14, DiId.右预取料1顶升上升到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料1顶升下降到位, 4, 15, DiId.右预取料1顶升下降到位.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料2对射1, 5, 0, DiId.右预取料2对射1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料2对射2, 5, 1, DiId.右预取料2对射2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_2, 5, 2, DiId.备用DI_0_5_2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_3, 5, 3, DiId.备用DI_0_5_3.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_4, 5, 4, DiId.备用DI_0_5_4.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_5, 5, 5, DiId.备用DI_0_5_5.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料2送回位置检测, 5, 6, DiId.右预取料2送回位置检测.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_7, 5, 7, DiId.备用DI_0_5_7.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_8, 5, 8, DiId.备用DI_0_5_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_9, 5, 9, DiId.备用DI_0_5_9.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料2真空吸到位, 5, 10, DiId.右预取料2真空吸到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料2顶升上升到位, 5, 11, DiId.右预取料2顶升上升到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.右预取料2顶升下降到位, 5, 12, DiId.右预取料2顶升下降到位.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_13, 5, 13, DiId.备用DI_0_5_13.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_14, 5, 14, DiId.备用DI_0_5_14.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备2, (int)DiId.备用DI_0_5_15, 5, 15, DiId.备用DI_0_5_15.ToString(), CardId.主设备2.ToString());



            //********************************************************************************************************************************//
            //**********************************************供料机1***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机急停, 0, 0, DiId.左供料机急停.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_0_1, 0, 1, DiId.备用DI_1_0_1.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左上仓对射, 0, 2, DiId.左供料机左上仓对射.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左下仓对射, 0, 3, DiId.左供料机左下仓对射.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右上仓对射, 0, 4, DiId.左供料机右上仓对射.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右下仓对射, 0, 5, DiId.左供料机右下仓对射.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左上仓按钮, 0, 6, DiId.左供料机左上仓按钮.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左下仓按钮, 0, 7, DiId.左供料机左下仓按钮.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右上仓按钮, 0, 8, DiId.左供料机右上仓按钮.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右下仓按钮, 0, 9, DiId.左供料机右下仓按钮.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左上仓气缸原点, 0, 10, DiId.左供料机左上仓气缸原点.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左下仓气缸原点, 0, 11, DiId.左供料机左下仓气缸原点.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右上仓气缸原点, 0, 12, DiId.左供料机右上仓气缸原点.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右下仓气缸原点, 0, 13, DiId.左供料机右下仓气缸原点.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左上仓气缸动点, 0, 14, DiId.左供料机左上仓气缸动点.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左下仓气缸动点, 0, 15, DiId.左供料机左下仓气缸动点.ToString(), CardId.供料机1.ToString());

            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右上仓气缸动点, 1, 0, DiId.左供料机右上仓气缸动点.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右下仓气缸动点, 1, 1, DiId.左供料机右下仓气缸动点.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左上仓门禁, 1, 2, DiId.左供料机左上仓门禁.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机左下仓门禁, 1, 3, DiId.左供料机左下仓门禁.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右上仓门禁, 1, 4, DiId.左供料机右上仓门禁.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机右下仓门禁, 1, 5, DiId.左供料机右下仓门禁.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机1料仓有料, 1, 6, DiId.左供料机1料仓有料.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.左供料机2料仓有料, 1, 7, DiId.左供料机2料仓有料.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_8, 1, 8, DiId.备用DI_1_1_8.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_9, 1, 9, DiId.备用DI_1_1_9.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_10, 1, 10, DiId.备用DI_1_1_10.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_11, 1, 11, DiId.备用DI_1_1_11.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_12, 1, 12, DiId.备用DI_1_1_12.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_13, 1, 13, DiId.备用DI_1_1_13.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_14, 1, 14, DiId.备用DI_1_1_14.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机1, (int)DiId.备用DI_1_1_15, 1, 15, DiId.备用DI_1_1_15.ToString(), CardId.供料机1.ToString());



            //********************************************************************************************************************************//
            //************************************************************供料机2************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机急停, 0, 0, DiId.右供料机急停.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_0_1, 0, 1, DiId.备用DI_2_0_1.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左上仓对射, 0, 2, DiId.右供料机左上仓对射.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左下仓对射, 0, 3, DiId.右供料机左下仓对射.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右上仓对射, 0, 4, DiId.右供料机右上仓对射.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右下仓对射, 0, 5, DiId.右供料机右下仓对射.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左上仓按钮, 0, 6, DiId.右供料机左上仓按钮.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左下仓按钮, 0, 7, DiId.右供料机左下仓按钮.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右上仓按钮, 0, 8, DiId.右供料机右上仓按钮.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右下仓按钮, 0, 9, DiId.右供料机右下仓按钮.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左上仓气缸原点, 0, 10, DiId.右供料机左上仓气缸原点.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左下仓气缸原点, 0, 11, DiId.右供料机左下仓气缸原点.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右上仓气缸原点, 0, 12, DiId.右供料机右上仓气缸原点.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右下仓气缸原点, 0, 13, DiId.右供料机右下仓气缸原点.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左上仓气缸动点, 0, 14, DiId.右供料机左上仓气缸动点.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左下仓气缸动点, 0, 15, DiId.右供料机左下仓气缸动点.ToString(), CardId.供料机2.ToString());

            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右上仓气缸动点, 1, 0, DiId.右供料机右上仓气缸动点.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右下仓气缸动点, 1, 1, DiId.右供料机右下仓气缸动点.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左上仓门禁, 1, 2, DiId.右供料机左上仓门禁.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机左下仓门禁, 1, 3, DiId.右供料机左下仓门禁.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右上仓门禁, 1, 4, DiId.右供料机右上仓门禁.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机右下仓门禁, 1, 5, DiId.右供料机右下仓门禁.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机1料仓有料, 1, 6, DiId.右供料机1料仓有料.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.右供料机2料仓有料, 1, 7, DiId.右供料机2料仓有料.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_8, 1, 8, DiId.备用DI_2_1_8.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_9, 1, 9, DiId.备用DI_2_1_9.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_10, 1, 10, DiId.备用DI_2_1_10.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_11, 1, 11, DiId.备用DI_2_1_11.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_12, 1, 12, DiId.备用DI_2_1_12.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_13, 1, 13, DiId.备用DI_2_1_13.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_14, 1, 14, DiId.备用DI_2_1_14.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDi((int)CardId.供料机2, (int)DiId.备用DI_2_1_15, 1, 15, DiId.备用DI_2_1_15.ToString(), CardId.供料机2.ToString());



            //********************************************************************************************************************************//
            //**********************************************主设备3***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.主设备门禁1, 0, 0, DiId.主设备门禁1.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.备用DI_3_0_1, 0, 1, DiId.备用DI_3_0_1.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.主设备门禁3, 0, 2, DiId.主设备门禁3.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.备用DI_3_0_3, 0, 3, DiId.备用DI_3_0_3.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.备用DI_3_0_4, 0, 4, DiId.备用DI_3_0_4.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.备用DI_3_0_5, 0, 5, DiId.备用DI_3_0_5.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.左工位R1温控报警, 0, 6, DiId.左工位R1温控报警.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.左工位R2温控报警, 0, 7, DiId.左工位R2温控报警.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.左工位R3温控报警, 0, 8, DiId.左工位R3温控报警.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.右工位R1温控报警, 0, 9, DiId.右工位R1温控报警.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.右工位R2温控报警, 0, 10, DiId.右工位R2温控报警.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.右工位R3温控报警, 0, 11, DiId.右工位R3温控报警.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.回流线下一台可以接收信号, 0, 12, DiId.回流线下一台可以接收信号.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.回流线上一台有载具信号, 0, 13, DiId.回流线上一台有载具信号.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.回流线进载漫反射, 0, 14, DiId.回流线进载漫反射.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDi((int)CardId.主设备3, (int)DiId.回流线出载漫反射, 0, 15, DiId.回流线出载漫反射.ToString(), CardId.主设备3.ToString());

        }
        private static void BindDo()
        {
            //********************************************************************************************************************************//
            //**********************************************主设备1***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R1真空1吸, 0, 0, DoId.左贴装R1真空1吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R1真空1破, 0, 1, DoId.左贴装R1真空1破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R2真空1吸, 0, 2, DoId.左贴装R2真空1吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R2真空1破, 0, 3, DoId.左贴装R2真空1破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R3真空1吸, 0, 4, DoId.左贴装R3真空1吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R3真空1破, 0, 5, DoId.左贴装R3真空1破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装上定位光源, 0, 6, DoId.左贴装上定位光源.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装上相机触发, 0, 7, DoId.左贴装上相机触发.ToString(), CardId.主设备1.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左下定位光源, 1, 0, DoId.左下定位光源.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左下相机触发, 1, 1, DoId.左下相机触发.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R1真空1吸, 1, 2, DoId.右贴装R1真空1吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R1真空1破, 1, 3, DoId.右贴装R1真空1破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R2真空1吸, 1, 4, DoId.右贴装R2真空1吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R2真空1破, 1, 5, DoId.右贴装R2真空1破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R3真空1吸, 1, 6, DoId.右贴装R3真空1吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R3真空1破, 1, 7, DoId.右贴装R3真空1破.ToString(), CardId.主设备1.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装上定位光源, 2, 0, DoId.右贴装上定位光源.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装上相机触发, 2, 1, DoId.右贴装上相机触发.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右下定位光源, 2, 2, DoId.右下定位光源.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右下相机触发, 2, 3, DoId.右下相机触发.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R1真空2吸, 2, 4, DoId.左贴装R1真空2吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R1真空2破, 2, 5, DoId.左贴装R1真空2破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R2真空2吸, 2, 6, DoId.左贴装R2真空2吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R2真空2破, 2, 7, DoId.左贴装R2真空2破.ToString(), CardId.主设备1.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R3真空2吸, 3, 0, DoId.左贴装R3真空2吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.左贴装R3真空2破, 3, 1, DoId.左贴装R3真空2破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R1真空2吸, 3, 2, DoId.右贴装R1真空2吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R1真空2破, 3, 3, DoId.右贴装R1真空2破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R2真空2吸, 3, 4, DoId.右贴装R2真空2吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R2真空2破, 3, 5, DoId.右贴装R2真空2破.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R3真空2吸, 3, 6, DoId.右贴装R3真空2吸.ToString(), CardId.主设备1.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备1, (int)DoId.右贴装R3真空2破, 3, 7, DoId.右贴装R3真空2破.ToString(), CardId.主设备1.ToString());


            //********************************************************************************************************************************//
            //**********************************************主设备1***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.红灯, 0, 0, DoId.红灯.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.黄灯, 0, 1, DoId.黄灯.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.绿灯, 0, 2, DoId.绿灯.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.蜂鸣, 0, 3, DoId.蜂鸣.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.给下一台有载具信号, 0, 4, DoId.给下一台有载具信号.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.给下一台装配失败信号, 0, 5, DoId.给下一台装配失败信号.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.给上一台可以接收信号, 0, 6, DoId.给上一台可以接收信号.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装中转真空吸, 0, 7, DoId.左贴装中转真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装中转真空破, 0, 8, DoId.左贴装中转真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装中转真空吸, 0, 9, DoId.右贴装中转真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装中转真空破, 0, 10, DoId.右贴装中转真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装顶升真空吸, 0, 11, DoId.左贴装顶升真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装顶升真空破, 0, 12, DoId.左贴装顶升真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装顶升真空吸, 0, 13, DoId.右贴装顶升真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装顶升真空破, 0, 14, DoId.右贴装顶升真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_0_15, 0, 15, DoId.备用DO_0_0_15.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.进载单控阻挡, 1, 0, DoId.进载单控阻挡.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.进载启动PU, 1, 1, DoId.进载启动PU.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.进载方向DR, 1, 2, DoId.进载方向DR.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.进载释放MF, 1, 3, DoId.进载释放MF.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装单控阻挡, 1, 4, DoId.左贴装单控阻挡.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_1_5, 1, 5, DoId.备用DO_0_1_5.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装顶升上升, 1, 6, DoId.左贴装顶升上升.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装启动PU, 1, 7, DoId.左贴装启动PU.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装方向DR, 1, 8, DoId.左贴装方向DR.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左贴装释放MF, 1, 9, DoId.左贴装释放MF.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.过渡单控阻挡, 1, 10, DoId.过渡单控阻挡.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.过渡启动PU, 1, 11, DoId.过渡启动PU.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.过渡方向DR, 1, 12, DoId.过渡方向DR.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.过渡释放MF, 1, 13, DoId.过渡释放MF.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装单控阻挡, 1, 14, DoId.右贴装单控阻挡.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_1_15, 1, 15, DoId.备用DO_0_1_15.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装顶升上升, 2, 0, DoId.右贴装顶升上升.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装启动PU, 2, 1, DoId.右贴装启动PU.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装方向DR, 2, 2, DoId.右贴装方向DR.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右贴装释放MF, 2, 3, DoId.右贴装释放MF.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_2_4, 2, 4, DoId.备用DO_0_2_4.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.出载启动PU, 2, 5, DoId.出载启动PU.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.出载方向DR, 2, 6, DoId.出载方向DR.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.出载释放MF, 2, 7, DoId.出载释放MF.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_2_8, 2, 8, DoId.备用DO_0_2_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_2_9, 2, 9, DoId.备用DO_0_2_9.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_2_10, 2, 10, DoId.备用DO_0_2_10.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_2_11, 2, 11, DoId.备用DO_0_2_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左预取料1真空吸, 2, 12, DoId.左预取料1真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左预取料1真空破, 2, 13, DoId.左预取料1真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左预取料1顶升上升, 2, 14, DoId.左预取料1顶升上升.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_2_15, 2, 15, DoId.备用DO_0_2_15.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_0, 3, 0, DoId.备用DO_0_3_0.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_1, 3, 1, DoId.备用DO_0_3_1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_2, 3, 2, DoId.备用DO_0_3_2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_3, 3, 3, DoId.备用DO_0_3_3.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_4, 3, 4, DoId.备用DO_0_3_4.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_5, 3, 5, DoId.备用DO_0_3_5.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_6, 3, 6, DoId.备用DO_0_3_6.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左预取料2真空吸, 3, 7, DoId.左预取料2真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左预取料2真空破, 3, 8, DoId.左预取料2真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左预取料2顶升上升, 3, 9, DoId.左预取料2顶升上升.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_10, 3, 10, DoId.备用DO_0_3_10.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_11, 3, 11, DoId.备用DO_0_3_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_12, 3, 12, DoId.备用DO_0_3_12.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_3_13, 3, 13, DoId.备用DO_0_3_13.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.左工位温度控制, 3, 14, DoId.左工位温度控制.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右工位温度控制, 3, 15, DoId.右工位温度控制.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_0, 4, 0, DoId.备用DO_0_4_0.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_1, 4, 1, DoId.备用DO_0_4_1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右预取料1真空吸, 4, 2, DoId.右预取料1真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右预取料1真空破, 4, 3, DoId.右预取料1真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右预取料1顶升上升, 4, 4, DoId.右预取料1顶升上升.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_5, 4, 5, DoId.备用DO_0_4_5.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_6, 4, 6, DoId.备用DO_0_4_6.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_7, 4, 7, DoId.备用DO_0_4_7.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_8, 4, 8, DoId.备用DO_0_4_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_9, 4, 9, DoId.备用DO_0_4_9.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_10, 4, 10, DoId.备用DO_0_4_10.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_11, 4, 11, DoId.备用DO_0_4_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_4_12, 4, 12, DoId.备用DO_0_4_12.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右预取料2真空吸, 4, 13, DoId.右预取料2真空吸.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右预取料2真空破, 4, 14, DoId.右预取料2真空破.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.右预取料2顶升上升, 4, 15, DoId.右预取料2顶升上升.ToString(), CardId.主设备2.ToString());

            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_0, 5, 0, DoId.备用DO_0_5_0.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_1, 5, 1, DoId.备用DO_0_5_1.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_2, 5, 2, DoId.备用DO_0_5_2.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_3, 5, 3, DoId.备用DO_0_5_3.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_4, 5, 4, DoId.备用DO_0_5_4.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_5, 5, 5, DoId.备用DO_0_5_5.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_6, 5, 6, DoId.备用DO_0_5_6.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_7, 5, 7, DoId.备用DO_0_5_7.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_8, 5, 8, DoId.备用DO_0_5_8.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_9, 5, 9, DoId.备用DO_0_5_9.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_10, 5, 10, DoId.备用DO_0_5_10.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_11, 5, 11, DoId.备用DO_0_5_11.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_12, 5, 12, DoId.备用DO_0_5_12.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_13, 5, 13, DoId.备用DO_0_5_13.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_14, 5, 14, DoId.备用DO_0_5_14.ToString(), CardId.主设备2.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备2, (int)DoId.备用DO_0_5_15, 5, 15, DoId.备用DO_0_5_15.ToString(), CardId.主设备2.ToString());


            //********************************************************************************************************************************//
            //**********************************************供料机1***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_0_0, 0, 0, DoId.备用DO_1_0_0.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_0_1, 0, 1, DoId.备用DO_1_0_1.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机左上仓气缸, 0, 2, DoId.左供料机左上仓气缸.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机左下仓气缸, 0, 3, DoId.左供料机左下仓气缸.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机右上仓气缸, 0, 4, DoId.左供料机右上仓气缸.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机右下仓气缸, 0, 5, DoId.左供料机右下仓气缸.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机左上轴刹车, 0, 6, DoId.左供料机左上轴刹车.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机左下轴刹车, 0, 7, DoId.左供料机左下轴刹车.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机右上轴刹车, 0, 8, DoId.左供料机右上轴刹车.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机右下轴刹车, 0, 9, DoId.左供料机右下轴刹车.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机左上仓按钮灯, 0, 10, DoId.左供料机左上仓按钮灯.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机左下仓按钮灯, 0, 11, DoId.左供料机左下仓按钮灯.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机右上仓按钮灯, 0, 12, DoId.左供料机右上仓按钮灯.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.左供料机右下仓按钮灯, 0, 13, DoId.左供料机右下仓按钮灯.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_0_14, 0, 14, DoId.备用DO_1_0_14.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_0_15, 0, 15, DoId.备用DO_1_0_15.ToString(), CardId.供料机1.ToString());

            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_0, 1, 0, DoId.备用DO_1_1_0.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_1, 1, 1, DoId.备用DO_1_1_1.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_2, 1, 2, DoId.备用DO_1_1_2.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_3, 1, 3, DoId.备用DO_1_1_3.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_4, 1, 4, DoId.备用DO_1_1_4.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_5, 1, 5, DoId.备用DO_1_1_5.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_6, 1, 6, DoId.备用DO_1_1_6.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_7, 1, 7, DoId.备用DO_1_1_7.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_8, 1, 8, DoId.备用DO_1_1_8.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_9, 1, 9, DoId.备用DO_1_1_9.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_10, 1, 10, DoId.备用DO_1_1_10.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_11, 1, 11, DoId.备用DO_1_1_11.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_12, 1, 12, DoId.备用DO_1_1_12.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_13, 1, 13, DoId.备用DO_1_1_13.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_14, 1, 14, DoId.备用DO_1_1_14.ToString(), CardId.供料机1.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机1, (int)DoId.备用DO_1_1_15, 1, 15, DoId.备用DO_1_1_15.ToString(), CardId.供料机1.ToString());



            //********************************************************************************************************************************//
            //**********************************************供料机2***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_0_0, 0, 0, DoId.备用DO_2_0_0.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_0_1, 0, 1, DoId.备用DO_2_0_1.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机左上仓气缸, 0, 2, DoId.右供料机左上仓气缸.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机左下仓气缸, 0, 3, DoId.右供料机左下仓气缸.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机右上仓气缸, 0, 4, DoId.右供料机右上仓气缸.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机右下仓气缸, 0, 5, DoId.右供料机右下仓气缸.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机左上轴刹车, 0, 6, DoId.右供料机左上轴刹车.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机左下轴刹车, 0, 7, DoId.右供料机左下轴刹车.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机右上轴刹车, 0, 8, DoId.右供料机右上轴刹车.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机右下轴刹车, 0, 9, DoId.右供料机右下轴刹车.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机左上仓按钮灯, 0, 10, DoId.右供料机左上仓按钮灯.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机左下仓按钮灯, 0, 11, DoId.右供料机左下仓按钮灯.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机右上仓按钮灯, 0, 12, DoId.右供料机右上仓按钮灯.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.右供料机右下仓按钮灯, 0, 13, DoId.右供料机右下仓按钮灯.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_0_14, 0, 14, DoId.备用DO_2_0_14.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_0_15, 0, 15, DoId.备用DO_2_0_15.ToString(), CardId.供料机2.ToString());

            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_0, 1, 0, DoId.备用DO_2_1_0.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_1, 1, 1, DoId.备用DO_2_1_1.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_2, 1, 2, DoId.备用DO_2_1_2.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_3, 1, 3, DoId.备用DO_2_1_3.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_4, 1, 4, DoId.备用DO_2_1_4.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_5, 1, 5, DoId.备用DO_2_1_5.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_6, 1, 6, DoId.备用DO_2_1_6.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_7, 1, 7, DoId.备用DO_2_1_7.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_8, 1, 8, DoId.备用DO_2_1_8.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_9, 1, 9, DoId.备用DO_2_1_9.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_10, 1, 10, DoId.备用DO_2_1_10.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_11, 1, 11, DoId.备用DO_2_1_11.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_12, 1, 12, DoId.备用DO_2_1_12.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_13, 1, 13, DoId.备用DO_2_1_13.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_14, 1, 14, DoId.备用DO_2_1_14.ToString(), CardId.供料机2.ToString());
            XDevice.Instance.BindDo((int)CardId.供料机2, (int)DoId.备用DO_2_1_15, 1, 15, DoId.备用DO_2_1_15.ToString(), CardId.供料机2.ToString());


            //********************************************************************************************************************************//
            //**********************************************主设备3***********************************************************************//
            //********************************************************************************************************************************//
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_0, 0, 0, DoId.备用DO_3_0_0.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_1, 0, 1, DoId.备用DO_3_0_1.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_2, 0, 2, DoId.备用DO_3_0_2.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_3, 0, 3, DoId.备用DO_3_0_3.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_4, 0, 4, DoId.备用DO_3_0_4.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_5, 0, 5, DoId.备用DO_3_0_5.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.回流线正转, 0, 6, DoId.回流线正转.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.回流线反转, 0, 7, DoId.回流线反转.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_8, 0, 8, DoId.备用DO_3_0_8.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_9, 0, 9, DoId.备用DO_3_0_9.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_10, 0, 10, DoId.备用DO_3_0_10.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_11, 0, 11, DoId.备用DO_3_0_11.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.回流线给下一台有载具信号, 0, 12, DoId.回流线给下一台有载具信号.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.回流线给上一台可以接收信号, 0, 13, DoId.回流线给上一台可以接收信号.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_14, 0, 14, DoId.备用DO_3_0_14.ToString(), CardId.主设备3.ToString());
            XDevice.Instance.BindDo((int)CardId.主设备3, (int)DoId.备用DO_3_0_15, 0, 15, DoId.备用DO_3_0_15.ToString(), CardId.主设备3.ToString());
        }
        private static void RegisterAxis()
        {
            //Đăng ký thông tin số lượng trục điều khiển sử dụng trong taskId 
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterAxis((int)AxisId.XAxis);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterAxis((int)AxisId.YAxis);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterAxis((int)AxisId.ZAxis);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterAxis((int)AxisId.RAxis);
        }

        private static void RegisterDi()
        {
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDi((int)DiId.左预取料1对射1);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDi((int)DiId.左预取料1对射2);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDi((int)DiId.左预取料1送回位置检测);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDi((int)DiId.左预取料1真空吸到位);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDi((int)DiId.左预取料1顶升上升到位);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDi((int)DiId.左预取料1顶升下降到位);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDi((int)DiId.左供料机1料仓有料);
        }

        private static void RegisterDo()
        {
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.左预取料1真空吸);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.左预取料1真空破);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.左预取料1顶升上升);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.左预取料2真空吸);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.左预取料2真空破);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.左预取料2顶升上升);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.右预取料1真空吸);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.右预取料1真空破);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.右预取料1顶升上升);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.右预取料2真空吸);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.右预取料2真空破);
            XTaskManager.Instance.FindTaskById((int)TaskId.Task70_ScannerBox).RegisterDo((int)DoId.右预取料2顶升上升);

        }
        public static void BindDevice()
        {
            BindCard();
            BindAxis();
            BindTask();
            BindDi();
            BindDo();
            RegisterAxis();
            RegisterDi();
            RegisterDo();
        }
    }
}
