using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore
{
    class XCommandCardOffline : XCommandCard
    {
        public bool IsConnected;

        private static XCommandCardOffline instance = new XCommandCardOffline();

        public XCommandCardOffline()
        {
        }
        public static XCommandCardOffline Instance
        {
            get { return instance; }
        }

        public override int Initial(string IP, bool simulator = false) 
        {
            return 0;
        }


        public override int Close(int actCardId)
        {
            return 0;
        }

        public override string ReadStringScalar(string variableName)
        {
            return  "0";
        }

        public override string ReadVariableBuffer(string variableName, int buffer)
        {
            return "0";
        }

         private int ReadIntAxis(string variableName, int axisId)
        {
            return 0;
        }

        private double  ReadDoubleAxis(string variableName, int axisId)
        {
            return 0;
        }

        public override int WritePos(int actCardId, object array, string name)
        {
            //double[] testaarray = (double[])array; 
            return 0;
        }

          
        public override int WriteLocalVariable(string variableName, string value, int BufferId)
        {
            return 0;
        }

        public override int WriteGlobalVariable(string variableName, string value)
        {
            return 0;
        }

        public void WriteDoubleScalar(string variableName, double value)
        {
        }

        public void WriteDoubleAxis(string variableName, double value, int axisId)
        {
        }

        public override int ReadDoneInt_HomeAll(int actCardId)
        {
            return 0;
        }
        public override void WriteVarToString(string variableName, string value)
        {
            return;
        }


        public override object ReadVarToVar(string var)
        {
            return null;
        }


        public override int ReadVarToInt(string var )
        {
            return 0;
        }

        public override string ReadVarToString(string var)
        {
            return "";
        }

        public override double  ReadVarToDouble(string var)
        {
            return 0;
        }

        private  void WriteInt(string variableName, int value)
        {
        }
        public void WriteIntBuffer(string variableName, int value, int bufferId)
        {
        }

        public void WriteDoubleBuffer(string variableName, double value, int bufferId)
        {
        }

        //Write double array to buffer 
        public void WriteDoubleArrayBuf(string variableName, double[] value, int bufferId)
        {
        }

        public void WriteDoubleArray(string variableName, double[] value)
        {
        }

        public override int Update(int actCardId)
        {
            return 0;
        }

        public override int SetDo(int actCardId, int channel, int index, int sts)
        {
            return 0;
        }

        public override int GetDo(int actCardId, int channel, int index, ref int sts)
        {
            return 0;
        }

        public override int GetDi(int actCardId, int channel, int index, ref int sts)
        {
            return 0;
        }

        public override int SetServo(int actCardId, int axisId, bool on)
        {
            return 0;
        }

        public override int GoHome_All(int actCardId,int value)
        {
            return 0;
        }
        public override int GoHome(int actCardId, int axisId)
        {
            return 0;
        }

        public override int RunBuffer(int actCardId,int buffer, string label)
        {
            return 0 ;
        }

        public override int MoveAbs(int actCardId, int axisId, double position, double vel)
        {
            return 0;
        }

        public override int MoveRel(int actCardId, int axisId, double distance, double vel)
        {
            return 0;
        }

        public override int MoveJog(int actCardId, int axisId, int IsStart,double vel)
        {
            return 0;
        }

        public override int Stop(int actCardId, int axisId)
        {
            return 0;
        }

        public override int EStop(int actCardId, int axisId)
        {
            return 0;
        }

        public override int SetAxisAccAndDec(int actCardId, int axisId, double acc, double dec)
        {
            return 0;
        }

        public override int GetMotionIo(int actCardId, int axisId, ref int sts)//需要确认
        {
            return 0;
        }

        public override int GetMotionSts(int actCardId, int axisId, ref int sts)//需要确认
        {
            return 0;

        }

        public override int GetMotionPos(int actCardId, int axisId, ref double pos)
        {
            return 0;
           
        }

        public override int GetCommandPos(int actCardId, int axisId, ref double pos)
        {
            return 0;
        }

    }
}
