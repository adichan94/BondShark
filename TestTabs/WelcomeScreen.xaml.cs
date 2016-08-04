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
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Linq;

namespace TestTabs
{
    /// <summary>
    /// Interaction logic for WelcomeScreen.xaml
    /// </summary>
   
    public partial class WelcomeScreen : UserControl
    {
        public static string id = "";
        public static string password = "";
        List<Trader> traders = new List<Trader>();
        Trader t1 = new Trader("WD123456", "password1");
        Trader t2 = new Trader("AC123456", "password2");
        Trader t3 = new Trader("AW123456", "password3");
        List<Bond> bonds = new List<Bond>();
        public WelcomeScreen()
        {
            InitializeComponent();
            traders.Add(t1);
            traders.Add(t2);
            traders.Add(t3);
            try
            {
                var client = new WebClient();
                client.Proxy = null;
                Stream stream = client.OpenRead("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds");
                //StreamReader stream = new StreamReader("ebonds.json");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Bond[]));
                Bond[] bondsArray = (Bond[])serializer.ReadObject(stream);
                bonds = new List<Bond>(bondsArray);
            }
            catch
            {
                StreamReader stream = new StreamReader("ebonds.json");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Bond[]));
                Bond[] bondsArray = (Bond[])serializer.ReadObject(stream.BaseStream);
                bonds = new List<Bond>(bondsArray);
                DataSourceLabel.Content = "Data loaded locally";
            }
           
        }

        private void IDBox_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            IDLabel.Content = "";
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordLabel.Content = "";
        }

       
        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            {
                var client = new WebClient();
                client.Proxy = null;
                string result = client.DownloadString("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/login?User=" + IDBox.Text + "&password=" + passwordBox.Password.ToString());

                if (result == "Successful Login")
                {
                    MessageBox.Show(result);
                    MainWindow win = (MainWindow)Window.GetWindow(this);
                    foreach (Bond bond in bonds)
                    {
                        bond.setDates();
                    }
                    id = IDBox.Text;
                    password = passwordBox.Password.ToString();
                    win.ChangeScreen(bonds,id);
                }
            }
            //catch
            //{
            //    MessageBox.Show("Invalid credentials");
            //}
            //if (IDBox.Text == "")
            //{
            //    MessageBox.Show("Enter ID");
            //}
            //else
            //{
            //    if (passwordBox.Password.ToString() == "")
            //    {
            //        MessageBox.Show("Enter Password");
            //    }
            //    else
            //    {
            //        bool exist = traders.Any(trad => trad.traderId == IDBox.Text);
            //        if (exist)
            //        {
            //            Trader trad = traders.FirstOrDefault(t => t.traderId == IDBox.Text);
            //            if (trad.password == passwordBox.Password.ToString())
            //            {
            //                MainWindow win = (MainWindow)Window.GetWindow(this);
            //                foreach (Bond bond in bonds)
            //                {
            //                    bond.setDates();
            //                }
            //                win.ChangeScreen(bonds);
            //            }
            //            else
            //            {
            //                MessageBox.Show("Access denied.");
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("ID does not exist");
            //        }
            //    }
            //}
        }



        private void IDBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text == "")
            {
                IDLabel.Content = "ID";
            }
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password.ToString() == "")
            {
                PasswordLabel.Content = "Password";
            }
        }
    }
}
