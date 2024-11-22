using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Demo
{
    public   class XML
    {
        public static void WriteFileToXml(string filePath, Object objectToWrite)
        {
            StreamWriter streamWriter = null;
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                XmlSerializer xmlSerializer = new XmlSerializer(objectToWrite.GetType());
                streamWriter = new StreamWriter(filePath);
                xmlSerializer.Serialize(streamWriter, objectToWrite);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                streamWriter?.Close();
            }
        }
        public static T ReadFileFromXml<T>(string filePath, T ReadData)
        {
            FileStream fileStream = null;
            try
            {
                try
                {
                    if (!File.Exists(filePath))
                    {
                        WriteFileToXml(filePath, ReadData);
                    }

                    XmlSerializer xmlSerializer = new XmlSerializer(ReadData.GetType());
                    fileStream = new FileStream(filePath, FileMode.Open);
                    ReadData = (T)xmlSerializer.Deserialize(fileStream);
                }
                catch (Exception ex)
                {
                }

                return ReadData;
            }
            finally
            {
                fileStream?.Close();
            }
        }
    }
}
