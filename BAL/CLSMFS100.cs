using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANTRA;

namespace BAL
{
   

    public class MFS:MFS100
    {
        private static MFS instance = null;
        private MFS() { }
        private static object lockThis = new object();

        public static MFS GInstance
        {
            get
            {
                lock (lockThis)
                {
                    if (instance == null)
                        instance = new MFS();

                    return instance;
                }
            }
        }
    }
}
