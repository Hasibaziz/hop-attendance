using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Structure
{
    public class ServerManager
    {
        private static ITaskManager _taskManager = null;
        public static ITaskManager GetTaskManager
        {
            get
            {
                ITaskManager objTaskManager = new ServerObject();
                return objTaskManager;
            }
        }
    }
}
