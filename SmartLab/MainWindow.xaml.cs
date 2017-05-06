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
using MANTRA;
using MySql.Data.MySqlClient;

namespace SmartLab
{
    public partial class MainWindow : Window
    {
        string uname = null;
        string pass = null;
        MFS100 mfs100 = new MFS100();
        public MainWindow()
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            uname = username.Text;
            pass = password.Password;
            string query = "select * from AppUsers where username = '" + uname + "'";
            MySqlConnection conn = new MySqlConnection("data source=.;database=labentry;User ID=gietlab;Password=gietcselabdb");
            MySqlCommand cmd = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                statusMessage.Text = "Error Connecting to the Server";
                return;
            }
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                string temp = (string)reader.GetValue(1);
                conn.Close();
                if (temp == pass)
                {
                    Authentation auth = new Authentation();
                    auth.Show();
                    this.Close();
                }
                else
                    statusMessage.Text = "The password provided for the username is Invalid!";
            }
            else
            {
                statusMessage.Text = "User does not Exist";
            }
        }

        public void ShowMessage(string msg, bool iserror)
        {
            MessageBox.Show(msg, "MFS100", MessageBoxButton.OK, (iserror ? MessageBoxImage.Error : MessageBoxImage.Information));
        }

    }
}
