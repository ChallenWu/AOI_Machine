using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACS.SPiiPlusNET;

namespace XCore
{
    public class XDi : XObject
    {
        public ACS.SPiiPlusNET.Api Acs1;

        private XCard card;
        private int channel;
        private int actDiId;
        private string name;
        private DISTSTYPE m_STS;
        private bool m_PLS;
        private bool m_PLF;
        private DISTSTYPE m_DiStsLast;
        private string cardname;
        public XDi(XCard card, int channel, int actDiId, string name, string cardname)
        {
            this.card = card;
            this.channel = channel;
            this.actDiId = actDiId;
            this.name = name;
            this.cardname = cardname;
          
        }
        
        public int CardId { get; set; }

        public int TaskId { get; set; }

        public long Update()
        {
            int sts = 0;
            int value ;
            value = XDevice.Instance.FindCardById(CardId).GetDi(channel, actDiId, ref sts);
            m_STS = (DISTSTYPE)sts;
            return 0;
        }

        public int[] DI_Data = new int[200];
        
        public int GetDi(ref DISTSTYPE sts)
        {
            try
            {
                int sts1 = 0;
                int ret = XDevice.Instance.FindCardById(CardId).GetDi(channel, actDiId, ref sts1);

                sts = (DISTSTYPE)sts1;
                return ret;
                //object result1 = XDevice.Instance.FindCardById(CardId).ReadVarToVar("Din");
                //int[] readDin = (int[])result1;
                //int port = actDiId / 8;
                //int bit = actDiId % 8;

                //if ((readDin[port] & (1 << bit)) == 0)
                //    sts = 0;
                //else
                //    sts = 1;
            }
            catch
            {
                return -1;
            }
            //sts = DI_Data[actDiId];
            return 0;
        }

        private int ReadIntAxis(string variableName, int axisId)
        {
            Acs1 = new ACS.SPiiPlusNET.Api();
            var result = Acs1.ReadVariable(variableName, ProgramBuffer.ACSC_NONE, axisId, axisId);
            return Convert.ToInt32(result);
        }

        public int ActId
        {
            get
            {
                return actDiId;
            }
        }

        public int SetId { get; set; }

        public int Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string CardName
        {
            get
            {
                return cardname;
            }
        }
        public DISTSTYPE STS
        {
            get
            {
                lock (this)
                {
                    return m_STS;
                }
            }
        }

        public bool PLS
        {
            get
            {
                lock (this)
                {
                    return m_PLS;
                }
            }
        }

        public bool PLF
        {
            get
            {
                lock (this)
                {
                    return m_PLF;
                }
            }
        }

        
    }
}
