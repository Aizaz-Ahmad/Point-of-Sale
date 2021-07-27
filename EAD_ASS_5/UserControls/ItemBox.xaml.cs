using EAD_ASS_5.BusinessObjects;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace EAD_ASS_5.UserControls
{
    /// <summary>
    /// Interaction logic for ItemBox.xaml
    /// </summary>
    public partial class ItemBox : UserControl
    {
        public Item item { get; set; }

        public ItemBox()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            icon.Source = new BitmapImage(new Uri("../Images/items/" + item.itemCategory.name + ".png", UriKind.Relative));
            itemName.Text = item.name;
            quantity.Text = "Qty: " + item.quantity.ToString();
            price.Text = "Price: " + item.price.ToString();
        }
        public void decreaseQuantity(int Quantity)
        {
            item.quantity -= Quantity;
            quantity.Text = "Qty: " + item.quantity.ToString();
        }
        public void increaseQuantity(int Quantity)
        {
            item.quantity += Quantity;
            quantity.Text = "Qty: " + item.quantity.ToString();
        }
    }
}
