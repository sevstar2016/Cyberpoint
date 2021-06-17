using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using Prism.Common;
using Cyberpoint.ViewModels;
using Microsoft.Win32;
using Cyberpoint.commands;
using NuGet.Protocol.Plugins;

namespace Cyberpoint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool close = false;

        private RegVM rvm = new RegVM();
        private BronVM bvm = new BronVM();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = rvm;

            /*Thread thr = new Thread(new ThreadStart(KillProcess));
            thr.Priority = ThreadPriority.AboveNormal;
            thr.IsBackground = false;
            thr.Start();*/

            Client cl = new Client();

            //SetTaskManager(false);

            cl.StartClient();        
        }

        static void KillProcess()
        {
            do
            {
                Process[] processInfo = Process.GetProcessesByName("explorer");
                if (processInfo != null)
                {
                    try
                    {
                        foreach (Process p in processInfo)
                        p.Kill();
                    }
                    catch (Exception) { }
                }
            }
            while (!close);
        }

        void Close(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            //close = true;
            //Process.Start("explorer.exe");
        }

        public void U1()
        {
            DataContext = bvm;
        }

        public void U2()
        {
            DataContext = rvm;
            close = true;
            Process.Start("explorer.exe");
            SetTaskManager(false);
        }

        public void SetTaskManager(bool enable)
        {
            RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            if (enable && objRegistryKey.GetValue("DisableTaskMgr") != null)
                objRegistryKey.DeleteValue("DisableTaskMgr");
            else
                objRegistryKey.SetValue("DisableTaskMgr", "1");
            objRegistryKey.Close();
        }
    }
}
