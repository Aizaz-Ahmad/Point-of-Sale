using System.Windows;
using System.Windows.Controls;
namespace EAD_ASS_5.UserControls
{
    /// <summary>
    /// Interaction logic for ItemCategoryBox.xaml
    /// </summary>
    public partial class ItemCategoryBox : UserControl
    {
        public bool isSelected
        {
            get { return isSelected; }
            set
            {
                border.BorderThickness = new Thickness(value ? 3 : 0);
            }
        }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public ItemCategoryBox()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            content.Text = CategoryName;
        }
    }
}
