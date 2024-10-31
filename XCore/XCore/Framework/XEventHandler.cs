using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore
{
    public abstract class XEventHandler : XObject
    {

        public void PostEvent(XEventHandler observer, XEventID xEventID, bool highPriorityFlag = false)
        {
            XController.Instance.EventServer.PostEvent(observer, xEventID, null, null, highPriorityFlag);
        }

        public void PostEventEStop(XStation xStation)
        {
            XEventArgs e = new XEventArgs();
            e.StationId = xStation.StationId;
            e.AlarmLevel = (int)XAlarmLevel.STOP;
            XController.Instance.EventServer.PostEvent(xStation, XEventID.ESTOP, e, null, true);
            //XController.Instance.AlarmEventServer.PostEvent(XAlarmReporter.Instance, XEventID.ESTOP, e, null, true);
        }

        public void PostEvent(XStation xStation, XAlarmLevel alarmLevel, XAlarmId sysAlarmId, int index = 0, bool flag = true,string text = "")
        {
            XEventArgs e = new XEventArgs();
            e.AlarmLevel = (int)alarmLevel;
            e.AlarmId = (int)sysAlarmId;
            e.IntValue = index;
            e.StationId = xStation.StationId;
            e.BoolValue = flag;
            e.StringValue = text;
            XController.Instance.EventServer.PostEvent(xStation, XEventID.ALARM, e, null, true);

            if ((sysAlarmId == XAlarmId.DOOR_OPEN || sysAlarmId == XAlarmId.AIR_LOW || sysAlarmId == XAlarmId.温控器异常) && (xStation.StationId == 0 || xStation.StationId == 1))
                XController.Instance.AlarmEventServer.PostEvent(XAlarmReporter.Instance, XEventID.ALARM, e, null, true);

            if (sysAlarmId != XAlarmId.DOOR_OPEN && sysAlarmId != XAlarmId.AIR_LOW && sysAlarmId != XAlarmId.温控器异常)
                XController.Instance.AlarmEventServer.PostEvent(XAlarmReporter.Instance, XEventID.ALARM, e, null, true);

        }

        public void PostEvent(XStation xStation, XAlarmLevel alarmLevel, XAlarmEventArgs args, string append = "")
        {
            args.Description += append;
            XEventArgs e = new XEventArgs();
            e.AlarmLevel = (int)alarmLevel;
            e.AlarmEventArgs = args;
            if(xStation!= null)
            e.StationId = xStation.StationId;
            e.AlarmId = 0;
            XController.Instance.EventServer.PostEvent(xStation, XEventID.ALARM, e, null, true);
            XController.Instance.AlarmEventServer.PostEvent(XAlarmReporter.Instance, XEventID.ALARM, e, null, true);
        }

        public void NotifyStations(XAlarmLevel alarmLevel, int alarmCode, string append = "")
        {
            XEventArgs e = new XEventArgs();
            e.AlarmLevel = (int)alarmLevel;
            e.AlarmId = alarmCode;
            e.StringValue = append;
            foreach (XStation station in XStationManager.Instance.Stations.Values)
            {
                XController.Instance.EventServer.PostEvent(station, XEventID.ALARM, e, null, true);
            }
            XController.Instance.AlarmEventServer.PostEvent(XAlarmReporter.Instance, XEventID.ALARM, e, null, true);
        }

        public virtual int HandleEvent(XEvent xEvent)
        {
            return -1;
        }
    }

   
}
