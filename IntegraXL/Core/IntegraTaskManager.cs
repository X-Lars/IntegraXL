using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    /// <summary>
    /// Manages execution of enqueued INTEGRA-7 requests in a serial manner.
    /// </summary>
    public class IntegraTaskManager : INotifyPropertyChanged
    {
        private readonly Queue<Task<bool>> _RequestsQueue = new();
        private bool _IsRunning = false;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        //private readonly BlockingCollection<Task<bool>> _RequestsQueue = new();

        /// <summary>
        /// Initializes and starts the task manager.
        /// </summary>
        internal void Initialize()
        {
            //var thread = new Thread(new ThreadStart(Execute))
            //{
            //    IsBackground = true,
            //    Name = "TaskManager Thread"
            //};
            //thread.Start();
        }

        public int Count
        {
            get => _RequestsQueue.Count();
        }

        public bool IsRunning
        {
            get => _IsRunning;
        }

        /// <summary>
        /// Enqueues a task based request to the task manager queue.
        /// </summary>
        /// <param name="task">The task to enqueue for execution.</param>
        internal void Enqueue(Task<bool> task, CancellationToken token)
        {

            //lock(_RequestsQueue)
            //{
                _RequestsQueue.Enqueue(task);
                NotifyPropertyChanged(nameof(Count));
                Debug.Print($"[{nameof(IntegraTaskManager)}] Task Count = {Count}");

                if (!_IsRunning)
                {
                    _IsRunning = true;
                    NotifyPropertyChanged(nameof(IsRunning));
                    ThreadPool.QueueUserWorkItem(Execute, null);
                }
            //}

            
            
            //try
            //{
            //    _RequestsQueue.Add(task, token);
            //}
            //catch (OperationCanceledException)
            //{
            //    Debug.Print($"[{nameof(IntegraTaskManager)}.{nameof(Enqueue)}] Cancelled");
            //}
        }

        /// <summary>
        /// Executes all enqueued task based requests.
        /// </summary>
        private async void Execute(object ignored)
        {
            while(true)
            {
                Task<bool> task;

                lock(_RequestsQueue)
                {
                    if(_RequestsQueue.Count == 0)
                    {
                        _IsRunning = false;
                        NotifyPropertyChanged(nameof(IsRunning));
                        break;
                    }

                    task = _RequestsQueue.Dequeue();
                }

                
                try
                {
                    task.Start();
                    await task;
                    Debug.Print($"[{nameof(IntegraTaskManager)}] Task Complete");
                }
                catch
                {
                    Debug.Print($"[{nameof(IntegraTaskManager)}] Task Error");
                }

                NotifyPropertyChanged(nameof(Count));
                Debug.Print($"[{nameof(IntegraTaskManager)}] Task Count = {Count}");
            }
            //foreach (var task in _RequestsQueue.GetConsumingEnumerable())
            //{
            //    try
            //    {
            //        //if (!task.IsCompleted)
            //        task.Start();

            //        await task;
            //    }
            //    catch (TaskCanceledException)
            //    {
            //        Debug.Print($"[{nameof(IntegraTaskManager)}] Task Cancelled");
            //    }

            //    Debug.Print($"[{nameof(IntegraTaskManager)}] Done");
            //}
        }
    }


    ///// <summary>
    ///// Manages execution of enqueued INTEGRA-7 requests in a serial manner.
    ///// </summary>
    //public class IntegraTaskManager
    //{
    //    private readonly BlockingCollection<Task<bool>> _RequestsQueue = new ();

    //    /// <summary>
    //    /// Initializes and starts the task manager.
    //    /// </summary>
    //    internal void Initialize()
    //    {
    //        var thread = new Thread(new ThreadStart(Execute))
    //        {
    //            IsBackground = true,
    //            Name = "TaskManager Thread"
    //        };
    //        thread.Start();
    //    }

    //    /// <summary>
    //    /// Enqueues a task based request to the task manager queue.
    //    /// </summary>
    //    /// <param name="task">The task to enqueue for execution.</param>
    //    internal void Enqueue(Task<bool> task, CancellationToken token)
    //    {
    //        try
    //        {
    //            _RequestsQueue.Add(task, token);
    //        }
    //        catch(OperationCanceledException)
    //        {
    //            Debug.Print($"[{nameof(IntegraTaskManager)}.{nameof(Enqueue)}] Cancelled");
    //        }
    //    }

    //    /// <summary>
    //    /// Executes all enqueued task based requests.
    //    /// </summary>
    //    private async void Execute()
    //    {
    //        foreach (var task in _RequestsQueue.GetConsumingEnumerable())
    //        {
    //            try
    //            {
    //                //if (!task.IsCompleted)
    //                task.Start();

    //                await task;
    //            }
    //            catch (TaskCanceledException)
    //            {
    //                Debug.Print($"[{nameof(IntegraTaskManager)}] Task Cancelled");
    //            }

    //            Debug.Print($"[{nameof(IntegraTaskManager)}] Done");
    //        }
    //    }
    //}
}
