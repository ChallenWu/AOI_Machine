using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro
{
    class XTaskManager
    {
        private Dictionary<int, XTask> tasks = new Dictionary<int, XTask>();

        private readonly static XTaskManager instance = new XTaskManager();
        XTaskManager()
        {

        }
        public static XTaskManager Instance
        {
            get { return instance; }
        }
        //public void BindTask(int taskId, XTask task, string name)
        //{
        //    if (tasks.ContainsKey(taskId) == false)
        //    {
        //        task.TaskId = taskId;
        //        task.Name = name;
        //        tasks.Add(taskId, task);
        //    }
        //}

        //public XTask FindTaskById(int taskId)
        //{
        //    if (tasks.ContainsKey(taskId) == false)
        //    {
        //        return null;
        //    }
        //    return tasks[taskId];
        //}

        public void Initialize()
        {
            foreach (XTask task in tasks.Values)
            {
                task.Initialize();
            }
        }

        public void Exit()
        {
            foreach (XTask task in tasks.Values)
            {
                task.Exit();
            }
        }
    }
}