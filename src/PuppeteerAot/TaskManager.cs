using System;
using System.Linq;
using PuppeteerAot.Helpers;

namespace PuppeteerAot
{
    public class TaskManager
    {
        private ConcurrentSet<WaitTask> WaitTasks { get; } = new();

        public void Add(WaitTask waitTask) => WaitTasks.Add(waitTask);

        public void Delete(WaitTask waitTask) => WaitTasks.Remove(waitTask);

        public void RerunAll()
        {
            foreach (var waitTask in WaitTasks)
            {
                _ = waitTask.RerunAsync();
            }
        }

        public void TerminateAll(Exception exception)
        {
            while (!WaitTasks.IsEmpty)
            {
                _ = WaitTasks.First().TerminateAsync(exception);
            }
        }
    }
}
