using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static OVisionPro.Globals;

namespace OVisionPro
{
    class XTask
    {
        // onStep để display current step while RUN 
        // be will add on wigets
        public event Action<string, Color> OnStep;
        private Thread _thread;
        // TH dừng lại để xem nó chạy đến đâu trong giao diện block_basic
        private RunState taskstate = new RunState();

        protected int LastStartTime;
        protected int StartTimeForEmptyRun;

        [DllImport("kernel32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetTickCount();
        public void SetStep(string step, Color color)
        {
            if (OnStep != null)
            {
                OnStep(step, color);
            }
        }

        public int TaskId { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// /// Running
        /// /// </summary>
        public void Start(object runMode)
        {
            if (_thread != null)
            {
                _thread.Abort();
            }

            _thread = new Thread(new ParameterizedThreadStart(Running));
            _thread.IsBackground = true;
            taskstate = RunState.running;
            SetStep("Running", Color.Green);
            _thread.Start(taskstate);
        }
        /// <summary>
        /// 复位，调用Homing
        /// </summary>
        public void Reset()
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
            _thread = new Thread(new ThreadStart(Homing));
            _thread.IsBackground = true;
            taskstate = RunState.running;
            _thread.Start();
        }
        /// <summary>
        /// Huy bo luong
        /// </summary>
        public void Cancel()
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
            SetStep("Stop", Color.Green);
        }

        /// <summary>
        /// 任务初始化
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// 任务退出
        /// </summary>
        public virtual void Exit()
        {
            Cancel();
        }
        /// <summary>
        /// 任务运行，需用户重写
        /// </summary>
        protected virtual void Running(object runMode) { }
        /// <summary>
        /// 任务复位，需用户重写
        /// </summary>
        protected virtual void Homing()
        {
        }

        protected virtual void PauseActive()        // Tạm dừng kích hoạt
        {
            taskstate = RunState.pausing;
        }

        protected virtual void ContinueActive()     // tiếp tục chạy
        {
            taskstate = RunState.running;
            LastStartTime = GetTickCount();         // Đặt lại thời gian trạng thái trước
        }
    }
}
