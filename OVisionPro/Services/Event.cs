//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using AoIMachinE.Services.Task;

//namespace AoIMachinE.Services
//{
//    public abstract class Event
//    {
//        protected abstract void ProcessEventQueue(CancellationToken cancellationToken);
//        #region Task

//        private Task worker;
//        private CancellationTokenSource stopToken;

//        public bool IsRunning
//        {
//            get { return this.worker != null && !this.worker.IsCompleted; }
//        }

//        public void Register()
//        {
//        }

//        public void Unregister()
//        {
//        }

//        public void Start()
//        {
//            if (this.IsRunning)
//            {
//                return;
//            }
//            this.stopToken = new CancellationTokenSource();
//            this.worker = Task.Factory.StartNew(
//                () => this.ProcessEventQueue(this.stopToken.Token),
//                this.stopToken.Token,
//                TaskCreationOptions.LongRunning,
//                TaskScheduler.Default);
//        }

//        public void Stop()
//        {
//            if (!this.IsRunning || this.stopToken.IsCancellationRequested)
//            {
//                return;
//            }
//            try
//            {
//                this.stopToken.Cancel();
//            }
//            catch
//            {

//            }
//            this.worker = null;
//        }

//        #endregion
//    }
//}
