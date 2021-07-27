using EAD_ASS_5.BusinessObjects;
using EAD_ASS_5.Model;
using EAD_ASS_5.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EAD_ASS_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private ItemCategoryBox selectedCategory;
        public Order order { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.startClock();
            order = new Order();
            this.loadItemCategories();
            this.loadItems();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            orderItemsTable.ItemsSource = order.orderItems;
            orderSection.DataContext = order;
            checkboxes.DataContext = new OrderItem(new Item());

        }


        private void changeCurrentDate(object source, EventArgs e)
        {
            todayDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }
        private void startClock()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += changeCurrentDate;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void loadItemCategories()
        {
            try
            {
                List<ItemCategory> itemCategories = DAO.GetItemCategories();
                foreach (ItemCategory itemCategory in itemCategories)
                {
                    ItemCategoryBox itemCategoryBox = new ItemCategoryBox { CategoryId = itemCategory.id, CategoryName = itemCategory.name };
                    itemCategoryBox.MouseDown += ItemCategoryBox_MouseDown;
                    itemCatogoriesPanel.Children.Add(itemCategoryBox);
                }
                selectedCategory = (ItemCategoryBox)itemCatogoriesPanel.Children[0];
                selectedCategory.isSelected = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Occured while retreiving categories: " + e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ItemCategoryBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ItemCategoryBox itemCategoryBox = (ItemCategoryBox)sender;
            selectedCategory.isSelected = false;
            itemCategoryBox.isSelected = true;
            this.changeItems(itemCategoryBox.CategoryId);
            selectedCategory = itemCategoryBox;
        }
        private void changeItems(int categoryId)
        {
            UIElementCollection collection = Items.Children;
            for (int i = 0; i < collection.Count; i++)
            {
                ItemBox itemBox = (ItemBox)collection[i];
                itemBox.Visibility = (categoryId == 1 || itemBox.item.itemCategory.id == categoryId ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        private void loadItems()
        {
            Items.Children.Clear();
            try
            {
                List<Item> orderItems = DAO.GetItems();
                foreach (Item item in orderItems)
                {
                    ItemBox itemBox = new ItemBox { item = item, Width = 135, Margin = new Thickness(5) };
                    itemBox.MouseDown += ItemBox_MouseDown;
                    Items.Children.Add(itemBox);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Occured while retreiving categories: " + e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ItemBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ItemBox itemBox = (ItemBox)sender;
            Item item = new Item(itemBox.item);
            QuantityBoxWindow boxWindow = new QuantityBoxWindow(item.quantity);
            boxWindow.ShowDialog();
            if (!boxWindow.cancelled)
            {
                item.quantity = boxWindow.quantityEntered;
                OrderItem orderItem = new OrderItem(item);
                orderItem.quantity = item.quantity;
                OrderItem alreadyAddedItem = order.orderItems.FirstOrDefault((orderitem) => orderitem.id == orderItem.id);
                if (alreadyAddedItem != null)
                {
                    updateOrderItemQuantityInGrid(alreadyAddedItem, orderItem.quantity);
                    itemBox.decreaseQuantity(alreadyAddedItem.quantity);
                }
                else
                {
                    itemBox.decreaseQuantity(item.quantity);
                    order.orderItems.Add(orderItem);
                }
            }
        }
        private void updateOrderItemQuantityInGrid(OrderItem orderItem, int quantity)
        {
            int index = order.orderItems.IndexOf(orderItem);
            order.orderItems.RemoveAt(index);
            orderItem.quantity += quantity;
            order.orderItems.Insert(index, orderItem);
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)sender;
            OrderItem orderItem = el.DataContext as OrderItem;
            order.orderItems.Remove(orderItem);
        }


        private void orderItemsTable_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            OrderItem orderItem = e.Row.DataContext as OrderItem;
            updateItemBoxQuantity(orderItem, orderItem.quantity);
        }
        private void updateItemBoxQuantity(OrderItem orderItem, int quantity)
        {
            for (int i = 0; i < Items.Children.Count; i++)
            {
                if (((ItemBox)Items.Children[i]).item.id == orderItem.id)
                {
                    ((ItemBox)Items.Children[i]).increaseQuantity(quantity);
                    break;
                }
            }
        }
        private void purcahse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderDetailsWindow orderDetailsWindow = new OrderDetailsWindow(order);
                orderDetailsWindow.ShowDialog();
                this.refreshWindow();
            }
            catch (Exception a)
            {
                MessageBox.Show("Exception Occured while saving Order " + a.Message, "Saving Order", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void orderItemsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderItem orderItem = orderItemsTable.SelectedItem as OrderItem;
            checkboxes.Visibility = orderItem != null ? Visibility.Visible : Visibility.Hidden;
            if (orderItem != null)
                checkboxes.DataContext = orderItem;
        }
        private void refreshWindow()
        {
            order = new Order();
            orderSection.DataContext = order;
            orderItemsTable.ItemsSource = order.orderItems;
            this.loadItems();
            this.changeItems(selectedCategory.CategoryId);
        }


        private void PreviousOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(System.IO.Path.GetFullPath("../../order_slips/"));
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            this.refreshWindow();
        }

        private void decQuantityBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)sender;
            OrderItem orderItem = el.DataContext as OrderItem;
            if (orderItem.quantity == 1)
                order.orderItems.Remove(orderItem);
            else
            {
                updateOrderItemQuantityInGrid(orderItem, -1);
                this.updateItemBoxQuantity(orderItem, -orderItem.quantity);
            }
        }
    }
}
