using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
namespace EAD_ASS_5.BusinessObjects
{
    public class Order : INotifyPropertyChanged
    {
        public ObservableCollection<OrderItem> orderItems { get; set; }
        private double _subTotal;
        private double _tax;
        private double _grandTotal;
        public int order_id { get; set; }
        public double discountPercentage { get; set; }
        public DateTime date_created;
        public double subTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                OnPropertyRaised("subTotal");
            }
        }
        public double Tax
        {
            get { return _tax; }
            set
            {
                _tax = value;
                OnPropertyRaised("Tax");
            }
        }
        public double grandTotal
        {
            get { return _grandTotal; }
            set
            {
                _grandTotal = value;
                OnPropertyRaised("grandTotal");
            }
        }
        public Order()
        {
            orderItems = new ObservableCollection<OrderItem>();
            orderItems.CollectionChanged += OrderItems_CollectionChanged;
            discountPercentage = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OrderItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OrderItem orderItem = (OrderItem)e.NewItems[0];
                subTotal += orderItem.totalPrice;
                Tax += orderItem.calcTax();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                OrderItem orderItem = (OrderItem)e.OldItems[0];
                subTotal -= orderItem.totalPrice;
                Tax -= orderItem.calcTax();
            }
            Tax = Convert.ToDouble(String.Format("{0:0.##}", Tax));// to convert to 2 decimal point number
            grandTotal = (subTotal + Tax);
            grandTotal -= (discountPercentage / 100 * grandTotal);
        }
        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
