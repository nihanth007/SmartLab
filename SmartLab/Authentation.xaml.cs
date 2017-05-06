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
using System.Windows.Shapes;
using MANTRA;
using MySql.Data.MySqlClient;

namespace SmartLab
{
    public partial class Authentation : Window
    {
        MFS100 mfs100 = new MFS100();

        public Authentation()
        {
            InitializeComponent();
            check();
        }

        private bool check()
        {
            if (mfs100.IsConnected())
            {
                ShowMessage("Device Connected", false);
                DeviceInfo deviceInfo = null;
                int ret = mfs100.Init();
                if (ret != 0)
                {
                    //ShowMessage(mfs100.GetErrorMsg(ret), true);
                }
                else
                {
                    deviceInfo = mfs100.GetDeviceInfo();
                }
                return true;
            }
            else
            {
                ShowMessage("Device not connected", true);
                return false;
            }
        }

        public void ShowMessage(string msg, bool iserror)
        {
            MessageBox.Show(msg, "MFS100", MessageBoxButton.OK, (iserror ? MessageBoxImage.Error : MessageBoxImage.Information));
        }

    }
}
