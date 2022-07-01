using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.Threading;

namespace WinManager
{
    static class Program
    {
        private static Form1 form;

        private static string msg = "Version 0.1 (Stable). Developed by Timur Uzun, 2022.";

        private static bool isDesktopBuildNumberShown = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            MessageBox.Show(msg);
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
            InitializeDesktopBuildNumberShowState();
        }

        public static void DisableAntiSpyware()
        {
            try
            {
                if (AskIfUserSure())
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", true);
                    key.SetValue("DisableAntiSpyware", 1, RegistryValueKind.DWord);
                    MessageBox.Show("Windows Defender has been disabled.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void DisableUpdates()
        {
            try
            {
                if (AskIfUserSure())
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows", true);
                    RegistryKey winSubKey = key.OpenSubKey("WindowsUpdate", true) ?? key.CreateSubKey("WindowsUpdate", true);
                    RegistryKey au = winSubKey.OpenSubKey("AU", true) ?? winSubKey.CreateSubKey("AU", true);
                    au.SetValue("NoAutoUpdate", 1, RegistryValueKind.DWord);
                    if (au.GetValue("NoAutoUpdate").ToString() == "1")
                    {
                        MessageBox.Show("Updates have been disabled.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeDesktopBuildNumberShowState()
        {
            try
            {
                if (AskIfUserSure())
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                    key.SetValue("PaintDesktopVersion", isDesktopBuildNumberShown ? 0 : 1);
                    InitializeDesktopBuildNumberShowState();
                    if (isDesktopBuildNumberShown) MessageBox.Show($"Desktop build number change has been enabled, refresh desktop.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else MessageBox.Show($"Desktop build number change has been disabled, refresh desktop.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InitializeDesktopBuildNumberShowState()
        {
            try
            {
                string value = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("PaintDesktopVersion").ToString();
                switch (value)
                {
                    case "0":
                        form.deskBldNum.Text = "Enable Desktop Build Number";
                        isDesktopBuildNumberShown = false;
                        break;

                    case "1":
                        form.deskBldNum.Text = "Disable Desktop Build Number";
                        isDesktopBuildNumberShown = true;
                        break;

                    default:
                        form.deskBldNum.Text = "Enable Desktop Build Number";
                        isDesktopBuildNumberShown = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool AskIfUserSure()
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else return false;
        }
    }
}
