using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCore.Config
{
    class XModels
    {
        public void LoadDataModels(string filePath)
        {
            try
            {
                // Sử dụng StreamReader để đọc từng dòng
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
        protected int count;
        public string modelName { get; set; }

        public int Count
        {
            get { return count; }
        }
    }
}
