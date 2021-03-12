using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace sanjigen.Engine
{
    public partial class Mesh
    {
        private sealed class AlwaysInlineScheduler : TaskScheduler
        {
            protected override void QueueTask(Task task) => ThreadPool.QueueUserWorkItem(s => TryExecuteTask((Task)s), task);
            protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) => TryExecuteTask(task);
            protected override IEnumerable<Task> GetScheduledTasks() => null;
        }
    }

}
