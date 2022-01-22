using System.Collections.Concurrent;
using System.Diagnostics;

namespace IntegraXL.Core
{
    public class IntegraTaskManager
    {
        private BlockingCollection<Task<bool>> _RequestsQueue = new();
        private bool _IsIdle = true;

        /// <summary>
        /// Initializes and starts the task manager.
        /// </summary>
        internal void Initialize()
        {
            var taskManager = new Thread(new ThreadStart(Execute));

            taskManager.IsBackground = true;
            taskManager.Start();
        }

        /// <summary>
        /// Enqueues a task based request to the task manager queue.
        /// </summary>
        /// <param name="task">The task to enqueue for execution.</param>
        internal void Enqueue(Task<bool> task)
        {
            _RequestsQueue.Add(task);
        }

        internal bool IsIdle
        {
            get => _IsIdle;
        }

        /// <summary>
        /// Executes all enqueued task based requests.
        /// </summary>
        private async void Execute()
        {
            foreach (var task in _RequestsQueue.GetConsumingEnumerable(CancellationToken.None))
            {
                _IsIdle = false;

                //Debug.Print($"{nameof(IntegraTaskManager)}.{nameof(Execute)} Begin {_RequestsQueue.Count}");

                task.Start();
                await task;

                _IsIdle = _RequestsQueue.Count == 0;
                Debug.Print($"[{nameof(IntegraTaskManager)}] Done");
            }
        }
    }
}
