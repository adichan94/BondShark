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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;
using System.Windows.Threading;

namespace TestTabs
{
    /// <summary>
    /// Interaction logic for TradeBlotter.xaml
    /// </summary>
    public partial class TradeBlotter : UserControl
    {
        
        public TradeBlotter(string id)
        {
            InitializeComponent();
            try
            {
                var client = new WebClient();
                client.Proxy = null;
                Stream stream = client.OpenRead("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/tradertradedetails?Username=" + id);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Trade[]));
                Trade[] tradesArray = (Trade[])serializer.ReadObject(stream);
                Trade.trades = tradesArray.ToList();
            }
            catch
            {
                ;
            }
            foreach(Trade trade in Trade.trades)
            {
                trade.FormatDate();
                trade.setPrices();
            }
            TradesDataGrid.ItemsSource = Trade.trades;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
            dispatcherTimer.Start();
        }

        public void dispatcherTimer_tick(object sender, EventArgs e)
        {
            TradesDataGrid.ItemsSource = Trade.trades;
        }

        public void FormatColumns()
        {
            foreach(var column in TradesDataGrid.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                if(column.Header.ToString() == "year" || column.Header.ToString() == "month" || 
                    column.Header.ToString() == "day" || column.Header.ToString() == "hour" ||
                    column.Header.ToString() == "minutes" || column.Header.ToString() == "seconds")
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
