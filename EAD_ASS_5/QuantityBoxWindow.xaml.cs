using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace EAD_ASS_5
{
    /// <summary>
    /// Interaction logic for QuantityBoxWindow.xaml
    /// </summary>
    public partial class QuantityBoxWindow : Window
    {
        private int availableQuantity;
        private bool _cancelled;
        private int _quantity;
        public bool cancelled { get { return this._cancelled; } }
        public int quantityEntered { get { return this._quantity; } }
        public QuantityBoxWindow(int availableQuantity)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.ToolWindow;
            this.availableQuantity = availableQuantity;
            available.Text = availableQuantity.ToString();
            quantity.Focus();
            yes.IsEnabled = false;
            _cancelled = true;
        }

        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            yes.IsEnabled = true;
            error.Text = "";
            if (!Regex.IsMatch(quantity.Text, "^[0-9]+$") || Convert.ToInt32(quantity.Text) <= 0)
            {
                error.Text = "Must Enter Integer Value (greater than zero)";
                yes.IsEnabled = false;
            }
            else
            {
                _quantity = Convert.ToInt32(quantity.Text);
                if (_quantity > availableQuantity)
                {
                    error.Text = $"{_quantity} Items are not available";
                    yes.IsEnabled = false;
                }
            }
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            _cancelled = false;
            this.Close();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            _cancelled = true;
            this.Close();
        }

        private void quantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && yes.IsEnabled)
            {
                _cancelled = false;
                this.Close();
            }
        }
    }
}
