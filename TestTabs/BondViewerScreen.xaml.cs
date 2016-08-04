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
    /// Interaction logic for BondViewerScreen.xaml
    /// </summary>
    public partial class BondViewerScreen : UserControl
    {
        public List<Bond> bonds = new List<Bond>();
        public List<Bond> currentBondsList;
        public BondViewerScreen(List<Bond> bondsList)
        {
            InitializeComponent();
            BondsDataGrid.ItemsSource = bondsList;
            BondsDataGrid.Loaded += SetMinWidths;
            bonds = bondsList;
            CurrencyFilter.Items.Add("Select Currency");
            CurrencyFilter.Items.Add("GBP");
            CurrencyFilter.Items.Add("INR");
            CurrencyFilter.Items.Add("JPY");
            CurrencyFilter.Items.Add("USD");
            CurrencyFilter.SelectedIndex = 0;
            FieldFilter.Items.Add("Select Field");
            FieldFilter.Items.Add("Agri");
            FieldFilter.Items.Add("Energy");
            FieldFilter.Items.Add("Environment");
            FieldFilter.Items.Add("Infra");
            FieldFilter.Items.Add("Insurance");
            FieldFilter.Items.Add("Tech");
            FieldFilter.SelectedIndex = 0;
            FrequencyFilter.Items.Add("Select coupon frequency");
            FrequencyFilter.Items.Add("Annual");
            FrequencyFilter.Items.Add("Semi-Annual");
            FrequencyFilter.Items.Add("Quarterly");
            FrequencyFilter.SelectedIndex = 0;
            RatingFilter.Items.Add("Select rating");
            RatingFilter.Items.Add("AAA");
            RatingFilter.Items.Add("AA");
            RatingFilter.Items.Add("BBB");
            RatingFilter.Items.Add("BB");
            RatingFilter.Items.Add("B+");
            RatingFilter.SelectedIndex = 0;
            currentBondsList = bonds;
            BondSelectBox.ItemsSource = currentBondsList;
        }

        private void TradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (BondsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Bond");
            }
            else
            {
                MainWindow win = (MainWindow)Window.GetWindow(this);
                win.CreateNewTradingWindow((Bond)BondsDataGrid.SelectedItem);
            }

        }

        private void BondsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BondsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Bond");
            }
            else
            {
                MainWindow win = (MainWindow)Window.GetWindow(this);
                win.CreateNewTradingWindow((Bond)BondsDataGrid.SelectedItem);
                //Bond b = (Bond)BondsDataGrid.SelectedItem;
            }
        }

        public void SetMinWidths(object source, EventArgs e)
        {
            foreach (var column in BondsDataGrid.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                if (column.Header.ToString() == "isin")
                    column.Header = "ISIN";
                if (column.Header.ToString() == "maturityMonth" || column.Header.ToString() == "maturityDay" || column.Header.ToString() == "maturityYear")
                {
                    column.Visibility = Visibility.Hidden;
                }
                if (column.Header.ToString() == "issueMonth" || column.Header.ToString() == "issueDay" || column.Header.ToString() == "issueYear")
                {
                    column.Visibility = Visibility.Hidden;
                }
                if (column.Header.ToString() == "high" || column.Header.ToString() == "low" || column.Header.ToString() == "last"
                     || column.Header.ToString() == "change" || column.Header.ToString() == "frequency" || column.Header.ToString() == "time"
                      || column.Header.ToString() == "bid" || column.Header.ToString() == "ask" || column.Header.ToString() == "creditRating")
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }

        public void FormatColumns()
        {
            foreach (var column in BondsDataGrid.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                if (column.Header.ToString() == "isin")
                    column.Header = "ISIN";
                if (column.Header.ToString() == "maturityMonth" || column.Header.ToString() == "maturityDay" || column.Header.ToString() == "maturityYear")
                {
                    column.Visibility = Visibility.Hidden;
                }
                if (column.Header.ToString() == "issueMonth" || column.Header.ToString() == "issueDay" || column.Header.ToString() == "issueYear")
                {
                    column.Visibility = Visibility.Hidden;
                }
                if (column.Header.ToString() == "high" || column.Header.ToString() == "low" || column.Header.ToString() == "last"
                     || column.Header.ToString() == "change" || column.Header.ToString() == "frequency" || column.Header.ToString() == "time"
                      || column.Header.ToString() == "bid" || column.Header.ToString() == "ask" || column.Header.ToString() == "creditRating")
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }

        private void CurrencyFilter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Currency();
            //if (CurrencyFilter.SelectedIndex != 0)
            //{
            //    //string uri = "http://192.168.137.158:8080/EbondSharkWeb/rest/ebonds?" + FilterSelector.SelectedItem.ToString() + "=" + CurrencyFilter.SelectedItem.ToString();
            //    List<Bond> filteredListofBonds = new List<Bond>();
            //    foreach (Bond bond in bonds)
            //    {
            //        if(bond.currency == CurrencyFilter.SelectedItem.ToString())
            //        {
            //            filteredListofBonds.Add(bond);
            //        }

            //    }
            //    RepopulateList(filteredListofBonds);
            //}
        }

        //private void FilterSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if(FilterSelector.SelectedIndex != 0)
        //    {
        //        switch (FilterSelector.SelectedIndex)
        //        {
        //            case 0: BondsDataGrid.ItemsSource = bonds;
        //                FieldFilter.Visibility = Visibility.Hidden;
        //                CurrencyFilter.Visibility = Visibility.Hidden;
        //                FrequencyFilter.Visibility = Visibility.Hidden;
        //                break;



        //            case 1: FieldFilter.SelectedIndex = 0;
        //                FieldFilter.Visibility = Visibility.Visible;
        //                CurrencyFilter.Visibility = Visibility.Hidden;
        //                FrequencyFilter.Visibility = Visibility.Hidden;
        //                break;

        //            case 2: FrequencyFilter.SelectedIndex = 0;
        //                FieldFilter.Visibility = Visibility.Hidden;
        //                CurrencyFilter.Visibility = Visibility.Hidden;
        //                FrequencyFilter.Visibility = Visibility.Visible;
        //                break;

        //            case 3: CurrencyFilter.SelectedIndex = 0;
        //                FieldFilter.Visibility = Visibility.Hidden;
        //                CurrencyFilter.Visibility = Visibility.Visible;
        //                FrequencyFilter.Visibility = Visibility.Hidden;
        //                CurrencyFilter.SelectedIndex = 0;
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        BondsDataGrid.ItemsSource = bonds;
        //        FormatColumns();
        //    }
        //}

        private void FieldFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Currency();
        }

        public void Currency()
        {
            if (CurrencyFilter.SelectedIndex != -1)
            {
                List<Bond> filteredList = new List<Bond>();
                if (CurrencyFilter.SelectedIndex == 0)
                {
                    filteredList = bonds;
                }
                else
                {
                    foreach (Bond bond in bonds)
                    {
                        if (bond.currency == CurrencyFilter.SelectedItem.ToString())
                        {
                            filteredList.Add(bond);
                        }
                    }
                }
                Frequency(filteredList);
            }
        }

        public void Frequency(List<Bond> filteredList)
        {
            List<Bond> newFilteredList = new List<Bond>();
            if (FrequencyFilter.SelectedIndex != -1)
            {
                if (FrequencyFilter.SelectedIndex == 0)
                {
                    RatingF(filteredList);
                }
                else
                {
                    foreach (Bond bond in filteredList)
                    {
                        if (bond.couponFrequency == FrequencyFilter.SelectedItem.ToString())
                        {
                            newFilteredList.Add(bond);
                        }

                    }
                    RatingF(newFilteredList);
                }
            }
        }

        public void Field(List<Bond> filteredList)
        {
            List<Bond> newFilteredList = new List<Bond>();
            if (FieldFilter.SelectedIndex != -1)
            {
                if (FieldFilter.SelectedIndex == 0)
                {
                    RepopulateList(filteredList);
                }
                else
                {
                    foreach (Bond bond in filteredList)
                    {
                        if (bond.field == FieldFilter.SelectedItem.ToString())
                        {
                            newFilteredList.Add(bond);
                        }

                    }
                    RepopulateList(newFilteredList);
                }

                this.currentBondsList = new List<Bond>(newFilteredList);
            }

        }

        public void RatingF(List<Bond> filteredList)
        {
            List<Bond> newFilteredList = new List<Bond>();
            if (RatingFilter.SelectedIndex != -1)
            {
                if (RatingFilter.SelectedIndex == 0)
                {
                    Field(filteredList);
                }
                else
                {

                    foreach (Bond bond in filteredList)
                    {
                        if (bond.Rating == RatingFilter.SelectedItem.ToString())
                        {
                            newFilteredList.Add(bond);
                        }
                    }
                    Field(newFilteredList);
                }

            }
        }

        private void FrequencyFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Currency();
        }

        public void RepopulateList(string uri)
        {
            List<Bond> filteredListOfBonds = new List<Bond>();
            var client = new WebClient();
            client.Proxy = null;
            Stream stream = client.OpenRead(uri);
            //StreamReader stream = new StreamReader("Bonds.txt");
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Bond[]));
            Bond[] bondsArray = (Bond[])serializer.ReadObject(stream);
            filteredListOfBonds = bondsArray.ToList<Bond>();
            foreach (Bond bond in filteredListOfBonds)
            {
                bond.setDates();
            }
            BondsDataGrid.ItemsSource = filteredListOfBonds;
            currentBondsList = filteredListOfBonds;
            FormatColumns();
        }

        public void RepopulateList(List<Bond> filteredList)
        {
            BondsDataGrid.ItemsSource = filteredList;
            currentBondsList = filteredList;
            FormatColumns();
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.CreateNewTradingWindow((Bond)BondSelectBox.SelectedItem);
        }

        private void BondSelectBox_GotFocus(object sender, RoutedEventArgs e)
        {
            BondSelectBox.Text = "";
        }

        private void BondSelectBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BondSelectBox.Text = "Select Bond";

        }

        private void RatingFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Currency();
        }
    }
}
