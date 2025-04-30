using ApplicationApp.View.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApplicationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Agent(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageAgents(MainFrame));
           
        }

        private void Open_Material(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate (new PageMaterial(MainFrame));
        }

        private void Open_Warehause(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageWarehouses(MainFrame));
        }

        private void Open_Product(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageProducts(MainFrame));
        }

        private void Open_Warehouse_product(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageWarehouseProducts(MainFrame));
        }

        private void Open_Sales(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageSales(MainFrame));
        }
    }
}