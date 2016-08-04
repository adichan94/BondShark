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
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;

namespace TestTabs
{
    /// <summary>
    /// Interaction logic for TradeWindow.xaml
    /// </summary>
    public partial class TradeWindow : UserControl
    {
        Bond bond = new Bond();
        public TradeWindow(Bond selectedBond)
        {
            InitializeComponent();
            bond = selectedBond;
            ISINLabel.Content = selectedBond.isin.ToString();
            IssuerLabel.Content = selectedBond.issuerName.ToString();
            YieldLabel.Content = selectedBond.yield.ToString();
            if(selectedBond.change > 0)
            {
                ChangeLabel.Content = selectedBond.change.ToString();
                this.ChangeLabel.Foreground = System.Windows.Media.Brushes.Green;
                GreenArrow.Visibility = Visibility.Visible;
            }
            else if(selectedBond.change == 0)
            {
                ChangeLabel.Content = selectedBond.change.ToString();
                this.ChangeLabel.Foreground = System.Windows.Media.Brushes.Yellow;
            }
            else
            {
                ChangeLabel.Content = selectedBond.change.ToString();
                this.ChangeLabel.Foreground = System.Windows.Media.Brushes.Red;
                RedArrow.Visibility = Visibility.Visible;
            }
            if(selectedBond.category == "corp")
            {
                CategoryLabel.Content = "Corporate";
            }
            else
            {
                CategoryLabel.Content = "Government";
            }
            IssueDateLabel.Content = selectedBond.Issue_Date.Date;
            MaturityDateLabel.Content = selectedBond.MaturityDate.Date;
            CouponRateLabel.Content = selectedBond.coupon;
            CouponFrequencyLabel.Content = selectedBond.couponFrequency;
            CurrencyLabel.Content = selectedBond.currency;
            Field_Label.Content = selectedBond.field;
            HighLabel.Content = selectedBond.high;
            LowLabel.Content = selectedBond.low;
            LastLabel.Content = selectedBond.last;
            ComputationResult result = new ComputationResult();
            try
            {
                var client = new WebClient();
                client.Proxy = null;
                Stream stream = client.OpenRead("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/bonddetails?ISIN=" +
                    selectedBond.isin.ToString() + "&Qty=" + "1" + "&param=Yield&value=" + selectedBond.yield.ToString());
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ComputationResult));
                result = (ComputationResult)serializer.ReadObject(stream);
            }
            catch
            {
                MessageBox.Show("Rest is resting");
            }
            DesiredYieldBox.Text = result.desiredYield.ToString();
            CleanPriceBox.Text = result.cleanPrice.ToString();
            DirtyPriceBox.Text = result.dirtyPrice.ToString();
            AccruedAmountLabel.Content = result.accruedAmount.ToString();
            SettlementAmountLabel.Content = result.settlementAmount.ToString();
        }

        private void QuantityBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow win = (MainWindow)Window.GetWindow(this);
                string uid = win.userid;
                DateTime TradeTime = DateTime.Now;
                var client = new WebClient();
                client.Proxy = null;
                client.DownloadString("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/tradePlaced?Username=" + uid + "&ISIN=" + bond.isin
                    + "&year=" + TradeTime.Year + "&month=" + TradeTime.Month + "&day=" + TradeTime.Day + "&hour=" + TradeTime.Hour +
                    "&minutes=" + TradeTime.Minute + "&seconds=" + TradeTime.Second + "&buySell=buy&price=" + BidPriceBox.Text + "&Qty=" + QuantityBox.Text);
                Trade t = new Trade();
                t.isin = "ABCD";
                Trade.trades.Add(t);
            }
            catch
            {
                MessageBox.Show("REST is resting");
            }
        }

        private void QuantityBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if ((int.Parse(QuantityBox.Text) < 0) || (int.Parse(QuantityBox.Text) > 10000))
                    {
                        MessageBox.Show("Quantity value must be between 0 and 100000");
                    }
                    else
                    {
                        var client = new WebClient();
                        client.Proxy = null;
                        Stream stream = client.OpenRead("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/bonddetails?ISIN=" +
                            bond.isin.ToString() + "&Qty=" + QuantityBox.Text + "&param=Yield&value=" + bond.yield.ToString());
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ComputationResult));
                        ComputationResult result = (ComputationResult)serializer.ReadObject(stream);
                        DesiredYieldBox.Text = result.desiredYield.ToString();
                        CleanPriceBox.Text = result.cleanPrice.ToString();
                        DirtyPriceBox.Text = result.dirtyPrice.ToString();
                        AccruedAmountLabel.Content = result.accruedAmount.ToString();
                        SettlementAmountLabel.Content = result.settlementAmount.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Limit Exceeded");
            }
            
        }

        private void DesiredYieldBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    var client = new WebClient();
                    client.Proxy = null;
                    Stream stream = client.OpenRead("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/bonddetails?ISIN=" +
                    bond.isin.ToString() + "&Qty=" + QuantityBox.Text + "&param=Yield&value=" + DesiredYieldBox.Text.ToString());
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ComputationResult));
                    ComputationResult result = (ComputationResult)serializer.ReadObject(stream);
                    DesiredYieldBox.Text = result.desiredYield.ToString();
                    CleanPriceBox.Text = result.cleanPrice.ToString();
                    DirtyPriceBox.Text = result.dirtyPrice.ToString();
                    AccruedAmountLabel.Content = result.accruedAmount.ToString();
                    SettlementAmountLabel.Content = result.settlementAmount.ToString();
                    
                }
                catch
                {
                    MessageBox.Show("Overflow error");
                }
            }
        }

        private void CleanPriceBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    if (double.Parse(CleanPriceBox.Text) < 0)
                    {
                        MessageBox.Show("Clean Price cannot be less than 0.");
                    }
                    else
                    {
                        var client = new WebClient();
                        client.Proxy = null;
                        Stream stream = client.OpenRead("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/bonddetails?ISIN=" +
                            bond.isin.ToString() + "&Qty=" + QuantityBox.Text + "&param=CleanPrice&value=" + CleanPriceBox.Text.ToString());
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ComputationResult));
                        ComputationResult result = (ComputationResult)serializer.ReadObject(stream);
                        DesiredYieldBox.Text = result.desiredYield.ToString();
                        CleanPriceBox.Text = result.cleanPrice.ToString();
                        DirtyPriceBox.Text = result.dirtyPrice.ToString();
                        AccruedAmountLabel.Content = result.accruedAmount.ToString();
                        SettlementAmountLabel.Content = result.settlementAmount.ToString();
                    }
                }
                catch
                {
                    MessageBox.Show("Limit exceeded");
                }
            }
        }

        private void DirtyPriceBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    if (double.Parse(CleanPriceBox.Text) < 0)
                    {
                        MessageBox.Show("Clean Price cannot be less than 0.");
                    }
                    else
                    {
                        var client = new WebClient();
                        client.Proxy = null;
                        Stream stream = client.OpenRead("http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds/bonddetails?ISIN=" +
                            bond.isin.ToString() + "&Qty=" + QuantityBox.Text + "&param=DirtyPrice&value=" + DirtyPriceBox.Text.ToString());
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ComputationResult));
                        ComputationResult result = (ComputationResult)serializer.ReadObject(stream);
                        DesiredYieldBox.Text = result.desiredYield.ToString();
                        CleanPriceBox.Text = result.cleanPrice.ToString();
                        DirtyPriceBox.Text = result.dirtyPrice.ToString();
                        AccruedAmountLabel.Content = result.accruedAmount.ToString();
                        SettlementAmountLabel.Content = result.settlementAmount.ToString();
                    }
                }
                catch
                {
                    MessageBox.Show("Limit exceeded");
                }
                
            }
        }
    }
}
