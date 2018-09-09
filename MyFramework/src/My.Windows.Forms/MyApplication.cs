using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace System.Windows.Forms
{
    public static class MyApplication
    {
        public static string RootPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }


        public static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void Delay(int millisecond)
        {
            Stopwatch watch = Stopwatch.StartNew();
            do {
                System.Windows.Forms.Application.DoEvents();
            } while (watch.ElapsedMilliseconds < millisecond);
        }
    }
}
