using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TestTabs
{
    [DataContract]
    public class Bond
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string issuerName { get; set; }
        [DataMember]
        public string isin { get; set; }
        [DataMember]
        public double coupon { get; set; }
        [DataMember]
        public int maturityMonth { get; set; }
        [DataMember]
        public int maturityDay { get; set; }
        [DataMember]
        public int maturityYear { get; set; }
        public DateTime MaturityDate { get; set; }
        [DataMember]
        public double high { get; set; }
        [DataMember]
        public double low { get; set; }
        [DataMember]
        public double last { get; set; }
        [DataMember]
        public double yield { get; set; }
        [DataMember]
        public double change { get; set; }
        [DataMember]
        public int frequency { get; set; }
        public string couponFrequency { get; set; }
        [DataMember]
        public int issueMonth { get; set; }
        [DataMember]
        public int issueDay { get; set; }
        [DataMember]
        public int issueYear { get; set; }
        public DateTime Issue_Date { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public int creditRating { get; set; }
        public string Rating { get; set; }
        [DataMember]
        public int time { get; set; }
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public double ask { get; set; }
        [DataMember]
        public double bid { get; set; }

        public Bond()
        {

        }

        public Bond(int bondID, string Iname, string BondISIN, double BondCoupon, int mmonth, int mday, int myear,
            double dayHigh, double dayLow, double dayLast, double BondYield, double priceChange, int couponFrequency, int imonth,
            int iday, int iyear, string Bondcurrency, int CreditRate, int maturityTime, string BondField, string bondCategory,
            double askPrice, double bidPrice)
        {
            id = bondID;
            isin = BondISIN;
            issuerName = Iname;
            coupon = BondCoupon;
            maturityMonth = mmonth;
            maturityDay = mday;
            maturityYear = myear;
            high = dayHigh;
            low = dayLow;
            last = dayLast;
            yield = BondYield;
            change = priceChange;
            frequency = couponFrequency;
            issueMonth = imonth;
            issueDay = iday;
            issueYear = iyear;
            currency = Bondcurrency;
            creditRating = CreditRate;
            time = maturityTime;
            field = BondField;
            category = bondCategory;
            ask = askPrice;
            bid = bidPrice;
        }

        public void setDates()
        {
            switch (creditRating)
            {
                case 1:
                    Rating = "B+";
                    break;

                case 2:
                    Rating = "BB";
                    break;

                case 3:
                    Rating = "BBB";
                    break;

                case 4:
                    Rating = "AA";
                    break;

                case 5:
                    Rating = "AAA";
                    break;

                default: break;
            }
            MaturityDate = new DateTime(maturityYear, maturityMonth, maturityDay);
            if (!(issueDay == 29 && issueMonth == 2))
                Issue_Date = new DateTime(issueYear, issueMonth, issueDay);
            switch (frequency)
            {
                case 1:
                    couponFrequency = "Annual";
                    break;

                case 2:
                    couponFrequency = "Semi-Annual";
                    break;

                case 4:
                    couponFrequency = "Quarterly";
                    break;

                default: break;
            }
        }

        public override string ToString()
        {
            return isin + " " + issuerName;
        }
    }
}
