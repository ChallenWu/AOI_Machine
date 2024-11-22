using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;
using System.Xml;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace XCore
{
    public sealed class XModelManager : XObject
    {
        private string dir_modelXml;
        private string file_modelXml;
        private string root_modelXml;
        private string file_modelNameXml;
        private string dir_BackUp;
        public const string AXISID = "AxisId";
        private const string TABLEHEAD = "Task";
        private readonly static XModelManager instance = new XModelManager();
        public Dictionary<string, string> nameMap = new Dictionary<string, string>();
        public ObservableCollection<string> modelname_realtime = new ObservableCollection<string> { };
        private string node;
        public int taskID;
        private string posXML;
        private string posNameXML;
        private string xmlRoot;
        private string dirbackupPosXML;
        public string Node
        {
            get { return node; }
        }
        XModelManager()
        {

        }
        public static XModelManager Instance
        {
            get { return instance; }
        }

        public void SetModelXmlPathAndRoot(string dir, string file_modelXml, string root, string dir_BackUp, int taskID, string posXML, string posNameXML, string xmlRoot, string dirbackupPosXML)
        {
            this.dir_modelXml = dir;
            this.file_modelXml = file_modelXml;
            this.root_modelXml = root;
            this.dir_BackUp = dir_BackUp;
            this.taskID = taskID;
            this.posXML = posXML;
            this.posNameXML = posNameXML;
            this.xmlRoot = xmlRoot;
            this.dirbackupPosXML = dirbackupPosXML;
            this.node = TABLEHEAD + taskID.ToString();
            string tmp_dir_modelXml = dir_modelXml + "GB-0123" + "\\";
            XPositionManager.Instance.SetPositionXmlPathAndRoot(tmp_dir_modelXml, posXML, posNameXML, xmlRoot, dirbackupPosXML);
            //Gán tọa độ cho từng task
            XPositionManager.Instance.BindPositionTableByTaskId((int)taskID);
        }

        public void load_position(string modelName)
        {
            string tmp_dir_modelXml = dir_modelXml + modelName + "\\";
            XPositionManager.Instance.SetPositionXmlPathAndRoot(tmp_dir_modelXml, posXML, posNameXML, xmlRoot, dirbackupPosXML);
            XPositionManager.Instance.BindPositionTableByTaskIdTry((int)taskID);
        }

        public void load_positionset(string modelName)
        {
            //Gán tọa độ cho từng task
            XPositionManager.Instance.LoadPositionSet();
        }


        public int LoadModelNames()
        {
            try
            {
                nameMap.Clear();
                string[] nodes1, content1;
                int result = XXml.ReadNodeAndInnerText(ModelXml_Path, root_modelXml + "//" + node, out nodes1, out content1);
                if (result >= 0 && nodes1.Length > 0)
                {
                    for (int i = 0; i < nodes1.Length; i++)
                    {
                        nameMap.Add(nodes1[i], MultiLanguage.GetPositonName(content1[i]));
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message, this.GetType().ToString());
                return -1;
            }
        }

        public int UpdateXMLModelNames()
        {
            try
            {
                nameMap.Clear();
                string[] nodes1, content1;
                string node = TABLEHEAD + taskID.ToString();
                int result = XXml.ReadNodeAndInnerText(ModelXml_Path, root_modelXml + "//" + node, out nodes1, out content1);
                if (result >= 0 && nodes1.Length > 0)
                {
                    for (int i = 0; i < nodes1.Length; i++)
                    {
                        nameMap.Add(nodes1[i], MultiLanguage.GetPositonName(content1[i]));
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message, this.GetType().ToString());
                return -1;
            }
        }

        public string ModelXml_Path
        {
            get { return dir_modelXml + file_modelXml; }
        }

        public string ModelXml_Root
        {
            get { return root_modelXml; }
        }

        public string BackUpModel_Dir
        {
            get { return dir_BackUp; }
        }

        public string ModelNameXml
        {
            get { return file_modelNameXml; }
        }
    }
}
