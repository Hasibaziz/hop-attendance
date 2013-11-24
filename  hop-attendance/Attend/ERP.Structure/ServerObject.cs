using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Structure.TaskManager;

namespace ERP.Structure
{
    public class ServerObject : ITaskManager
    {
        public object Execute(int moduleId, string methodName, object param)
        {
            ErpTaskManager obj = new ErpTaskManager();
            return obj.Execute(methodName, param);
        }

        public object Execute(int moduleId, string methodName)
        {
            ErpTaskManager obj = new ErpTaskManager();
            return obj.Execute(methodName);
        }
    }
}
