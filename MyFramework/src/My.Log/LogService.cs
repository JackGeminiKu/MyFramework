using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace My.Log
{
    public static class LogService
    {
        static Log _defaultLog;

        static LogService()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));    // log4net配置文件
            _defaultLog = new Log(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
        }

        public static Log GetLogger(string name)
        {
            return new Log(name);
        }

        static Log Default
        {
            get { return _defaultLog; }
        }

        public static void Debug(object message)
        {
            Default.Debug(message);
        }

        public static void Debug(object message, Exception ex)
        {
            Default.Debug(message, ex);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            Default.DebugFormat(format, args);
        }

        public static void Info(object message)
        {
            Default.Info(message);
        }

        public static void Info(object message, Exception exception)
        {
            Default.Info(message, exception);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            Default.InfoFormat(format, args);
        }

        public static void Warn(object message)
        {
            Default.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            Default.Warn(message, exception);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            Default.WarnFormat(format, args);
        }

        public static void Error(object message)
        {
            Default.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            Default.Error(message, exception);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            Default.ErrorFormat(format, args);
        }

        public static void Fatal(object message)
        {
            Default.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            Default.Fatal(message, exception);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            Default.FatalFormat(format, args);
        }
    }



    public class Log
    {
        private ILog _log;

        internal Log(string name)
        {
            _log = LogManager.GetLogger(name);
        }

        internal Log(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Debug(object message, Exception ex)
        {
            _log.Debug(message, ex);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        public bool IsDebugEnabled { get { return _log.IsDebugEnabled; } }

        public bool IsInfoEnabled { get { return _log.IsInfoEnabled; } }

        public bool IsWarnEnabled { get { return _log.IsWarnEnabled; } }

        public bool IsErrorEnabled { get { return _log.IsErrorEnabled; } }

        public bool IsFatalEnabled { get { return _log.IsFatalEnabled; } }
    }

    //public static class LogService
    //{
    //    static ILog log = LogManager.GetLogger(typeof(LogService));


    //    static LogService()
    //    {
    //        XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
    //    }

    //    public static void Debug(object message)
    //    {
    //        log.Debug(message);
    //    }

    //    public static void DebugFormat(string format, params object[] args)
    //    {
    //        log.DebugFormat(format, args);
    //    }

    //    public static void Info(object message)
    //    {
    //        log.Info(message);
    //    }

    //    public static void InfoFormat(string format, params object[] args)
    //    {
    //        log.InfoFormat(format, args);
    //    }

    //    public static void Warn(object message)
    //    {
    //        log.Warn(message);
    //    }

    //    public static void Warn(object message, Exception exception)
    //    {
    //        log.Warn(message, exception);
    //    }

    //    public static void WarnFormat(string format, params object[] args)
    //    {
    //        log.WarnFormat(format, args);
    //    }

    //    public static void Error(object message)
    //    {
    //        log.Error(message);
    //    }

    //    public static void Error(object message, Exception exception)
    //    {
    //        log.Error(message, exception);
    //    }

    //    public static void ErrorFormat(string format, params object[] args)
    //    {
    //        log.ErrorFormat(format, args);
    //    }

    //    public static void Fatal(object message)
    //    {
    //        log.Fatal(message);
    //    }

    //    public static void Fatal(object message, Exception exception)
    //    {
    //        log.Fatal(message, exception);
    //    }

    //    public static void FatalFormat(string format, params object[] args)
    //    {
    //        log.FatalFormat(format, args);
    //    }

    //    public static bool IsDebugEnabled
    //    {
    //        get
    //        {
    //            return log.IsDebugEnabled;
    //        }
    //    }

    //    public static bool IsInfoEnabled
    //    {
    //        get
    //        {
    //            return log.IsInfoEnabled;
    //        }
    //    }

    //    public static bool IsWarnEnabled
    //    {
    //        get
    //        {
    //            return log.IsWarnEnabled;
    //        }
    //    }

    //    public static bool IsErrorEnabled
    //    {
    //        get
    //        {
    //            return log.IsErrorEnabled;
    //        }
    //    }

    //    public static bool IsFatalEnabled
    //    {
    //        get
    //        {
    //            return log.IsFatalEnabled;
    //        }
    //    }
    //} 
}
