using System.Collections.Concurrent;
using System.Diagnostics;

namespace IntegraXL.Core
{
    /// <summary>
    /// Manages execution of enqueued INTEGRA-7 requests in a serial manner.
    /// </summary>
    public class IntegraTaskManager
    {
        private BlockingCollection<Task<bool>> _RequestsQueue = new BlockingCollection<Task<bool>>();

        /// <summary>
        /// Initializes and starts the task manager.
        /// </summary>
        internal void Initialize()
        {
            var thread = new Thread(new ThreadStart(Execute));

            thread.IsBackground = true;
            thread.Name = "TaskManager Thread";
            thread.Start();
        }

        public bool IsCancelled { get; private set; }
        /// <summary>
        /// Enqueues a task based request to the task manager queue.
        /// </summary>
        /// <param name="task">The task to enqueue for execution.</param>
        internal void Enqueue(Task<bool> task)
        {
            if (!task.IsCompleted && !task.IsCanceled)
                _RequestsQueue.Add(task);
        }

        /// <summary>
        /// Executes all enqueued task based requests.
        /// </summary>
        private async void Execute()
        {
            foreach (var task in _RequestsQueue.GetConsumingEnumerable())
            {
                try
                {
                    if (!task.IsCompleted)
                        task.Start();

                    await task;
                }
                catch (TaskCanceledException)
                {
                    Debug.Print($"[{nameof(IntegraTaskManager)}] Task Cancelled");
                }

                Debug.Print($"[{nameof(IntegraTaskManager)}] Done");
            }
        }
    }
}
