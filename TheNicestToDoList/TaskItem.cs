using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNicestToDoList
{
    internal class TaskItem
    {
        public string title { get; }
        public Priority priority { get; }
        public DateTime dueTime { get; }

        public TaskItem(string title, Priority priority, DateTime dueTime)
        {
            this.title = title;
            this.priority = priority;
            this.dueTime = dueTime;
        }

        public override string ToString()
        {
            return $"{title} | {priority} | {dueTime.ToString("yyyy-MM-dd HH:mm")}";
        }
    }
}
