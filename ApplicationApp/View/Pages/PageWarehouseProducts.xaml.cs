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

namespace ApplicationApp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageWarehouseProducts.xaml
    /// </summary>
    public partial class PageWarehouseProducts : Page
    {
        Frame MainFrame;
        WarehouseProductViewModel ViewModel;
        public PageWarehouseProducts(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            ViewModel = new WarehouseProductViewModel();
            this.DataContext = ViewModel;
            ViewModel.ShowMessage += msg => MessageBox.Show(msg);
        }

        

        private void OpenAddPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageWarehouseProductsWrite(MainFrame));
        }

        private void EtitOpenPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageWarehouseProductsWrite(MainFrame,ViewModel.SelectedWarehouseProduct));
        }
    }
}
