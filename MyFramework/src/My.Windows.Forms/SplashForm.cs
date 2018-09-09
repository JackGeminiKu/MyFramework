using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My.Windows.Forms
{
    public sealed partial class SplashForm : Form
    {
        SplashForm()
        {
            InitializeComponent();
        }

        static SplashForm _instance = new SplashForm();

        public static SplashForm GetInstance()
        {
            return _instance;
        }
    }
}
