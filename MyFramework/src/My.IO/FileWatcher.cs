using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace My.IO
{
    /// <summary>
    /// file watcher
    /// </summary>
    public class FileWatcher
    {
        private FileSystemWatcher _watcher = new FileSystemWatcher();

        public FileWatcher(string path)
        {
            _watcher.Path = System.IO.Path.GetDirectoryName(path);
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Filter = System.IO.Path.GetFileName(path);
            _watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            _watcher.EnableRaisingEvents = true;
        }

        public string Path { get; set; }

        public event EventHandler<FileWatcherEventArgs> WroteFile;

        protected void OnWroteFile(FileWatcherEventArgs e)
        {
            if (WroteFile != null) WroteFile(null, e);
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            DateTime lastWrite = File.GetLastWriteTime(Path);
            if (lastWrite != _lastRead) {
                _lastRead = lastWrite;
                OnWroteFile(new FileWatcherEventArgs(System.IO.Path.GetFileName(Path)));
            }
        }
        private DateTime _lastRead = DateTime.MinValue;

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
        }
    }


    /// <summary>
    /// file watcher event args
    /// </summary>
    public class FileWatcherEventArgs : EventArgs
    {
        public FileWatcherEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}
