using EAD_ASS_5.BusinessObjects;
using EAD_ASS_5.Model;
using Spire.Pdf;
using System;
using System.IO;
using System.IO.Packaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace EAD_ASS_5
{
    /// <summary>
    /// Interaction logic for OrderDetailsWindow.xaml
    /// </summary>
    public partial class OrderDetailsWindow : Window
    {
        public void generatePDF(string path)
        {
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    MemoryStream lMemoryStream = new MemoryStream();
                    Package package = Package.Open(lMemoryStream, FileMode.Create);
                    XpsDocument doc = new XpsDocument(package);
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                    writer.Write(grid);
                    doc.Close();
                    package.Close();

                    PdfDocument pdfDocument = new PdfDocument();
                    pdfDocument.LoadFromXPS(lMemoryStream);
                    pdfDocument.SaveToFile(path, FileFormat.PDF);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });
        }
        public OrderDetailsWindow(Order order)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = order;
            int order_id = DAO.saveOrder(order);
            order.order_id = order_id;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = this.DataContext as Order;
                string path = System.IO.Path.GetFullPath("../../order_slips/") + $"order # {order.order_id} ({order.date_created.ToString("dd-M-yyyy")}).pdf";
                await Task.Run(() =>
                {
                    this.generatePDF(path);
                });
                MessageBoxResult result = MessageBox.Show("Order Slip Saved to the path " + path + "\nDo you want to open Slip?", "Order Slip Saved", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                    System.Diagnostics.Process.Start(path);
            }
            catch (Exception a)
            {
                throw a;
            }
        }
    }
}
