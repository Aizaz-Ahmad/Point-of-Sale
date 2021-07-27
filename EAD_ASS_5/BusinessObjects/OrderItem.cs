using EAD_ASS_5.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
namespace EAD_ASS_5.BusinessObjects
{
    public class OrderItem : Item, INotifyPropertyChanged
    {
        public double totalPrice { get; set; }
        private const double TAX_PERCENTAGE = 0.03;
        private int _quantity;
        public new int quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
                this.totalPrice = price * _quantity;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("quantity"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public string _comments;
        public string comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("comments"));
            }
        }
        public ObservableCollection<bool> additionalItems { get; set; }
        public static ObservableCollection<string> additionalItemsName { get; set; } = DAO.GetadditionalItemsName();
        public OrderItem(Item item) : base(item)
        {
            additionalItems = new ObservableCollection<bool>();
            additionalItems.Add(false);
            additionalItems.Add(false);
            additionalItems.Add(false);
            additionalItems.Add(false);
            additionalItems.Add(false);
            additionalItems.Add(false);
            additionalItems.Add(false);
            additionalItems.Add(false);

            additionalItems.CollectionChanged += AdditionalItems_CollectionChanged;

            this.totalPrice = price * _quantity;
        }

        private void AdditionalItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                List<string> names = new List<string>();
                for (int i = 0; i < additionalItems.Count; i++)
                    if (additionalItems[i])
                        names.Add(additionalItemsName[i]);
                comments = String.Join(", ", names.ToArray());
            }
        }

        public double calcTax()
        {
            return Convert.ToInt32(tax) * totalPrice * TAX_PERCENTAGE;
        }
    }
}
