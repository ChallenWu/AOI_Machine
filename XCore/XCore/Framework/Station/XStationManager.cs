using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore
{
    public sealed class XStationManager : XObject
    {
        private Dictionary<int, XStation> stations = new Dictionary<int, XStation>();
        private readonly static XStationManager instance = new XStationManager();
        XStationManager()
        {
            BindStation(-1, "Controller");
        }
        public static XStationManager Instance
        {
            get { return instance; }
        }

        public void BindStation(int stationId, string name)
        {
            if (stations.ContainsKey(stationId) == false)
            {
                XStation station = new XStation(stationId, name);
                stations.Add(stationId, station);
            }
        }

        public XStation FindStationById(int stationId)
        {
            if (stations.ContainsKey(stationId) == false)
            {
                return null;
            }
            return stations[stationId];
        }

        public Dictionary<int, XStation> Stations
        {
            get { return stations; }
        }

        public void Start(object runMode)
        {
            foreach (XStation station in stations.Values)
                station.Start(runMode);
        }

        public void Pause()
        {
            foreach (XStation station in stations.Values)
                station.Pause();
        }

        public void Continue()
        {
            foreach (XStation station in stations.Values)
                station.Continue();
        }
        
        public void Reset()
        {
            foreach (XStation station in stations.Values)
                station.Reset();
        }

        public void Stop()
        {
            foreach (XStation station in stations.Values)
                station.Stop();
        }
    }
}
