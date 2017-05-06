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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            uname = username.Text;
            pass = password.Password;
            string query = "select * from AppUsers where username = '" + uname + "'";
            MySqlConnection conn = new MySqlConnection("data source=localhost;database=labentry;User ID=gietlab;Password=gietcselabdb");
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


    }
}
