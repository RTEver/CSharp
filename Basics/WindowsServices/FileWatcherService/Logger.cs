using System;
using System.IO;
using System.Threading;

namespace FileWatcherService
{
    internal sealed class Logger : Object
    {
        private FileSystemWatcher watcher;
        private Object locker;
        private Boolean enabled;

        public Logger()
            : base()
        {
            locker = new Object();
            enabled = true;

            watcher = new FileSystemWatcher("D:\\Temp");
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Deleted += Watcher_Deleted;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;

            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;

            enabled = false;
        }

        private void RecordEntry(String fileEvent, String filePath)
        {
            lock (locker)
            {
                using (var writer = new StreamWriter("D:\\templog.txt", true))
                {
                    writer.WriteLine(String.Format("{0} File {1} was {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));

                    writer.Flush();
                }
            }
        }

        private void Watcher_Renamed(Object sender, RenamedEventArgs e)
        {
            var fileEvent = "renamed in " + e.FullPath;

            var filePath = e.OldFullPath;

            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Deleted(Object sender, FileSystemEventArgs e)
        {
            string fileEvent = "deleted";

            string filePath = e.FullPath;

            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Changed(Object sender, FileSystemEventArgs e)
        {
            string fileEvent = "changed";

            string filePath = e.FullPath;

            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Created(Object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";

            string filePath = e.FullPath;

            RecordEntry(fileEvent, filePath);
        }
    }
}