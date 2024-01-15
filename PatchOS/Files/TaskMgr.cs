using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files
{
    public class TaskMgr
    {
        // properties
        public string Name { get; private set; }
        public string UID { get; private set; }
        public int ID { get; set; }

        // memory usage
        public uint MemoryUsed { get; set; }
        public uint MemoryFree { get; set; }
        public uint MemoryTotal { get; set; }

        // constructor
        public TaskMgr(string name, string uid)
        {
            this.Name = name;
            this.UID = uid;
            this.ID = 0;
        }

    }

    public static class TaskManager
    {
        // list of tasks
        public static List<Task> List { get; private set; } = new List<Task>();

        // register task
        public static void RegisterTask(Task task)
        {
            List.Add(task);
        }

        // unregister task
        public static void EndTask(TaskMgr task)
        {
            List.RemoveAt(task.ID);
        }

  
    }
}
