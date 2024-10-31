using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoTech
{
    public class MESManager
    {
        private MESManager instance;
        public MESManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MESManager();
                return instance;
            }    
        }
        private MESManager()
        {

        }
        public void UploadMesMessage()
        {

        }
    }
}
